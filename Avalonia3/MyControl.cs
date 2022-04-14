using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;

namespace Avalonia3
{
    public class MyControl : Control
    {
        public override void Render(DrawingContext context)
        {
            context.DrawLine(_pen, new Point(10, 10), new Point(190, 190));
            context.DrawText(_pen.Brush, new Point(10, 175), _text);
        }

        private readonly IPen _pen = new Pen(Brushes.DeepPink, 1);

        private readonly FormattedText _text = new("Manual Text Render",
            new Typeface("Verdana"),
            15,
            TextAlignment.Left,
            TextWrapping.NoWrap,
            Size.Infinity);
    }
}
