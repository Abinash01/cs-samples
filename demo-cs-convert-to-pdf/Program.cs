
using System;
using System.IO;


namespace demo_cs_convert_to_pdf
{
   internal static class Program
   {
      private static void Main()
      {
         var inputImagePath = Path.Combine( Leadtools.Demo.Support.Path.GetResourcesPath(), @"OCR1.tif" );
         var inputDocumentPath = Path.Combine( Leadtools.Demo.Support.Path.GetResourcesPath(), @"press-release.doc" );

         var unlocked = Leadtools.Demo.Support.Licensing.SetLicense();
         if (!unlocked) return;
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
