using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Leadtools;
using Leadtools.Codecs;
using Leadtools.Drawing;
using Leadtools.ImageProcessing;
using Leadtools.ImageProcessing.Color;
using Leadtools.ImageProcessing.Effects;

namespace Dithering
{
   public partial class Form1 : Form
   {
      public Form1()
      {
         InitializeComponent();
      }

      private void Form1_Load(object sender, EventArgs e)
      {
         const string licenseFile = @"C:\LEADTOOLS 19\Common\License\Leadtools.lic";
         string developerKey = File.ReadAllText(licenseFile + ".key");
         RasterSupport.SetLicense(licenseFile, developerKey);
         // Initialize a new RasterCodecs object 
         using (var codecs = new RasterCodecs())
         {
            rpbOriginal.Image = codecs.Load(
               @"tippy test_edited.jpg", 24,
               CodecsLoadByteOrder.Bgr, 1, 1);
         }

         SetUpDitheringMethods();
         SetUpPalettes();
         SetUpColorMatchingMethods();

         rpbNormalScale.UseDpi = false;
         nudMaxPixels.Value = 5000;
      }

      private void SetUpColorMatchingMethods()
      {
         cbColorMatching.DataSource = Enum.GetValues(typeof(ColorDifferencingMethod));
         cbColorMatching.SelectedIndex = 0;
      }

      private void SetUpPalettes()
      {
         var paletteItems = new[]
         {
            new ComboBoxPaletteItem {Text = "CMYK", Palette = Palette.CmykPalette},
            new ComboBoxPaletteItem {Text = "Rubik's Cube", Palette = Palette.RubiksCubePalette},
            new ComboBoxPaletteItem {Text = "Wood Stain", Palette = Palette.WoodStainPalette},
            new ComboBoxPaletteItem {Text = "Basic", Palette = Palette.BasicColorPalette},
            new ComboBoxPaletteItem {Text = "Triadic", Palette = Palette.TriadicColorPalette}
         };

         cbPalette.DisplayMember = "Text";
         cbPalette.ValueMember = "Palette";
         cbPalette.DataSource = paletteItems;
         cbPalette.SelectedIndex = 0;
      }

      private void SetUpDitheringMethods()
      {
         var ditheringItems = new List<ComboBoxDitheringItem>
         {
            new ComboBoxDitheringItem {Text = "Custom None", Matrix = Dithering.NoDiffusionMatrix},
            new ComboBoxDitheringItem {Text = "Custom Atkinson", Matrix = Dithering.AtkinsonMatrix},
            new ComboBoxDitheringItem {Text = "Custom Floyd-Steinberg", Matrix = Dithering.FloydSteinbergMatrix},
            new ComboBoxDitheringItem {Text = "Custom Stucki", Matrix = Dithering.StuckiMatrix}
         };
         ditheringItems.AddRange(Enum.GetNames(typeof(RasterDitheringMethod))
                                     .Select(name => new ComboBoxDitheringItem {Text = name, Matrix = null}));

         cbDitheringMethod.DisplayMember = "Text";
         cbDitheringMethod.ValueMember = "Matrix";
         cbDitheringMethod.DataSource = ditheringItems;
         cbDitheringMethod.SelectedIndex = 0;
      }

      private void ProcessImage()
      {
         if (cbPalette.SelectedItem == null) return;

         int pixelCount = rpbOriginal.Image.Width * rpbOriginal.Image.Height;
         var newPixelMax = (int) nudMaxPixels.Value;
         double factor = Math.Sqrt((double) newPixelMax / pixelCount);
         var newWidth = (int) (rpbOriginal.Image.Width * factor);
         var newHeight = (int) (rpbOriginal.Image.Height * factor);
         if (newWidth == 0 || newHeight == 0) return;
         lblWidthHeight.Text =
            $@"{newWidth} ({newWidth / 2d}"") x {newHeight} ({newHeight / 2d}"") = {newWidth * newHeight}";


         RasterColor[] palette = (cbPalette.SelectedItem as ComboBoxPaletteItem)?.Palette ?? Palette.CmykPalette;

         rpbPreProcessed.Image?.Dispose();
         rpbPreProcessed.Image = rpbOriginal.Image.Clone();

         new GammaCorrectCommand {Gamma = (int) (nudGamma.Value * 100)}.Run(rpbPreProcessed.Image);
         new SharpenCommand((int) nudSharpness.Value).Run(rpbPreProcessed.Image);

         new SizeCommand
         {
            Flags = RasterSizeFlags.Bicubic,
            Height = newHeight,
            Width = newWidth
         }.Run(rpbPreProcessed.Image);
         rpbPreProcessed.Image.XResolution = 2;
         rpbPreProcessed.Image.YResolution = 2;

         rpbProcessed.Image?.Dispose();
         if (Enum.TryParse(cbDitheringMethod.SelectedItem.ToString(), out RasterDitheringMethod ditheringMethod))
         {
            rpbProcessed.Image = rpbPreProcessed.Image.Clone();
            new ColorResolutionCommand(
                  ColorResolutionCommandMode.InPlace,
                  4, RasterByteOrder.Bgr,
                  ditheringMethod,
                  ColorResolutionCommandPaletteFlags.UsePalette,
                  palette)
               .Run(rpbProcessed.Image);
         }
         else
         {
            if (!Enum.TryParse(cbColorMatching.SelectedValue?.ToString(), out ColorDifferencingMethod cmm))
               cmm = ColorDifferencingMethod.EuclideanDistance;

            var selectedMatrix = cbDitheringMethod.SelectedValue as Dithering.ErrorDiffusionMatrix;
            if (selectedMatrix != null)
               rpbProcessed.Image =
                  Dithering.ErrorDiffusionDithering(selectedMatrix, rpbPreProcessed.Image.Clone(), palette, cmm);
         }
         rpbNormalScale.Image = rpbProcessed.Image;
      }

      private void btnLoad_Click(object sender, EventArgs e)
      {
         // show the open file dialog 
         var dlg = new OpenFileDialog {Filter = @"All Files|*.*"};

         if (dlg.ShowDialog(this) != DialogResult.OK) return;
         try
         {
            // try to load the file 
            rpbOriginal.Image?.Dispose();
            // set the image into the viewer
            using (var codecs = new RasterCodecs())
            {
               rpbOriginal.Image = codecs.Load(dlg.FileName);
            }
            ProcessImage();
         }
         catch (Exception ex)
         {
            MessageBox.Show(this, ex.Message);
         }
      }

      private void cbDitheringMethod_SelectedIndexChanged(object sender, EventArgs e)
      {
         ProcessImage();
      }

      private void cbPalette_SelectedIndexChanged(object sender, EventArgs e)
      {
         ProcessImage();
      }


      private void nudGamma_ValueChanged(object sender, EventArgs e)
      {
         ProcessImage();
      }

      private void nudSharpness_ValueChanged(object sender, EventArgs e)
      {
         ProcessImage();
      }

      private void cbColorMatching_SelectedIndexChanged(object sender, EventArgs e)
      {
         ProcessImage();
      }

      private void nudMaxPixels_ValueChanged(object sender, EventArgs e)
      {
         ProcessImage();
      }

      public class ComboBoxPaletteItem
      {
         public string Text { get; set; }
         public RasterColor[] Palette { get; set; }

         public override string ToString()
         {
            return Text;
         }
      }


      public class ComboBoxDitheringItem
      {
         public string Text { get; set; }
         public Dithering.ErrorDiffusionMatrix Matrix { get; set; }

         public override string ToString()
         {
            return Text;
         }
      }
   }
}