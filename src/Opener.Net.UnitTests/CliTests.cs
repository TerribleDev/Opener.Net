using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Opener.Net.UnitTests
{
    public class CliTests
    {
        [Fact]
        public void AssertPlatforms()
        {
            var results = Opener.LinCommand("npm install");
            Assert.Equal(results.Command, "xdg-open");
            Assert.Equal(results.Arguments, new List<string>(){ "npm install"});

            results = Opener.MacCommand("npm install");
            Assert.Equal(results.Command, "open");
            Assert.Equal(results.Arguments, new List<string>(){ "npm install"});

            results = Opener.WinCommand("npm install");
            Assert.Equal(results.Command, "cmd.exe");
            Assert.Equal(results.Arguments, new List<string>(){ "/c","start",string.Empty,"npm install"});

        }
    }
}