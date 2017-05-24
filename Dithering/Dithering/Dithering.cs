using System;
using Leadtools;

namespace Dithering
{
   public static class Dithering
   {
      public static readonly ErrorDiffusionMatrix NoDiffusionMatrix = new ErrorDiffusionMatrix
      {
         Denominator = 1
      };

      public static readonly ErrorDiffusionMatrix AtkinsonMatrix = new ErrorDiffusionMatrix
      {
         //  - # 1 1     where # = pixel being processed, - = previously processed pixel
         //  1 1 1       and pixel difference is distributed to neighbor pixels
         //    1  
         // (n/8)
         Denominator = 8,
         Cells = new[]
         {
            new ErrorDiffusionMatrix.Cell {XOffset = 1, YOffset = 0, Numerator = 1},
            new ErrorDiffusionMatrix.Cell {XOffset = 2, YOffset = 0, Numerator = 1},
            new ErrorDiffusionMatrix.Cell {XOffset = -1, YOffset = 1, Numerator = 1},
            new ErrorDiffusionMatrix.Cell {XOffset = 0, YOffset = 1, Numerator = 1},
            new ErrorDiffusionMatrix.Cell {XOffset = 1, YOffset = 1, Numerator = 1},
            new ErrorDiffusionMatrix.Cell {XOffset = 0, YOffset = 2, Numerator = 1}
         }
      };

      public static readonly ErrorDiffusionMatrix FloydSteinbergMatrix = new ErrorDiffusionMatrix
      {
         //  - # 7    where *=pixel being processed, -=previously processed pixel
         //  3 5 1    and pixel difference is distributed to neighbor pixels
         // (n/16)
         Denominator = 16,
         Cells = new[]
         {
            new ErrorDiffusionMatrix.Cell {XOffset = 1, YOffset = 0, Numerator = 7},
            new ErrorDiffusionMatrix.Cell {XOffset = -1, YOffset = 1, Numerator = 3},
            new ErrorDiffusionMatrix.Cell {XOffset = 0, YOffset = 1, Numerator = 5},
            new ErrorDiffusionMatrix.Cell {XOffset = 1, YOffset = 1, Numerator = 1}
         }
      };

      public static readonly ErrorDiffusionMatrix StuckiMatrix = new ErrorDiffusionMatrix
      {
         //  - - # 7 5    where *=pixel being processed, -=previously processed pixel
         //  2 4 8 4 2  and pixel difference is distributed to neighbor pixels
         //  1 2 4 2 1
         // (n/42)
         Denominator = 42,
         Cells = new[]
         {
            new ErrorDiffusionMatrix.Cell {XOffset = 1, YOffset = 0, Numerator = 7},
            new ErrorDiffusionMatrix.Cell {XOffset = 2, YOffset = 0, Numerator = 5},

            new ErrorDiffusionMatrix.Cell {XOffset = -2, YOffset = 1, Numerator = 2},
            new ErrorDiffusionMatrix.Cell {XOffset = -1, YOffset = 1, Numerator = 4},
            new ErrorDiffusionMatrix.Cell {XOffset = 0, YOffset = 1, Numerator = 8},
            new ErrorDiffusionMatrix.Cell {XOffset = 1, YOffset = 1, Numerator = 4},
            new ErrorDiffusionMatrix.Cell {XOffset = 2, YOffset = 1, Numerator = 2},

            new ErrorDiffusionMatrix.Cell {XOffset = -2, YOffset = 2, Numerator = 1},
            new ErrorDiffusionMatrix.Cell {XOffset = -1, YOffset = 2, Numerator = 2},
            new ErrorDiffusionMatrix.Cell {XOffset = 0, YOffset = 2, Numerator = 4},
            new ErrorDiffusionMatrix.Cell {XOffset = 1, YOffset = 2, Numerator = 2},
            new ErrorDiffusionMatrix.Cell {XOffset = 2, YOffset = 2, Numerator = 1}
         }
      };

      private static int CalculateEuclideanDistance(this RasterColor c1, RasterColor c2)
      {
         // https://en.wikipedia.org/wiki/Color_difference
         int dR = c1.R - c2.R,
             dG = c1.G - c2.G,
             dB = c1.B - c2.B;
         return dR * dR + dG * dG + dB * dB;
      }

