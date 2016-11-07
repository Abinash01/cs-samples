
using Leadtools;
using Leadtools.Codecs;
using Leadtools.Forms;
using Leadtools.Forms.Ocr;
using Leadtools.ImageProcessing.Core;
using System;
using System.IO;


namespace demo_cs_detect_recognize_micr
{
   internal static class Program
   {
      private static void Main()
      {
         var inputImagePath = Path.Combine( Leadtools.Demo.Support.Path.GetResourcesPath(), @"MICR_SAMPLE.tif" );

         var unlocked = Leadtools.Demo.Support.Licensing.SetLicense();
         if (!unlocked) return;
         var micr = DetectAndRecognizeMicr( inputImagePath );
         // The default system font does not include the special MICR characters.
         // Therefore, they are displayed as ?
         // To display the characters, you have to set the font of console window itself.
         // To see the actual characters, you can break on the WriteLine() call
         // and examine the micr string to see the special chars.

         // Console.OutputEncoding = System.Text.Encoding.UTF8;
         Console.WriteLine( "MICR: {0}", micr );
      }

      private static string DetectAndRecognizeMicr( string fileName )
      {
         try
         {
            //Initialize the codecs class and load the image
            using ( var codecs = new RasterCodecs() )
            using ( var img = codecs.Load( fileName ) )
            {
               //prepare the MICR detector command and run it
               var micrDetector = new MICRCodeDetectionCommand
               {
                  SearchingZone = new LeadRect(0, 0, img.Width, img.Height)
               };
               micrDetector.Run( img );

               //See if there is a MICR detected - if not return
               if ( micrDetector.MICRZone == LeadRect.Empty )
                  return "No MICR detected in this file.";

               //if there is a MICR zone detected startup OCR
               using ( var ocrEngine = OcrEngineManager.CreateEngine( OcrEngineType.Advantage, false ) )
               {
                  ocrEngine.Startup( null, null, null, null );

                  //create the OCR Page
                  var ocrPage = ocrEngine.CreatePage( img, OcrImageSharingMode.None );

                  //Create the OCR zone for the MICR and add to the Ocr Page and recognize
                  var micrZone = new OcrZone
                  {
                     Bounds = LogicalRectangle.FromRectangle(micrDetector.MICRZone),
                     ZoneType = OcrZoneType.Micr
                  };
                  ocrPage.Zones.Add( micrZone );
                  ocrPage.Recognize( null );

                  //return the MICR text
                  return ocrPage.GetText( -1 );
               }
            }
         }
         catch ( Exception ex )
         {
            return ex.Message;
         }
      }

   }
}
