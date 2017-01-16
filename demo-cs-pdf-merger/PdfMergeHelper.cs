using System.Collections.Generic;
using System.IO;
using System.Linq;
using Leadtools.Pdf;

namespace PdfMerger
{
    internal static class PdfMergeHelper
    {
        private static string[] ExtractFiles(string sourceOddFileName, string sourceEvenFileName)
        {
            var oddFile = new PDFFile(sourceOddFileName);
            var oddPageCount = oddFile.GetPageCount();

            var evenFile = new PDFFile(sourceEvenFileName);
            var evenPageCount = evenFile.GetPageCount();

            var filenames = new string[oddPageCount+evenPageCount];

            // Extract each page to an individual PDF file
            for (var i = 1; i <= oddPageCount; i++)
            {
                var pageNum = (i * 2) - 1;
                filenames[pageNum-1] = $"temp-page-{pageNum}.pdf";
                oddFile.ExtractPages(i, i, filenames[pageNum - 1]);
            }
            for (var i = 1; i <= evenPageCount; i++)
            {
                var pageNum = (i * 2);
                filenames[pageNum - 1] = $"temp-page-{pageNum}.pdf";
                evenFile.ExtractPages(i, i, filenames[pageNum - 1]);
            }

            return filenames;
        }

        private static void MergeFiles(IReadOnlyList<string> sourceFileNames, string destinationFileName)
        {
            var pdfFile = new PDFFile(sourceFileNames?.First());
            pdfFile.MergeWith(sourceFileNames?.Skip(1).ToArray(), destinationFileName);
        }

        private static void CleanUp(IEnumerable<string> fileNames)
        {
            foreach (var fileName in fileNames)
            {
                if(File.Exists(fileName))
                    File.Delete(fileName);
            }
        }

        public static void InterlaceFiles(string mergedPagesFileName, string oddPagesFileName, string evenPagesFileName)
        {
            string[] allPages = { };
            try
            {
                allPages = ExtractFiles(oddPagesFileName, evenPagesFileName);
                MergeFiles(allPages, mergedPagesFileName);
            }
            finally
            {
                CleanUp(allPages);
            }
        }
    }
}