      private static double CalculateRelativeLuminance601(this RasterColor c1, RasterColor c2)
      {
         // https://en.wikipedia.org/wiki/Relative_luminance
         // https://en.wikipedia.org/wiki/Luma_(video)
         double y1 = (c1.R * 299 + c1.G * 587 + c1.B * 114) / (255d * 1000),
                y2 = (c2.R * 299 + c2.G * 587 + c2.B * 114) / (255d * 1000);

         double dLuma = y1 - y2;

         double dR = (c1.R - c2.R) / 255d,
                dG = (c1.G - c2.G) / 255d,
                dB = (c1.B - c2.B) / 255d;

         return dR * dR * .299 + dG * dG * .587 + dB * dB * .114
                + dLuma * dLuma;
      }

      private static double CalculateRelativeLuminance709(this RasterColor c1, RasterColor c2)
      {
         // https://en.wikipedia.org/wiki/Relative_luminance
         // https://en.wikipedia.org/wiki/Luma_(video)
         double y1 = (c1.R * 212.6 + c1.G * 715.2 + c1.B * 72.2) / (255d * 1000),
                y2 = (c2.R * 212.6 + c2.G * 715.2 + c2.B * 72.2) / (255d * 1000);

         double dLuma = y1 - y2;

         double dR = (c1.R - c2.R) / 255d,
                dG = (c1.G - c2.G) / 255d,
                dB = (c1.B - c2.B) / 255d;

         return dR * dR * .2126 + dG * dG * .7152 + dB * dB * .0722
                + dLuma * dLuma;
      }


      public static RasterColor FindClosestColor(
         RasterColor color,
         RasterColor[] palette,
         ColorDifferencingMethod colorDifferencingMethod)
      {
         double minDistance = double.MaxValue;
         var bestIndex = 0;

         switch (colorDifferencingMethod)
         {
            case ColorDifferencingMethod.EuclideanDistance:
               for (var i = 0; i < palette.Length; i++)
               {
                  double distance = color.CalculateEuclideanDistance(palette[i]);
                  if (!(distance < minDistance)) continue;
                  minDistance = distance;
                  bestIndex = i;
               }
               break;

            case ColorDifferencingMethod.RelativeLuminance601:
               for (var i = 0; i < palette.Length; i++)
               {
                  double distance = color.CalculateRelativeLuminance601(palette[i]);
                  if (!(distance < minDistance)) continue;
                  minDistance = distance;
                  bestIndex = i;
               }
               break;

            case ColorDifferencingMethod.RelativeLuminance709:
               for (var i = 0; i < palette.Length; i++)
               {
                  double distance = color.CalculateRelativeLuminance709(palette[i]);
                  if (!(distance < minDistance)) continue;
                  minDistance = distance;
                  bestIndex = i;
               }
               break;

            default:
               throw new ArgumentOutOfRangeException(nameof(colorDifferencingMethod), colorDifferencingMethod, null);
         }

         return palette[bestIndex];
      }


      private static byte AddClamp(byte a, int b)
      {
         if (b == 0) return a;
         int c = a + b;
         if (c < byte.MinValue) return byte.MinValue;
         if (c > byte.MaxValue) return byte.MaxValue;
         return (byte) c;
      }

      private static byte[] AllocateImageBytes(RasterImage image)
      {
         int height = image.Height;
         int bytesPerLine = image.Width * (image.BitsPerPixel == 24 ? 3 : 4);
         var imageBuffer = new byte[bytesPerLine * height];
         image.Access();
         try
         {
            for (var y = 0; y < height; y++)
               image.GetRow(y, imageBuffer, y * bytesPerLine, bytesPerLine);
         }
         finally
         {
            image.Release();
         }

         return imageBuffer;
      }

      private static void SetImageBytes(RasterImage image, byte[] imageBytes)
      {
         image.Access();
         try
         {
            int bytesPerLine = image.Width * (image.BitsPerPixel == 24 ? 3 : 4);
            for (var line = 0; line < image.Height; line++)
               image.SetRow(line, imageBytes, line * bytesPerLine, bytesPerLine);
         }
         finally
         {
            image.Release();
         }
      }

