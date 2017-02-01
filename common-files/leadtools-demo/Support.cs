using System;

namespace Leadtools.Demo
{
   internal static class Support
   {
      private const string NewEvaluationLicenseUrl = "https://www.leadtools.com/downloads/evaluation-form?evallicenseonly=true";

      internal static class Licensing
      {
         /* TODO: Change this to use your license file and developer key */
         /* Do not distribute your developer key in a file like this.  This is just for demo purposes. */
         private const string LicenseFilePath = @"C:\LEADTOOLS 19\Common\License\leadtools.lic";
         private const string DeveloperKeyPath = @"C:\LEADTOOLS 19\Common\License\leadtools.lic.key";

         public static bool SetLicense( bool silent = false)
         {
            try
            {
               var developerKey = System.IO.File.ReadAllText( DeveloperKeyPath );
               RasterSupport.SetLicense( LicenseFilePath, developerKey );
            }
            catch ( Exception ex )
            {
               System.Diagnostics.Debug.Write( ex.Message );
            }

            if ( RasterSupport.KernelExpired )
            {
               var dir = System.IO.Path.GetDirectoryName( System.Reflection.Assembly.GetExecutingAssembly().Location );
               /* Try the common LIC directory */
               if (dir != null)
               {
                  var licenseFileRelativePath = System.IO.Path.Combine( dir, Path.RelativePathToRoot, "common-files\\leadtools-license\\eval-license-files.LIC" );
                  var keyFileRelativePath = System.IO.Path.Combine( dir, Path.RelativePathToRoot, "common-files\\leadtools-license\\eval-license-files.LIC.key" );

                  if ( System.IO.File.Exists( licenseFileRelativePath ) && System.IO.File.Exists( keyFileRelativePath ) )
                  {
                     var developerKey = System.IO.File.ReadAllText( keyFileRelativePath );
                     try
                     {
                        RasterSupport.SetLicense( licenseFileRelativePath, developerKey );
                     }
                     catch ( Exception ex )
                     {
                        System.Diagnostics.Debug.Write( ex.Message );
                     }
                  }
               }
            }

            if (!RasterSupport.KernelExpired) return true;
            if (silent) return false;
            const string msg = "Your license file is missing, invalid, or expired. LEADTOOLS will not function.\nPlease contact sales@leadtools.com for information on obtaining a valid license.";
            var logmsg = string.Format( "*** NOTE: {0} ***{1}", msg, Environment.NewLine );
            System.Diagnostics.Debugger.Log( 0, null, "*******************************************************************************" + Environment.NewLine );
            System.Diagnostics.Debugger.Log( 0, null, logmsg );
            System.Diagnostics.Debugger.Log( 0, null, "*******************************************************************************" + Environment.NewLine );

            Console.WriteLine( msg );
            Path.ShellExecute( NewEvaluationLicenseUrl );
            return false;
         }
      }

      internal static class Path
      {
         public const string RelativePathToRoot = "..\\..\\..\\Examples\\Git";
         private const string ResourcesFilesFolder = "common-files\\resources";

         public static string GetExecutingLocation()
         {
            return System.IO.Path.GetDirectoryName( System.Reflection.Assembly.GetExecutingAssembly().Location );
         }

         public static string GetResourcesPath()
         {
            return System.IO.Path.Combine( GetExecutingLocation(), RelativePathToRoot, ResourcesFilesFolder );
         }

         public static string GetOutputPath( bool create = true )
         {
            string outputPath = System.IO.Path.Combine( GetExecutingLocation(), "output\\" );
            if ( create && !System.IO.Directory.Exists( outputPath ) )
            {
               System.IO.Directory.CreateDirectory( outputPath );
            }
            return outputPath;
         }

         public static void OpenExplorer( string path )
         {
            ShellExecute( "explorer", path );
         }

         public static void ShellExecute( string filename, string arguments = "" )
         {
            System.Diagnostics.Process.Start( filename, string.Format( "\"{0}\"", arguments ) );
         }
      }

   }
}
