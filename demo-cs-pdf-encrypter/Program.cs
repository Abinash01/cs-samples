using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Windows.Forms;
using ContextMenu;
using Leadtools;

[assembly: CLSCompliant( true )]
internal class Program
{
   // for demo purposes, we'll register with all files, and let the program handle incompatibilities
   private static readonly string[] FileTypes = { ".pdf" };

   // text to appear
   private const string MenuText = "LEADTOOLS PDF Encrypt";

   // menu icon
   private const string IconFilename = "avatar.ico";

   private static void UnlockSupport()
   {
      // TODO: If you need a license file, go to https://www.leadtools.com/downloads?category=main
      var lic = Path.Combine( Path.GetDirectoryName( Application.ExecutablePath ), @"Leadtools.lic" );

      var key = File.ReadAllText( Path.Combine( Path.GetDirectoryName( Application.ExecutablePath ), @"Leadtools.lic.key" ) );
      RasterSupport.SetLicense( lic, key );
   }

   [STAThread]
   private static void Main( string[] args )
   {
      // determine if arg is explicitly register or unregister option
      if ( !HandleInput( args ) )
         // invoked from shell, args[0] is the selected file
         HandleContextMenuSelection( args[0] );
   }

   private static bool HandleInput( IList<string> args )
   {
      // extract this program's guid to use as the registry key for insertion/extraction
      var assembly = Assembly.GetExecutingAssembly();
      var guidAttribute = (GuidAttribute) assembly.GetCustomAttributes( typeof( GuidAttribute ), true )[0];

      var handled = false;
      var message = "";

      // register
      if ( args.Count == 0 || string.Compare( args[0], "-register", StringComparison.OrdinalIgnoreCase ) == 0 )
      {
         // keyword detected, do not process as image
         handled = true;

         // elevation is required for registry access
         if ( !IsElevated() )
            message = "Registering requires administrator rights.  Run as administrator and try again.";
         else
         {
            // full path to self, %L is placeholder for selected file
            var menuCommand = $"\"{Application.ExecutablePath}\" \"%L\"";

            // the icon gets copied to the same output directory, so find it there
            var iconPath = Path.Combine( Path.GetDirectoryName( Application.ExecutablePath ), IconFilename );

            // register the context menu for each file type
            try
            {
               foreach ( var t in FileTypes )
                  FileShellExtension.Register( t, guidAttribute.Value, MenuText, menuCommand, iconPath );

               message = $"Registration successful.  \"{MenuText}\" will appear as an option on the context menu.";
            }
            catch ( ArgumentException )
            {
               message = "Unable to modify registry.  No operation performed.";
            }
            catch ( AccessViolationException )
            {
               message = "Insufficient permission to modify registry.  Check privileges and policy.";
            }
            catch ( Exception ex )
            {
               message = ex.Message;
            }
         }
      }
      // unregister		
      else if ( string.Compare( args[0], "-unregister", StringComparison.OrdinalIgnoreCase ) == 0 )
      {
         handled = true;

         // elevation is required for registry access
         if ( IsElevated() == false )
            message = "Unregistering requires administrator rights.";
         else
         {
            try
            {
               // unregister the context menu for each file type
               foreach ( var t in FileTypes )
                  FileShellExtension.Unregister( t, guidAttribute.Value );

               message = "Successfully unregistered.  Context menu entry removed.";
            }
            catch ( ArgumentException )
            {
               message = "Unable to modify registry.  No operation performed.";
            }
            catch ( AccessViolationException )
            {
               message = "Insufficient permission to modify registry.  Check privileges and policy.";
            }
            catch ( Exception ex )
            {
               message = ex.Message;
            }
         }
      }

      if ( !string.IsNullOrEmpty( message ) )
         MessageBox.Show( message );

      // command line did not contain an action
      return handled;
   }

   /// <summary>
   /// Determine if built-in role is running as administrator
   /// </summary>
   /// <returns>True if program has admin rights, false otherwise</returns>
   private static bool IsElevated() => new WindowsPrincipal( WindowsIdentity.GetCurrent() ).IsInRole( WindowsBuiltInRole.Administrator );

   private static string PromptForNewPassword()
   {
      bool passwordsMatch;
      string newPassword;
      do
      {
         Console.Write("Enter new password: ");
         newPassword = Console.ReadLine();
         Console.Write("Enter new password again: ");
         var newPasswordAgain = Console.ReadLine();
         passwordsMatch = newPassword == newPasswordAgain;
         if (!passwordsMatch)
            Console.WriteLine("Password did not match. Please try again.");
         
      } while (!passwordsMatch);
      return newPassword;
   }

   /// <summary>
   /// Pass the file to the LEADTOOLS converter
   /// </summary>
   /// <param name="filePath">Full path to the image to convert</param>
   private static void HandleContextMenuSelection( string filePath )
   {
      try
      {
         UnlockSupport();
         Console.WriteLine( $"Processing \"{Path.GetFileName( filePath )}\"" );
         string password = null;
         if ( PdfEncrypt.IsEncrypted( filePath ) )
         {
            Console.Write( "Enter current password: " );
            password = Console.ReadLine();
         }
         var newPassword = PromptForNewPassword();
         
         PdfEncrypt.Encrypt( filePath, newPassword, password );
         Console.WriteLine(string.IsNullOrEmpty(newPassword)
            ? $"{filePath} has no password and is NOT encrypted"
            : $"{filePath} is encrypted with the new password");
      }
      catch ( Exception ex )
      {
         MessageBox.Show( $"An error occurred: {ex}" );
      }
   }
}