      public static RasterImage NoDithering(RasterImage image, RasterColor[] ditheringPalette,
                                            ColorDifferencingMethod colorMatching)
      {
         int width = image.Width, height = image.Height;
         int rOffset, bOffset, gOffset;
         if (image.Order == RasterByteOrder.Bgr)
         {
            rOffset = 2;
            gOffset = 1;
            bOffset = 0;
         }
         else
         {
            rOffset = 0;
            gOffset = 1;
            bOffset = 2;
         }

         byte[] buffer = AllocateImageBytes(image);

         // Translates the x,y coordinates to a buffer index
         int CalculateIndex(int x, int y)
         {
            return y * width * (image.BitsPerPixel == 24 ? 3 : 4)
                   + x * (image.BitsPerPixel == 24 ? 3 : 4);
         }

         for (var y = 0; y < height; y++)
         for (var x = 0; x < width; x++)
         {
            int index = CalculateIndex(x, y);

            byte b = buffer[index + bOffset];
            byte r = buffer[index + rOffset];
            byte g = buffer[index + gOffset];

            RasterColor bestColor = FindClosestColor(new RasterColor(r, g, b), ditheringPalette, colorMatching);

            buffer[index + bOffset] = bestColor.B;
            buffer[index + rOffset] = bestColor.R;
            buffer[index + gOffset] = bestColor.G;
         }
         SetImageBytes(image, buffer);

         return image;
      }

      public static RasterImage ErrorDiffusionDithering(ErrorDiffusionMatrix matrix, RasterImage image,
                                                        RasterColor[] ditheringPalette,
                                                        ColorDifferencingMethod colorMatching)
      {
         int width = image.Width,
             height = image.Height;
         int rOffset, bOffset, gOffset;
         if (image.Order == RasterByteOrder.Bgr)
         {
            rOffset = 2;
            gOffset = 1;
            bOffset = 0;
         }
         else
         {
            rOffset = 0;
            gOffset = 1;
            bOffset = 2;
         }

         byte[] buffer = AllocateImageBytes(image);

         int CalculateIndex(int x, int y)
         {
            return y * width * (image.BitsPerPixel == 24 ? 3 : 4) + x * (image.BitsPerPixel == 24 ? 3 : 4);
         }

         int CalculateError(int error, int numerator, int denominator)
         {
            if (error == 0) return 0;
            // fixes problem where for example -5/8==0, but -5>>3 == -1.
            return (int) Math.Floor((double) error * numerator / denominator);
         }

         void ApplyError(int bx, int by, int[] error, int numerator, int denominator)
         {
            int currentIndex = CalculateIndex(bx, by);
            buffer[currentIndex + bOffset] = AddClamp(buffer[currentIndex + bOffset],
                                                      CalculateError(error[bOffset], numerator, denominator));
            buffer[currentIndex + gOffset] = AddClamp(buffer[currentIndex + gOffset],
                                                      CalculateError(error[gOffset], numerator, denominator));
            buffer[currentIndex + rOffset] = AddClamp(buffer[currentIndex + rOffset],
                                                      CalculateError(error[rOffset], numerator, denominator));
         }

         for (var y = 0; y < height; y++)
         for (var x = 0; x < width; x++)
         {
            int index = CalculateIndex(x, y);

            byte b = buffer[index + bOffset],
                 r = buffer[index + rOffset],
                 g = buffer[index + gOffset];

            var currentPixel = new RasterColor(r, g, b);
            RasterColor bestColor = FindClosestColor(currentPixel, ditheringPalette, colorMatching);

            var error = new int[image.BitsPerPixel == 24 ? 3 : 4];
            error[rOffset] = r - bestColor.R;
            error[gOffset] = g - bestColor.G;
            error[bOffset] = b - bestColor.B;

            buffer[index + bOffset] = bestColor.B;
            buffer[index + rOffset] = bestColor.R;
            buffer[index + gOffset] = bestColor.G;

            if (matrix?.Cells == null) continue;
            foreach (ErrorDiffusionMatrix.Cell cell in matrix.Cells)
            {
               int targetX = x + cell.XOffset,
                   targetY = y + cell.YOffset;

               if (targetX < 0 || targetX >= image.Width) continue;
               if (targetY < 0 || targetY >= image.Height) continue;

               ApplyError(targetX, targetY, error, cell.Numerator, matrix.Denominator);
            }
         }

         SetImageBytes(image, buffer);
         return image;
      }

      public class ErrorDiffusionMatrix
      {
         public Cell[] Cells;
         public int Denominator;

         public class Cell
         {
            public int Numerator;
            public int XOffset;
            public int YOffset;
         }
      }
   }
}