using System.Collections.Generic;
using Leadtools;
using Leadtools.Pdf;

namespace Pdf_Highlight_Words
{
    public class PdfWord
    {
        public LeadRect Bounds { get; set; }
        public string Value { get; set; }
        public bool IsEndOfLine { get; set; }

        public override string ToString()
        {
            return Value;
        }

        public static PdfWord[] GetWords(PDFDocument document, int pageNumber)
        {
            var page = document.Pages[pageNumber - 1];
            var objects = page.Objects;
            var words = new List<PdfWord>();

            if (!(objects?.Count > 0)) return words.ToArray();
            
            var objectIndex = 0;
            var objectCount = objects.Count;

            // Loop through all the objects
            while (objectIndex < objectCount)
            {
                // Find the total bounding rectangle, begin and end index of the next word
                var wordBounds = LeadRect.Empty;
                var firstObjectIndex = objectIndex;

                // Loop till we reach EndOfWord or reach the end of the objects
                PDFObject pdfObject;
                do
                {
                    pdfObject = objects[objectIndex];
                    if (pdfObject.ObjectType == PDFObjectType.Text && !char.IsWhiteSpace(pdfObject.Code))
                    {
                        // Add the bounding rectangle of this object
                        var pdfRect = page.ConvertRect(PDFCoordinateType.Pdf, PDFCoordinateType.Pixel, pdfObject.Bounds);
                        var objectBounds = LeadRect.FromLTRB((int) pdfRect.Left, (int) pdfRect.Top, (int) pdfRect.Right,
                            (int) pdfRect.Bottom);

                        wordBounds = wordBounds.IsEmpty ? objectBounds : LeadRect.Union(wordBounds, objectBounds);
                    }
                    else
                        firstObjectIndex = objectIndex + 1;

                    objectIndex++;
                } while (objectIndex < objectCount && !(pdfObject.TextProperties.IsEndOfWord ||
                                                        pdfObject.TextProperties.IsEndOfLine));

                if (firstObjectIndex == objectIndex) continue;

                var chars = new char[objectIndex - firstObjectIndex];
                for (var i = firstObjectIndex; i < objectIndex; i++)
                {
                    if (objects[i].ObjectType == PDFObjectType.Text)
                        chars[i - firstObjectIndex] = objects[i].Code;
                }
                    
                // Add this word to the list
                var lastObject = objects[objectIndex - 1];
                var word = new PdfWord
                {
                    Value = new string(chars),
                    Bounds = wordBounds,
                    IsEndOfLine = lastObject.TextProperties.IsEndOfLine
                };
                words.Add(word);
            }
            return words.ToArray();
        }
      
    }
}
