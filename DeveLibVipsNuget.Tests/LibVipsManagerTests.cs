using System.IO;
using Xunit;

namespace DeveLibVipsNuget.Tests
{
    public class LibVipsManagerTests
    {
        [Fact]
        public void ExtractsFiles()
        {
            var vipsFile = LibVipsManager.ExtractAndGetVipsExeFile();
            var fileExists = File.Exists(vipsFile);
            Assert.True(fileExists);
        }
    }
}
