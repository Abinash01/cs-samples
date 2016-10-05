using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Leadtools;
using Leadtools.Codecs;
using Leadtools.Forms;
using Leadtools.Forms.Ocr;
using Leadtools.Forms.DocumentWriters;
using System.IO;
namespace Minimum_OCR_Console_19
{
   class Program
   {
      static void Main(string[] args)
      {
         const string filenameFilter = "*arabic.tif";
         try
         {
            // Set license to unlock support
            Leadtools.Demo.Support.Licensing.SetLicense();
            string sourceFolder = null;
            string targetFolder = null;
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
            using ( IOcrEngine ocrEngine = OcrEngineManager.CreateEngine( OcrEngineType.Arabic, false ) )
            {
               ocrEngine.Startup( null, null, null, null );
               using ( IOcrDocument ocrDocument = ocrEngine.DocumentManager.CreateDocument() )
               {
                  if ( System.IO.Directory.Exists( sourceFolder ) )
                  {
                     string[] files = Directory.GetFiles( sourceFolder, filenameFilter, SearchOption.TopDirectoryOnly );
                     foreach ( string file in files )
                     {
                        Console.WriteLine( "Loading " + file );
                        string TargetFileName = Path.Combine( targetFolder, Path.GetFileNameWithoutExtension( file ) + ".txt" );
                        using ( RasterImage image = codecs.Load( file ) )
                        {
                           if ( image.PageCount == 1 )
                           {
                              ocrDocument.Pages.AddPage( image, null );
                              Console.WriteLine( "Starting OCR" );
                              ocrDocument.Pages[0].Recognize( null );
                              ocrDocument.Save( TargetFileName, DocumentFormat.Text, null );
                           }
                           else
                           {
                              ocrDocument.Pages.AddPages( image, 1, image.PageCount, null );
                              Console.WriteLine( "Starting OCR" );
                              foreach ( Leadtools.Forms.Ocr.IOcrPage page in ocrDocument.Pages )
                              {
                                 page.Recognize( null );
                                 ocrDocument.Save( TargetFileName, DocumentFormat.Text, null );
                              }
                           }
                           ocrDocument.Pages.Clear();
                           Console.WriteLine( "Finished OCR\r\nSaved " + TargetFileName );
                        }
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
