using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using Leadtools;
using Leadtools.Annotations.Automation;
using Leadtools.Annotations.Core;
using Leadtools.Annotations.WinForms;
using Leadtools.Codecs;
using Leadtools.Controls;
using Leadtools.Forms.DocumentWriters;
using Leadtools.Forms.Ocr;

namespace Pdf_Redaction_Ocr
{
    public partial class MainForm : Form
    {
        private AnnAutomation _annAutomation;
        private AnnAutomationManager _annAutomationManager;
        private ImageViewerAutomationControl _automationControl;
        private AutomationManagerHelper _automationManagerHelper;

        private ImageViewer _imageViewer;

        public MainForm()
        {
            UnlockSupport();
            InitializeComponent();
            InitViewer();
            InitAnnotations();
        }

        private static void UnlockSupport()
        {
            // TODO: This is using the default path; change as needed.
            // If you need a license file, go to https://www.leadtools.com/downloads?category=main
            const string lic = @"C:\leadtools 19\common\license\Leadtools.lic";
            var key = File.ReadAllText(@"C:\leadtools 19\common\license\Leadtools.lic.key");
            RasterSupport.SetLicense(lic, key);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_automationControl != null)
                _automationControl.Dispose();

            if (_automationManagerHelper != null)
                _automationManagerHelper.Dispose();

            if (_imageViewer == null) return;
            if (_imageViewer.HasImage)
                _imageViewer.Image.Dispose();
            _imageViewer.Dispose();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var dlg = new OpenFileDialog())
            {
                if (dlg.ShowDialog() != DialogResult.OK) return;
                using (var codecs = new RasterCodecs())
                {
                    codecs.Options.RasterizeDocument.Load.Resolution = 300;
                    _imageViewer.Image = codecs.Load(dlg.FileName);
                }
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var dlg = new SaveFileDialog {Filter = @"PDF|*.pdf"})
            {
                if (dlg.ShowDialog() != DialogResult.OK) return;
                try
                {
                    Cursor = Cursors.WaitCursor;
                    var outputImage = BurnImage();
                    DoOcr(outputImage, dlg.FileName);
                    Process.Start(dlg.FileName);
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
        }

        private RasterImage BurnImage()
        {
            var image = _imageViewer.Image;
            _annAutomationManager.RenderingEngine.RenderOnImage(_annAutomation.Container, image);
            _annAutomation.Container.Children.Clear();
            return image;
        }

        private static void DoOcr(RasterImage image, string fileName)
        {
            using (var ocrEngine = OcrEngineManager.CreateEngine(OcrEngineType.Advantage, false))
            {
                ocrEngine.Startup(null, null, null, null);

                var pdfOptions = ocrEngine.DocumentWriterInstance.GetOptions(DocumentFormat.Pdf) as PdfDocumentOptions;
                if (pdfOptions == null) throw new ApplicationException("Failed to set PDF Options");

                pdfOptions.ImageOverText = true;
                ocrEngine.DocumentWriterInstance.SetOptions(DocumentFormat.Pdf, pdfOptions);

                using (var ocrDocument = ocrEngine.DocumentManager.CreateDocument())
                {
                    ocrDocument.Pages.AddPage(image, null);
                    ocrDocument.Pages.Recognize(null);
                    ocrDocument.Save(fileName, DocumentFormat.Pdf, null);
                }
            }
        }

        private void InitViewer()
        {
            _imageViewer = new ImageViewer {Dock = DockStyle.Fill};
            panel1.Controls.Add(_imageViewer);
        }

        private void InitAnnotations()
        {
            _annAutomationManager = new AnnAutomationManager
            {
                RestrictDesigners = true,
                EditObjectAfterDraw = false
            };
            _annAutomationManager.CreateDefaultObjects();

            // Only show the selector and redaction.  Remove the other objects.
            for (var count = 0; count < _annAutomationManager.Objects.Count; count++)
            {
                var id = _annAutomationManager.Objects[count].Id;
                if (id == AnnObject.RedactionObjectId || id == AnnObject.SelectObjectId) continue;
                _annAutomationManager.Objects.RemoveAt(count);
                count--;
            }

            _automationManagerHelper = new AutomationManagerHelper(_annAutomationManager);
            _automationManagerHelper.CreateToolBar();
            panel1.Controls.Add(_automationManagerHelper.ToolBar);

            _automationControl = new ImageViewerAutomationControl {ImageViewer = _imageViewer};

            _imageViewer.InteractiveModes.BeginUpdate();
            _imageViewer.InteractiveModes.Add(new AutomationInteractiveMode {AutomationControl = _automationControl});
            _imageViewer.InteractiveModes.EndUpdate();

            _annAutomation = new AnnAutomation(_annAutomationManager, _automationControl) {Active = true};

            _imageViewer.ItemChanged += (s, e) =>
            {
                if (_annAutomation == null) return;
                _annAutomation.Container.Size =
                    _annAutomation.Container.Mapper.SizeToContainerCoordinates(_imageViewer.ImageSize.ToLeadSizeD());
                _imageViewer.Zoom(ControlSizeMode.Fit, 1, LeadPoint.Empty);
                _annAutomationManager.CurrentObjectId = AnnObject.RedactionObjectId;
            };
        }
    }
}