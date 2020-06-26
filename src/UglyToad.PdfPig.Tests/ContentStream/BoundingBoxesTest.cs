namespace UglyToad.PdfPig.Tests.ContentStream
{
    using PdfPig.Core;
    using PdfPig.DocumentLayoutAnalysis;
    using PdfPig.DocumentLayoutAnalysis.WordExtractor;
    using PdfPig.Content;
    using System.Drawing;
    using System.IO;
    using System.Text;
    using Xunit;

    public class BoundingBoxesTest
    {
        [Fact]
        public void CorrectlySetsSingleLetterBoxes()
        {
            var zoom = 5.0f;
            var greenPen = new Pen(Color.GreenYellow, zoom);
            using var document = PdfDocument.Open("/Users/bryan/Documents/Code/PdfPig/src/UglyToad.PdfPig.Tests/Resources/biotest.pdf");
            var page = document.GetPage(3);
            using var bmp = new Bitmap($"/Users/bryan/Documents/Code/PdfPig/src/UglyToad.PdfPig.Tests/Resources/biotest3.png");
            using var graphics = Graphics.FromImage(bmp);

            foreach (var word in page.GetWords(NearestNeighbourWordExtractor.Instance))
            {
                if (word.Text == "Pacing")
                {
                    var temp = word.Text;
                }
                foreach (var letter in word.Letters)
                {
                    if (string.IsNullOrWhiteSpace(letter.Value)) continue;

                    double ulx = 0, uly = 0, width = 0, height = 0;

                    if (letter.TextOrientation == TextOrientation.Horizontal)
                    {
                        ulx = letter.GlyphRectangle.TopLeft.X * zoom;
                        uly = letter.GlyphRectangle.TopLeft.Y * zoom;
                        width = letter.GlyphRectangle.Width * zoom;
                        height = letter.GlyphRectangle.Height * zoom;
                    }
                    else if (letter.TextOrientation == TextOrientation.Rotate90)
                    {
                        ulx = letter.GlyphRectangle.BottomLeft.X * zoom;
                        uly = letter.GlyphRectangle.BottomLeft.Y * zoom;
                        width = letter.GlyphRectangle.Height * zoom;
                        height = letter.GlyphRectangle.Width * zoom;
                    }
                    else if (letter.TextOrientation == TextOrientation.Rotate180)
                    {
                        ulx = letter.GlyphRectangle.BottomRight.X * zoom;
                        uly = letter.GlyphRectangle.BottomRight.Y * zoom;
                        width = letter.GlyphRectangle.Width * zoom;
                        height = letter.GlyphRectangle.Height * zoom;
                    }
                    else if (letter.TextOrientation == TextOrientation.Rotate270)
                    {
                        ulx = letter.GlyphRectangle.TopRight.X * zoom;
                        uly = letter.GlyphRectangle.TopRight.Y * zoom;
                        width = letter.GlyphRectangle.Height * zoom;
                        height = letter.GlyphRectangle.Width * zoom;
                    }

                    var rect = new Rectangle((int)ulx, bmp.Height - (int)uly, (int)width, (int)height);
                    graphics.DrawRectangle(greenPen, rect);
                }
            }
            bmp.Save($"/Users/bryan/Desktop/output.png");
        }
    }
}
