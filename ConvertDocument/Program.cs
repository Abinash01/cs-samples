using System;
using System.IO;
using Leadtools;
using Leadtools.Codecs;

namespace ConvertDocument
{
    internal class Program
    {
        private static bool UnlockLeadtools()
        {
            const string licenseFile = @"..\..\..\common\license\leadtools.lic";
            try
            {
                var developerKey = File.ReadAllText(licenseFile + ".key");
                RasterSupport.SetLicense(licenseFile, developerKey);

                if (RasterSupport.KernelExpired)
                {
                    Console.WriteLine("LEADTOOLS License has Expired!\nPress [Enter] to exit.");
                    Console.ReadLine();
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Console.WriteLine("\nPress [Enter] to exit.");
                Console.ReadLine();
                return false;
            }
            return true;
        }

        private static void ConvertDocumentToImage(
            string inputFile,
            string outputFile,
            RasterImageFormat outputFormat,
            int bitsPerPixel)
        {
            if (!File.Exists(inputFile))
                throw new ArgumentException($"{inputFile} not found.", nameof(inputFile));

            if (bitsPerPixel != 0 && bitsPerPixel != 1 && bitsPerPixel != 2 && bitsPerPixel != 4 &&
                bitsPerPixel != 8 && bitsPerPixel != 16 && bitsPerPixel != 24 && bitsPerPixel != 32)
                throw new ArgumentOutOfRangeException(nameof(bitsPerPixel), bitsPerPixel,
                    $"Invalid {nameof(bitsPerPixel)} value");

            using (var codecs = new RasterCodecs())
            {
                codecs.Options.RasterizeDocument.Load.XResolution = 300;
                codecs.Options.RasterizeDocument.Load.YResolution = 300;

                // indicates the start of a loop from the same source file
                codecs.StartOptimizedLoad();

                var totalPages = codecs.GetTotalPages(inputFile);
                if (totalPages > 1 && !RasterCodecs.FormatSupportsMultipageSave(outputFormat))
                    Console.WriteLine($"The {outputFormat} format does not support multiple pages.\n"
                        + "The resulting file will only contain the only last page of the document.");

                for (var pageNumber = 1; pageNumber <= totalPages; pageNumber++)
                {
                    Console.WriteLine($"Loading and saving page {pageNumber}");
                    using (var rasterImage = codecs.Load(inputFile, bitsPerPixel, CodecsLoadByteOrder.Bgr, pageNumber, pageNumber))
                        codecs.Save(rasterImage, outputFile, outputFormat, bitsPerPixel, 1, -1, 1, CodecsSavePageMode.Append);
                }

                // indicates the end of the load for the source file
                codecs.StopOptimizedLoad();
            }
        }

        private static void Main(string[] args)
        {
            if (args.Length != 2)
            {
                Console.WriteLine("Incorrect number of arguments.");
                return;
            }
            if (!UnlockLeadtools())
            {
                return;
            }
            var inputFile = args[0];
            var outputFile = args[1];
            if (!File.Exists(inputFile))
            {
                Console.WriteLine($"{inputFile} does not exist.");
                return;
            }
            ConvertDocumentToImage(inputFile, outputFile, RasterImageFormat.CcittGroup4, 1);
            System.Diagnostics.Process.Start(outputFile);
        }
    }
}
