using Leadtools;
using Leadtools.Pdf;

namespace Pdf_Highlight_Words
{
    public class PdfCharacter
    {
        public PDFObject PdfObject { get; set; }
        public LeadRect Bounds { get; set; }

        public PdfCharacter(PDFObject pdfObject, PDFRect bounds)
        {
            PdfObject = pdfObject;
            Bounds = LeadRect.FromLTRB((int)bounds.Left, (int)bounds.Top, (int)bounds.Right, (int)bounds.Bottom);
        }
        public override string ToString()
        {
            return PdfObject.Code.ToString();
        }
    }
}
