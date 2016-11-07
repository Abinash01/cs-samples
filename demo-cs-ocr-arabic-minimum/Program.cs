using System;
using Leadtools.Codecs;
using Leadtools.Forms.Ocr;
using Leadtools.Forms.DocumentWriters;
using System.IO;
namespace Minimum_OCR_Console_19
{
   internal static class Program
   {
      private static void Main(string[] args)
      {
         const string filenameFilter = "*arabic.tif";
         try
         {
            // Set license to unlock support
            Leadtools.Demo.Support.Licensing.SetLicense();
            string sourceFolder;
            string targetFolder;
            if ( args.Length == 2 )
            {
               sourceFolder = args[0];
               targetFolder = args[1];
            }
            else
            {
               sourceFolder = Leadtools.Demo.Support.Path.GetResourcesPath();
               targetFolder = Leadtools.Demo.Support.Path.GetOutputPath();
            }

            using ( var codecs = new RasterCodecs() )
            using ( var ocrEngine = OcrEngineManager.CreateEngine( OcrEngineType.Arabic, false ) )
            {
               ocrEngine.Startup( null, null, null, null );
               using ( var ocrDocument = ocrEngine.DocumentManager.CreateDocument() )
               {
                  if (!Directory.Exists(sourceFolder)) return;
                  var files = Directory.GetFiles( sourceFolder, filenameFilter, SearchOption.TopDirectoryOnly );
                  foreach ( var file in files )
                  {
                     Console.WriteLine( "Loading " + file );
                     var targetFileName = Path.Combine( targetFolder, Path.GetFileNameWithoutExtension( file ) + ".txt" );
                     using ( var image = codecs.Load( file ) )
                     {
                        if ( image.PageCount == 1 )
                        {
                           ocrDocument.Pages.AddPage( image, null );
                           Console.WriteLine( "Starting OCR" );
                           ocrDocument.Pages[0].Recognize( null );
                           ocrDocument.Save( targetFileName, DocumentFormat.Text, null );
                        }
                        else
                        {
                           ocrDocument.Pages.AddPages( image, 1, image.PageCount, null );
                           Console.WriteLine( "Starting OCR" );
                           foreach ( var page in ocrDocument.Pages )
                           {
                              page.Recognize( null );
                              ocrDocument.Save( targetFileName, DocumentFormat.Text, null );
                           }
                        }
                        ocrDocument.Pages.Clear();
                        Console.WriteLine( "Finished OCR\r\nSaved " + targetFileName );
                     }
                  }
               }
            }
         }

         catch ( Exception ex )
         {
            Console.WriteLine( ex.Message );
         }
         finally
         {
            Console.WriteLine( "Press ENTER to Exit" );
            Console.ReadLine();
         }

      }
   }
}
