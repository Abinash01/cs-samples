
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace demo_cs_convert_to_pdf
{
   class Program
   {
      static void Main( string[] args )
      {
         string inputImagePath = Path.Combine( Leadtools.Demo.Support.Path.GetResourcesPath(), @"OCR1.tif" );
         string inputDocumentPath = Path.Combine( Leadtools.Demo.Support.Path.GetResourcesPath(), @"press-release.doc" );

         bool unlocked = Leadtools.Demo.Support.Licensing.SetLicense();
         if ( unlocked )
         {
            Console.WriteLine( "Converting {0}", inputImagePath );
            Console.Write( "Converting to image-based PDF.   " );
            Convert.ToPdf.ExportPdfImage( inputImagePath );
            Console.WriteLine( "Done..." );

            Console.Write( "Converting to document-based PDF.   " );
            Convert.ToPdf.ExportPdfDocument( inputImagePath );
            Console.WriteLine( "Done..." );

            Console.WriteLine( "Converting {0}", inputDocumentPath );
            Console.WriteLine( "Converting to PDF via SVG.   " );
            Convert.ToPdf.ExportPdfViaSvg( inputDocumentPath );
            Console.WriteLine( "Done..." );

            Leadtools.Demo.Support.Path.OpenExplorer( Leadtools.Demo.Support.Path.GetOutputPath() );
         }
      }
   }
}
