using System;
using Leadtools.Pdf;
using System.Text;

public class PdfEncrypt
{
   /// <summary>
   /// Check to see if string is ASCII
   /// </summary>
   /// <param name="s"></param>
   /// <returns>true if string is all ASCII, otherwise false</returns>
   public static bool IsStringAscii(string s) => Encoding.ASCII.GetByteCount( s ) == s.Length;

   /// <summary>
   /// Will add a user password to a pdf file
   /// </summary>
   /// <param name="fileName">The complete path of the file to be processed</param>
   /// <param name="newPassword">The user password to apply to the file</param>
   /// <param name="password">The current password of the PDF file</param>
   /// <returns>true if the PDF file is encrypted; otherwise, it is false</returns>
   public static void Encrypt( string fileName, string newPassword, string password )
   {
      if (!IsStringAscii(newPassword))
         throw new ArgumentException(nameof(newPassword), $"{nameof(newPassword)} must be ASCII only");

      var maxPasswordLength = PDFDocument.MaximumPasswordLength;
      if (newPassword.Length > maxPasswordLength )
         throw new ArgumentOutOfRangeException(nameof(newPassword), $"Maximum length of {nameof(newPassword)} is {maxPasswordLength}");

      new PDFFile( fileName, password ) {
         SecurityOptions = new PDFSecurityOptions {
            UserPassword = newPassword,
            EncryptionMode = PDFEncryptionMode.RC128Bit
         },
         CompatibilityLevel = PDFCompatibilityLevel.PDF15
      }.Convert( 1, -1, null );
   }

   /// <summary>
   /// Will add a user password to a PDF file.  
   /// </summary>
   /// <param name="fileName">The complete path of the file to be processed</param>
   /// <param name="newPassword">The user password to apply to the file</param>
   public static void Encrypt(string fileName, string newPassword)
   {
      if ( IsEncrypted( fileName ) )
         throw new InvalidOperationException($"{fileName} is already encrypted");

      Encrypt(fileName, newPassword, null);
   }

   /// <summary>
   /// Checks to see if a file has a user password.
   /// </summary>
   /// <param name="fileName">File to check</param>
   /// <returns>true if the PDF file is encrypted; otherwise, it is false</returns>
   public static bool IsEncrypted(string fileName)
   {
      var fileType = PDFFile.GetPDFFileType( fileName, true );
      switch (fileType)
      {
         case PDFFileType.Unknown:
         case PDFFileType.EncapsulatedPostscript:
         case PDFFileType.Postscript:
            Console.WriteLine( "Not a valid PDF file" );
            return false;

         case PDFFileType.PDF17:
         case PDFFileType.PDF16:
         case PDFFileType.PDF15:
         case PDFFileType.PDF14:
         case PDFFileType.PDF13:
         case PDFFileType.PDF12:
         case PDFFileType.PDF11:
         case PDFFileType.PDF10:
            return PDFFile.IsEncrypted( fileName );

         default:
            throw new ArgumentOutOfRangeException();
      }
   }


}