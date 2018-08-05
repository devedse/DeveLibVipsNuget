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

        private static bool ExtractedFilesOnce = false;
        private static object Lockject = new object();

        private static Lazy<string> LibVipsDirLazy = new Lazy<string>(() =>
        {
            var location = Assembly.GetEntryAssembly().Location;
            var dirLocation = Path.GetDirectoryName(location);
            var combined = Path.Combine(dirLocation, LibVipsSubDir);
            return combined;
        }, true);

        public static void ExtractLibVips()
        {
            if (!Directory.Exists(LibVipsDir))
            {
                Directory.CreateDirectory(LibVipsDir);
            }
            var executingAssembly = Assembly.GetExecutingAssembly();
            var resources = executingAssembly.GetManifestResourceNames();

            foreach (var resource in resources)
            {
                var expectedFileName = resource.Substring("DeveLibVipsNuget.LibVips.".Length);
                var obtainedFileStream = executingAssembly.GetManifestResourceStream(resource);
                var destPath = Path.Combine(LibVipsDir, expectedFileName);

                if (!File.Exists(destPath) || obtainedFileStream.Length != new FileInfo(destPath).Length)
                {
                    Console.WriteLine($"Extracting {expectedFileName}...");
                    using (var fs = new FileStream(destPath, FileMode.Create, FileAccess.Write, FileShare.Read))
                    {
                        var bytesToWrite = new byte[obtainedFileStream.Length];
                        obtainedFileStream.Read(bytesToWrite, 0, bytesToWrite.Length);
                        fs.Write(bytesToWrite, 0, bytesToWrite.Length);
                    }
                }
            }

            Console.WriteLine("LibVips extraction complete");
        }

        public static string ExtractAndGetVipsExeFile()
        {
            if (!ExtractedFilesOnce)
            {
                lock (Lockject)
                {
                    if (!ExtractedFilesOnce)
                    {
                        ExtractLibVips();
                        ExtractedFilesOnce = true;
                    }
                }
            }
            return Path.Combine(LibVipsDir, LibVipsExeFile);
        }
    }
}
