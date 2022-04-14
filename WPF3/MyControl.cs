using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WPF3
{
    public class MyControl : Control
    {
        protected override void OnRender(DrawingContext drawingContext)
        {
            drawingContext.DrawLine(_pen, new Point(10, 10), new Point(190, 190));
            drawingContext.DrawText(_text, new Point(10, 175));
        }

        private readonly Pen _pen = new(Brushes.DeepPink, 1);
        private readonly FormattedText _text = new("Manual Text Render",
            CultureInfo.InvariantCulture,
            FlowDirection.LeftToRight,
            new Typeface("Verdana"),
            15,
            Brushes.DeepPink,
            1);
    }
}
