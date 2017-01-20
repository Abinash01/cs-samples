using System.Diagnostics;
using Microsoft.Win32;

namespace ContextMenu
{
   /// <summary>
   ///    Register and unregister shell context menus.
   /// </summary>
   internal static class FileShellExtension
   {
      private static string GetRegistryPath(string fileType, string shellKeyName)
      {
         var classesRootKey = Registry.ClassesRoot;
         using (var subKey = classesRootKey.OpenSubKey(fileType))
         {
            Debug.Assert(subKey != null, "subKey != null");
            var regSubKeyValue = subKey.GetValue(null).ToString();
            return $@"{regSubKeyValue}\shell\{shellKeyName}";
         }
      }

      /// <summary>
      ///    Register shell context menu.
      /// </summary>
      /// <param name="fileType">The file type to register.</param>
      /// <param name="shellKeyName">Name that appears in the registry.</param>
      /// <param name="menuText">Text that appears in the context menu.</param>
      /// <param name="menuCommand">Command line that is executed.</param>
      /// <param name="iconPath">Icon to be displayed</param>
      public static void Register(string fileType, string shellKeyName, string menuText, string menuCommand,
         string iconPath)
      {
         var regPath = GetRegistryPath(fileType, shellKeyName);
         // add context menu to the registry
         using (var key = Registry.ClassesRoot.CreateSubKey(regPath))
         {
            Debug.Assert(key != null, "key != null");
            key.SetValue(null, menuText);
            key.SetValue("Icon", iconPath, RegistryValueKind.String);
         }

         // add command that is invoked to the registry
         using (var key = Registry.ClassesRoot.CreateSubKey($@"{regPath}\command"))
         {
            Debug.Assert(key != null, "key != null");
            key.SetValue(null, menuCommand);
         }
      }

      /// <summary>
      ///    Unregister shell context menu.
      /// </summary>
      /// <param name="fileType">The file type to unregister.</param>
      /// <param name="shellKeyName">Name that was registered in the registry.</param>
      public static void Unregister(string fileType, string shellKeyName)
         => Registry.ClassesRoot.DeleteSubKeyTree(GetRegistryPath(fileType, shellKeyName));
   }
}