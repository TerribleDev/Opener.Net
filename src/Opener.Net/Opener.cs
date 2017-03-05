using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;

namespace Opener.Net
{
    public static class Opener
    {
        /// <summary>
        /// Launch a command on macOS, Linux, or windows
        /// </summary>
        /// <param name="command">the command to launch</param>
        /// <param name="arguments">any arguments you wish to pass</param>
        /// <param name="processStartInfo">You may configure the process with the System.Diagnostics.ProcessStartInfo type. However filename, and arguments will be overriden. If this is null, by default stdout/stderr will be redirected</param>
        /// <returns></returns>
        public static Process Open(string command, IEnumerable<string> arguments = null, ProcessStartInfo processStartInfo = null)
        {
            var cmdRun = BuildCommand(command, arguments);
            var startInfo = processStartInfo  ?? new ProcessStartInfo(){
                RedirectStandardOutput = true,
                RedirectStandardInput = true,
                RedirectStandardError = true
            };
            startInfo.FileName = cmdRun.Command;
            startInfo.Arguments = string.Join(" ", cmdRun.Arguments);
            return System.Diagnostics.Process.Start(startInfo);
        }
        internal static ResolvedCommand BuildCommand(string command, IEnumerable<string> arguments = null )
        {
            ResolvedCommand resolvedCommand;
            if(arguments == null) arguments = Enumerable.Empty<string>();
            if(RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                resolvedCommand = WinCommand(command, arguments);
                //c:/windows/system32/cmd.exe "/c" start "" "usercommand" otherargs
            }
            else if(RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                resolvedCommand = MacCommand(command, arguments);
                //mac "open command"
                // open shellCommand args
                
            }
            else if(RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                resolvedCommand = LinCommand(command, arguments);
                //linux "xdg-open" shellCommand args
            }
            else
            {
                throw new PlatformNotSupportedException();
            }
            return resolvedCommand;
        }

        internal static ResolvedCommand WinCommand(string command, IEnumerable<string> arguments = null)
        {
                var shellCommand = "cmd.exe";
                var args = arguments?.Select(a=>{
                    return a.Replace("&", "^&");
                }).ToList() ?? new List<string>();
                args.Insert(0, "/c");
                args.Insert(1, "start");
                args.Insert(2, string.Empty);
                args.Insert(3, command);
                return new ResolvedCommand(){Command = shellCommand, Arguments = args};
        }
        internal static ResolvedCommand LinCommand(string command, IEnumerable<string> arguments = null)
        {
            var shellCommand = "xdg-open";
            var args = arguments?.ToList() ?? new List<string>();
            args.Insert(0, command);
            return new ResolvedCommand(){ Command = shellCommand, Arguments = args};
        }
        internal static ResolvedCommand MacCommand(string command, IEnumerable<string> arguments = null)
        {
            var shellCommand = "open";
            var args = arguments?.ToList() ?? new List<string>();
            args.Insert(0, command);
            return new ResolvedCommand(){ Command = shellCommand, Arguments = args};
        }
        public class ResolvedCommand 
        {
            public string Command { get; set; }
            public List<string> Arguments { get; set; }
        }
    }
}