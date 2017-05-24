using Leadtools;

namespace Dithering
{
   internal static class Palette
   {
      //public string PaletteName { get; }

      //public RasterColor[] Colors { get; }

      //public enum PreDefinedPalette
      //{
      //   BasicColors,
      //   Cmyk, 
      //   RubiksCube,
      //   TriadicColor,
      //   WoodStain,
      //   UserPalette,
      //}

      //public Palette(PreDefinedPalette palette)
      //{
      //   PaletteName = palette.ToString();

      //   switch (palette)
      //   {
      //      case PreDefinedPalette.BasicColors:
      //         Colors = BasicColorPalette;
      //         break;
      //      case PreDefinedPalette.Cmyk:
      //         Colors = CmykPalette;
      //         break;
      //      case PreDefinedPalette.RubiksCube:
      //         Colors = RubiksCubePalette;
      //         break;
      //      case PreDefinedPalette.TriadicColor:
      //         Colors = TriadicColorPalette;
      //         break;
      //      case PreDefinedPalette.WoodStain:
      //         Colors = WoodStainPalette;
      //         break;
      //      case PreDefinedPalette.UserPalette:
      //         Colors = null;
      //         break;
      //      default:
      //         throw new ArgumentOutOfRangeException(nameof(palette), palette, null);
      //   }

      //}


      public static readonly RasterColor[] BasicColorPalette =
      {
         new RasterColor(255, 0, 255),
         new RasterColor(255, 255, 0),
         new RasterColor(0, 255, 255),
         new RasterColor(0, 0, 0),
         new RasterColor(255, 255, 255),
         new RasterColor(255, 0, 0),
         new RasterColor(0, 255, 0),
         new RasterColor(0, 0, 255)
      };

      public static readonly RasterColor[] CmykPalette =
      {
         new RasterColor(0, 255, 255), //System.Drawing.Color.Cyan
         new RasterColor(255, 255, 0), //System.Drawing.Color.Yellow
         new RasterColor(255, 0, 255), //System.Drawing.Color.Magenta
         new RasterColor(0, 0, 0),
         new RasterColor(255, 255, 255)
      };

      public static readonly RasterColor[] RubiksCubePalette =
      {
         new RasterColor(255, 255, 255),
         new RasterColor(140, 0, 15),
         new RasterColor(0, 115, 47),
         new RasterColor(0, 51, 115),
         new RasterColor(255, 210, 0),
         new RasterColor(255, 70, 0)
      };

      public static readonly RasterColor[] TriadicColorPalette =
      {
         new RasterColor(85, 21, 21), // red
         new RasterColor(21, 85, 21), // green
         new RasterColor(21, 21, 85), // blue

         new RasterColor(170, 21, 21), // red
         new RasterColor(21, 170, 21), // green
         new RasterColor(21, 21, 170), // blue

         new RasterColor(255, 21, 21), // red
         new RasterColor(21, 255, 21), // green
         new RasterColor(21, 21, 255), // blue

         new RasterColor(0, 85, 85),
         new RasterColor(85, 0, 85),
         new RasterColor(85, 85, 0),

         new RasterColor(0, 170, 170),
         new RasterColor(170, 0, 170),
         new RasterColor(170, 170, 0),

         new RasterColor(0, 0, 0),
         new RasterColor(255, 255, 255)
      };

      public static readonly RasterColor[] WoodStainPalette =
      {
         new RasterColor(190, 158, 121), //sunbleached
         new RasterColor(163, 113, 67), // paprika
         new RasterColor(200, 143, 80), // summary oak
         new RasterColor(93, 52, 33), //light walnut
         new RasterColor(137, 66, 33), // traditional cherry
         new RasterColor(116, 53, 52), // cabernet
         new RasterColor(87, 54, 19), //dark walnut
         new RasterColor(3, 2, 0) //ebony
      };
   }
}