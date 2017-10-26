using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Telekad.Mapping;

namespace Telekad.Services.MapTilesAdapter.Debug
{
    public class DebugMapLayer : BaseMapLayer
    {
        public DebugMapLayer(IConfigurationElement configuration) : base(configuration) { }

        protected virtual Brush Background { get { return Brushes.LightGray; } }

        private static RenderTargetBitmap GenerateDebugTile(string key, int width, int height, Brush background)
        {
            var rtBitmap = new RenderTargetBitmap(
                width,
                height,
                96,
                96,
                PixelFormats.Default);

            var drawVisual = new DrawingVisual();
            using (DrawingContext dc = drawVisual.RenderOpen())
            {
                var ft = new FormattedText(key, CultureInfo.InvariantCulture,
                                           FlowDirection.LeftToRight, new Typeface("Segoe UI"), 26, Brushes.DarkGray);
                if (ft.Width + 10 > width)
                    ft.SetFontSize(16);
                var p3 = new Point((width / 2.0) - ft.Width / 2, (height / 2.0) - ft.Height / 2);
                dc.DrawRectangle(background, new Pen(Brushes.Black, .4), new Rect(0, 0, rtBitmap.Width, rtBitmap.Height));
                dc.DrawText(ft, p3);

            }
            rtBitmap.Render(drawVisual);
            return rtBitmap;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2122:DoNotIndirectlyExposeMethodsWithLinkDemands")]
        protected override void GetLayer(QuadKey key, Stream stream)
        {
            var counter = 0;
            var displayKey = String.Join(" ", key.ToString().GroupBy(c => counter++ / 4).Select(g => new String(g.ToArray())));
            var encoder = new PngBitmapEncoder();
            var frame = BitmapFrame.Create(GenerateDebugTile(displayKey, 256, 256, this.Background));
            encoder.Frames.Add(frame);
            encoder.Save(stream);
            stream.Seek(0, 0);
        }

    }
}