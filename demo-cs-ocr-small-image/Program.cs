using System;
using Leadtools.Codecs;
using Leadtools.Forms.Ocr;
using System.IO;
using Leadtools;
using Leadtools.ImageProcessing;
using Leadtools.ImageProcessing.Core;

namespace demo_cs_ocr_small_image
{
   internal static class Program
   {
      private static void Main()
      {
         if ( Leadtools.Demo.Support.Licensing.SetLicense() == false )
            return;
         var file = Path.Combine( Leadtools.Demo.Support.Path.GetExecutingLocation(), "small-low-res-img.png" );
         var results = PreProcessAndRecognizeSmallImage( file );
         Console.Write(results);
      }

      private static string PreProcessAndRecognizeSmallImage( string filename )
      {
         //initialize the codes
         using ( var codecs = new RasterCodecs() )
         using ( var img = codecs.Load( filename ) )
         {
            ChangeImageResolution( img, 300 );
            //Convert to Black and white with the AutoBinarizeCommand
            new AutoBinarizeCommand().Run( img );

            //Convert to 1BPP
            new ColorResolutionCommand { BitsPerPixel = 1 }.Run( img );

            //run the OCR process on the processed image and extract the text
            using ( var ocrEngine = OcrEngineManager.CreateEngine( OcrEngineType.Advantage, false ) )
            {
               ocrEngine.Startup( null, null, null, null );
               using ( var ocrPage = ocrEngine.CreatePage( img, OcrImageSharingMode.None ) )
               {
                  ocrPage.Recognize( null );
                  return ocrPage.GetText( -1 );
               }
            }
         }
      }

      private static void ChangeImageResolution( RasterImage image, int resolution )
      {
         // Rescale the image to 300x300 DPI with Bicubic interpolation
         var newWidth = ( image.Width / (double) image.XResolution ) * resolution;
         var newHeight = ( image.Height / (double) image.YResolution ) * resolution;
         new SizeCommand( (int) newWidth, (int) newHeight, RasterSizeFlags.Bicubic ).Run( image );
         image.XResolution = resolution;
         image.YResolution = resolution;
      }


   }
}
