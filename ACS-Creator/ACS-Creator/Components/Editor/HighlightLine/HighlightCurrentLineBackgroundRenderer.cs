using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using ICSharpCode.AvalonEdit;
using ICSharpCode.AvalonEdit.Rendering;

namespace ACS.Creator.Components.Editor.HighlightLine
{
    public class HighlightCurrentLineBackgroundRenderer : IBackgroundRenderer
    {
        private TextEditor _editor;

        public HighlightCurrentLineBackgroundRenderer(TextEditor editor)
        {
            _editor = editor;
        }
        public KnownLayer Layer
        {
            get { return KnownLayer.Background; }
        }
        public void Draw(TextView textView, DrawingContext drawingContext)
        {
            if (_editor.Document == null)
                return;
            textView.EnsureVisualLines();
            var currentLine = _editor.Document.GetLineByOffset(_editor.CaretOffset);
            foreach (var rect in BackgroundGeometryBuilder.GetRectsForSegment(textView, currentLine))
            {
                drawingContext.DrawRectangle(
                    new SolidColorBrush(Color.FromArgb(200, 50, 50, 50)), null,
                    new Rect(rect.Location, new Size(textView.ActualWidth, rect.Height)));

                int borderThinkness = 2;
                drawingContext.DrawRectangle(
                    new SolidColorBrush(Color.FromArgb(200, 0, 0, 0)), null,
                    new Rect(new Point(rect.Location.X+borderThinkness,rect.Location.Y+borderThinkness) , new Size(textView.ActualWidth-2*borderThinkness, rect.Height- 2 * borderThinkness)));
            }
        }
    }
}
