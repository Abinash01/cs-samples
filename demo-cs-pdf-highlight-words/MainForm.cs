using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Leadtools;
using Leadtools.Codecs;
using Leadtools.Drawing;
using Leadtools.Pdf;
using Leadtools.WinForms;

namespace Pdf_Highlight_Words
{
    public partial class MainForm : Form
    {
        private readonly Dictionary<int, PdfWord[]> _words = new Dictionary<int, PdfWord[]>();
        private readonly Dictionary<int, PdfCharacter[]> _characters = new Dictionary<int, PdfCharacter[]>();
        private PDFDocument _pdf;
        private bool[] _parsed;

        public MainForm()
        {
            InitializeComponent();

            // TODO: Assumes the default LEADTOOLS installation path.  Update as needed.
            RasterSupport.SetLicense(@"C:\LEADTOOLS 19\Common\License\LEADTOOLS.LIC",
                System.IO.File.ReadAllText(@"C:\LEADTOOLS 19\Common\License\LEADTOOLS.LIC.KEY"));

            // Set up RasterImageViewer to paint with interpolation
            var props = rasterImageViewer1.PaintProperties;
            props.PaintDisplayMode = RasterPaintDisplayModeFlags.ScaleToGray | RasterPaintDisplayModeFlags.Resample;
            rasterImageViewer1.PaintProperties = props;

            txtPages.Enabled = false;
            chkFit.Checked = true;
        }

        private void ParsePage(int pageNumber)
        {
            if (_parsed[pageNumber - 1]) return;
            // Parse the PDF page
            _pdf.ParsePages(PDFParsePagesOptions.AllIgnoreWhiteSpaces, pageNumber, pageNumber);
            _words.Add(pageNumber, PdfWord.GetWords(_pdf, pageNumber));

            // Get characters
            var page = _pdf.Pages[pageNumber - 1];
            if (page.Objects != null)
            {
                var characters = new List<PdfCharacter>();
                foreach (var obj in page.Objects)
                {
                    if (obj.ObjectType != PDFObjectType.Text) continue;
                    characters.Add(new PdfCharacter(obj,
                        page.ConvertRect(PDFCoordinateType.Pdf, PDFCoordinateType.Pixel, obj.Bounds)));
                }
                _characters.Add(pageNumber, characters.ToArray());
            }
            _parsed[pageNumber - 1] = true; // Mark that we have parsed this page 
        }

        private void DisplayPage(int pageNumber)
        {
            lstWords.BeginUpdate();
            lstCharacters.BeginUpdate();

            lstWords.Items.Clear();
            lstCharacters.Items.Clear();

            ParsePage(pageNumber);

            using (var codecs = new RasterCodecs())
                rasterImageViewer1.Image = _pdf.GetPageImage(codecs, pageNumber);

            // Add each character to the list box
            foreach (var character in _characters[pageNumber])
                lstCharacters.Items.Add(character);

            // Add each word to the list box
            foreach (var word in _words[pageNumber])
                lstWords.Items.Add(word);

            lstWords.EndUpdate();
            lstCharacters.EndUpdate();
        }


        private void DrawCharacterRect(PdfCharacter pdfChar)
        {
            var clr = Color.DarkRed;
            if (pdfChar.PdfObject.TextProperties.IsEndOfLine)
                clr = Color.DarkGreen;
            else if (pdfChar.PdfObject.TextProperties.IsEndOfWord)
                clr = Color.DarkBlue;

            DrawRect(pdfChar.Bounds, clr);
        }

        private void DrawRect(LeadRect rect, Color clr)
        {
            if (rasterImageViewer1.Image == null) return;
            // Create a container object so we can draw on the image.  Note this usually changes the image's bit depth to 24.
            var container = new RasterImageGdiPlusGraphicsContainer(rasterImageViewer1.Image);
            using (var g = container.Graphics)
            using (Brush b = new SolidBrush(Color.FromArgb(50, clr)))
            {
                g.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceOver;
                g.FillRectangle(b, new Rectangle(rect.Left, rect.Top, rect.Width, rect.Height));
            }
        }

        private void ChangePage()
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                DisplayPage((int)txtPages.Value);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void chkFit_CheckedChanged(object sender, EventArgs e)
        {
            rasterImageViewer1.SizeMode = chkFit.Checked ? RasterPaintSizeMode.Fit : RasterPaintSizeMode.Normal;
            rasterImageViewer1.InteractiveMode = chkFit.Checked
                ? RasterViewerInteractiveMode.MagnifyGlass
                : RasterViewerInteractiveMode.Pan;
        }

        private void lstCharacters_SelectedIndexChanged(object sender, EventArgs e)
        {
            var pdfChar = lstCharacters.SelectedItem as PdfCharacter;
            if (pdfChar == null) return;
            DrawCharacterRect(pdfChar);
            txtBounds.Text =
                $@"{pdfChar.Bounds.Left},{pdfChar.Bounds.Top},{pdfChar.Bounds.Width},{pdfChar.Bounds.Height}";
            rasterImageViewer1.Refresh();
        }

        private void lstWords_SelectedIndexChanged(object sender, EventArgs e)
        {
            var word = lstWords.SelectedItem as PdfWord;
            if (word == null) return;
            DrawRect(word.Bounds, Color.DarkRed);
            txtBounds.Text = $@"{word.Bounds.Left},{word.Bounds.Top},{word.Bounds.Width},{word.Bounds.Height}";
            rasterImageViewer1.Refresh();
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            try
            {
                using (var dlg = new OpenFileDialog())
                {
                    dlg.Filter = @"PDF (*.pdf)|*.pdf";
                    dlg.FilterIndex = 0;
                    if (dlg.ShowDialog() != DialogResult.OK) return;
                    _pdf?.Dispose();
                    _pdf = new PDFDocument(dlg.FileName) {Resolution = 300};
                    
                    _words.Clear();
                    _characters.Clear();
                    _parsed = new bool[_pdf.Pages.Count];

                    // Setup page changer
                    txtPages.Enabled = true;
                    txtPages.Maximum = _pdf.Pages.Count;
                    txtPages.Minimum = 1;
                    txtPages.Value = 1;

                    ChangePage();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtPages_ValueChanged(object sender, EventArgs e)
        {
            ChangePage();
        }
    }
}
