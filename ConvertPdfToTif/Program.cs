using Leadtools;
using Leadtools.Codecs;
using System;
using System.IO;

namespace ConvertPdfToTif
{
   internal static class Program
   {
      private static void Main()
      {
         const string filePath = "C:\\LEADTOOLS 19\\Examples\\DotNet\\GitHub\\ConvertPdfToTif\\Files\\fax.pdf";
         if ( SetLicense() )
            ConvertPdfToTif( filePath );
      }

      private static bool SetLicense()
      {
         const string licenseFile = "..\\..\\..\\common\\license\\leadtools.lic";
         try
         {
            var developerKey = File.ReadAllText( licenseFile + ".key" );
            RasterSupport.SetLicense( licenseFile, developerKey );

            if ( RasterSupport.KernelExpired )
            {
               Console.WriteLine( "Kernel Expired!\nPress [Enter] to exit." );
               Console.ReadLine();
            }
         }
         catch ( Exception ex )
         {
            Console.WriteLine( ex.ToString() );
            Console.WriteLine( "\nPress [Enter] to exit." );
            Console.ReadLine();
            return false;
         }
         return true;
      }

      private static void ConvertPdfToTif( string fileName )
      {
         Func<int, string> getSaveFileName = pg => Path.GetDirectoryName( fileName )
                     + Path.DirectorySeparatorChar
                     + Path.GetFileNameWithoutExtension( fileName )
                     + "_pg"
                     + pg.ToString()
                     + ".tif";
         try
         {
            using ( var codecs = new RasterCodecs() )
            {
               var info = codecs.GetInformation( fileName, true );
               for ( var pageNumber = 1; pageNumber <= info.TotalPages; pageNumber++ )
               {
                  // Make sure the resulting img has the original resolution
                  var pdfInfo = codecs.GetRasterPdfInfo( fileName, pageNumber );
                  codecs.Options.RasterizeDocument.Load.XResolution = pdfInfo.XResolution;
                  codecs.Options.RasterizeDocument.Load.YResolution = pdfInfo.YResolution;

                  // Save the file using a format appropriate for the bits per pixel
                  var bpp = pdfInfo.BitsPerPixel;
                  var saveFormat = RasterImageFormat.Tif;

                  if ( bpp == 1 )
                     saveFormat = RasterImageFormat.CcittGroup4;
                  else if ( bpp > 1 && bpp < 9 )
                     saveFormat = RasterImageFormat.TifLzw;
                  else if ( bpp == 24 )
                     saveFormat = RasterImageFormat.TifJpeg;

                  using ( var page = codecs.Load( fileName, pageNumber ) )
                     codecs.Save( page, getSaveFileName( pageNumber ), saveFormat, 0 );
               }
            }
            Console.WriteLine( "Done" );
         }
         catch ( Exception ex )
         {
            Console.WriteLine( ex.ToString() );
         }
         finally
         {
            Console.ReadLine();
         }
      }
   }
}
