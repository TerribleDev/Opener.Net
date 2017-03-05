using System;
using System.Diagnostics;
using System.Linq;
using Xunit;

namespace Tests
{
    public class Tests
    {
        [Fact]
        public void Test1() 
        {
            var result = Opener.Net.Opener.Open("http://google.com");
            result.WaitForExit();
        }
        [Fact]
        public void Test2() 
        {
            var result = Opener.Net.Opener.Open("npm install", processStartInfo: new ProcessStartInfo(){ WorkingDirectory = "c:/projects/npmrest"});
            result.WaitForExit();
        }
    }
}
