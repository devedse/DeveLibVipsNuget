using System;
using System.IO;
using System.Reflection;

namespace DeveLibVipsNuget
{
    public static class LibVipsManager
    {
        public static string LibVipsDir => LibVipsDirLazy.Value;

        private const string LibVipsSubDir = "LibVips";
        private const string LibVipsExeFile = "vips.exe";

        private static Lazy<string> LibVipsDirLazy = new Lazy<string>(() =>
        {
            var location = Assembly.GetEntryAssembly().Location;
            var combined = Path.Combine(location, LibVipsSubDir);
            return combined;
        }, true);

        public static void ExtractLibVips()
        {
            var resources = Assembly.GetExecutingAssembly().GetManifestResourceNames();

        }

        public static string ExtractAndGetVipsExeFile()
        {
            ExtractLibVips();
            return Path.Combine(LibVipsDir, LibVipsExeFile);
        }
    }
}
