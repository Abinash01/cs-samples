using Leadtools;
using Leadtools.Codecs;
using Leadtools.Forms.DocumentWriters;
using Leadtools.Forms.Ocr;
using Leadtools.ImageProcessing.Core;
using System;
using System.IO;

namespace demo_cs_convert_to_pdf.Convert
{
   internal static class ToPdf
   {
      public static void ExportPdfImage( string filename )
      {
         var outputFile = Path.Combine( Leadtools.Demo.Support.Path.GetOutputPath(), Path.GetFileNameWithoutExtension( filename ) + "_Image.pdf" );

         // Create instance of RasterCodecs object to load and save the file
         // https://www.leadtools.com/help/leadtools/v19/dh/co/leadtools.codecs~leadtools.codecs.rastercodecs.html
         using ( var codecs = new RasterCodecs() )
         {
            // We want to load all the pages of the file
            codecs.Options.Load.AllPages = true;

            // Load the image
            // https://www.leadtools.com/help/leadtools/v19/dh/l/leadtools~leadtools.rasterimage.html
            using ( var image = codecs.Load( filename ) )
            {
               if ( image.BitsPerPixel != 1 )
               {
                  // Use the AutoBinarize Command to convert the image to 1bpp (black and white)
                  // https://www.leadtools.com/help/leadtools/v19/dh/po/leadtools.imageprocessing.core~leadtools.imageprocessing.core.autobinarizecommand.html
                  new AutoBinarizeCommand().Run( image );
               }
               // Save the image out as a 1BPP CCITT Group 4 compressed PDF
               // https://www.leadtools.com/help/leadtools/v19/dh/co/leadtools.codecs~leadtools.codecs.rastercodecs~save.html
               codecs.Save( image, outputFile, RasterImageFormat.RasPdfG4, 1 );
            }
         }
      }

      public static void ExportPdfDocument( string filename )
      {
         var outputFile = Path.Combine( Leadtools.Demo.Support.Path.GetOutputPath(), Path.GetFileNameWithoutExtension( filename ) + "_Document.pdf" );

         // Create an instance of the OCR Engine
         // https://www.leadtools.com/help/leadtools/v19/dh/fo/leadtools.forms.ocr~leadtools.forms.ocr.iocrengine.html
         using ( var ocrEngine = OcrEngineManager.CreateEngine( OcrEngineType.Advantage, false ) )
         {
            // Startup the engine using default parameters
            // https://www.leadtools.com/help/leadtools/v19/dh/fo/leadtools.forms.ocr~leadtools.forms.ocr.iocrengine~startup.html
            ocrEngine.Startup( null, null, null, null );
            
            // Run the Auto Recognize Manager to preprocess the image and save it out as a Document based PDF
            // https://www.leadtools.com/help/leadtools/v19/dh/fo/leadtools.forms.ocr~leadtools.forms.ocr.iocrautorecognizemanager.html
            ocrEngine.AutoRecognizeManager.PreprocessPageCommands.Add( OcrAutoPreprocessPageCommand.All );
            ocrEngine.AutoRecognizeManager.Run( filename, outputFile, DocumentFormat.Pdf, null, null );
         }
      }

      public static void ExportPdfViaSvg( string filename )
      {
         var outputFile = Path.Combine( Leadtools.Demo.Support.Path.GetOutputPath(), Path.GetFileNameWithoutExtension( filename ) + "_Svg.pdf" );

         // Setup a new RasterCodecs object
         using ( var codecs = new RasterCodecs() )
         {
            // Check to see if we can use this method
            // https://www.leadtools.com/help/leadtools/v19/dh/co/leadtools.codecs~leadtools.codecs.rastercodecs~canloadsvg(string).html
            if (!codecs.CanLoadSvg(filename))
            {
               Console.WriteLine( "\nCannot load this file as SVG for conversion to PDF.\nSee: https://www.leadtools.com/help/leadtools/v19/dh/co/leadtools.codecs~leadtools.codecs.rastercodecs~canloadsvg(string).html#remarksSectionHeading" );
               return;
            }

            codecs.Options.RasterizeDocument.Load.Resolution = 300;

            // Get the number of pages in the input document
            var pageCount = codecs.GetTotalPages( filename );

            // Create a new instance of the LEADTOOLS Document Writer
            var docWriter = new DocumentWriter();

            // Set the PDF options
            // https://www.leadtools.com/help/leadtools/v19/dh/ft/leadtools.forms.documentwriters~leadtools.forms.documentwriters.pdfdocumentoptions.html
            var pdfOptions = docWriter.GetOptions( DocumentFormat.Pdf ) as PdfDocumentOptions;
            if (pdfOptions != null)
            {
               pdfOptions.DocumentType = PdfDocumentType.Pdf;
               docWriter.SetOptions( DocumentFormat.Pdf, pdfOptions );
            }

            // Create a new PDF document
            docWriter.BeginDocument( outputFile, DocumentFormat.Pdf );

            var message = "";
            // Loop through all the pages
            for ( var pageNumber = 1; pageNumber <= pageCount; pageNumber++ )
            {
               // Get the page as SVG
               // https://www.leadtools.com/help/leadtools/v19/dh/co/leadtools.codecs~leadtools.codecs.rastercodecs~loadsvg.html
               var page = new DocumentSvgPage();
               var lastMessageLen = message.Length;
               
               message = string.Format("\rConverting page {0} of {1}", pageNumber, pageCount);
               var diff = lastMessageLen - message.Length;
               Console.Write( "{0}{1}", message, ( diff > 0 ? new string( ' ', diff ) : "" ) );
               using ( page.SvgDocument = codecs.LoadSvg( filename, pageNumber, null ) )
               {
                  // Add the page
                  docWriter.AddPage( page );
               }
            }
            Console.Write( "\r{0}\r", new String( ' ', message.Length ) );
            // Finally, finish writing the PDF file on disk
            docWriter.EndDocument();
         }
      }

   }
}
