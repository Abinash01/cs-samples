using System;
using System.IO;
using Fclp;
using Leadtools;

namespace PdfMerger
{
    internal class Program
    {
        private static void UnlockSupport()
        {
            // TODO: This is using the default LEADTOOLS installation path; change as needed.
            // If you need a license file, go to https://www.leadtools.com/downloads?category=main
            const string lic = @"..\..\..\common\license\Leadtools.lic";
            var key = File.ReadAllText(@"..\..\..\\common\license\Leadtools.lic.key");
            RasterSupport.SetLicense(lic, key);
        }

        private static void ShowHelpMessage(string helpText)
        {
            Console.WriteLine(helpText);
            Environment.Exit((int) ExitCode.Success);
        }

        private static ICommandLineParserResult ParseArguments(string[] args, out ApplicationArguments arguments)
        {
            var parser = new FluentCommandLineParser<ApplicationArguments>();

            parser.Setup(arg => arg.OddPagesFilePath)
                .As('o', "odd")
                .WithDescription("PDF file that has the odd numbered pages.")
                .Required();

            parser.Setup(arg => arg.EvenPagesFilePath)
                .As('e', "even")
                .WithDescription("PDF file that has the even numbered pages.")
                .Required();

            parser.Setup(arg => arg.DestinationFilePath)
                .As('d', "dest")
                .WithDescription("Path and file name of the merged PDF file.")
                .Required();

            parser.Setup(arg => arg.OverwriteExisting)
                .As('w', "overwrite")
                .WithDescription("Overwrite, if file already exists.")
                .SetDefault(false);

            // sets up the parser to execute the callback when -? or --help is detected
            parser.SetupHelp("?", "help")
                .Callback(ShowHelpMessage);

            arguments = parser.Object;
            return parser.Parse(args);
        }

        private static void Main(string[] args)
        {
            ApplicationArguments arguments;
            var result = ParseArguments(args, out arguments);
            if (result.HasErrors)
            {
                Console.Write(result.ErrorText);
                Environment.Exit((int) ExitCode.InvalidArgument);
            }

            var odd = arguments.OddPagesFilePath;
            var even = arguments.EvenPagesFilePath;
            var destination = arguments.DestinationFilePath;
            var overwrite = arguments.OverwriteExisting;

            if (File.Exists(destination))
                if (overwrite)
                {
                    File.Delete(destination);
                }
                else
                {
                    Console.WriteLine($"Destination file already exists: {destination}");
                    Environment.Exit((int) ExitCode.FileAlreadyExists);
                }

            UnlockSupport();

            try
            {
                PdfMergeHelper.InterlaceFiles(destination, odd, even);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Environment.Exit((int) ExitCode.UnknownError);
            }
        }
    }
}