using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Telekad.Services.MapTilesAdapter
{
    public static class ImageUtils
    {
        [ExcludeFromCodeCoverage]
        public static RenderTargetBitmap OverlapImages(BitmapImage[] watermarkImages)
        {
            if (watermarkImages.Length == 0)
                return null;
            var rtBitmap = new RenderTargetBitmap(
                watermarkImages[0].PixelWidth,
                watermarkImages[0].PixelHeight,
                watermarkImages[0].DpiX,
                watermarkImages[0].DpiY,
                PixelFormats.Default);

            var drawVisual = new DrawingVisual();
            using (DrawingContext dc = drawVisual.RenderOpen())
            {
                foreach (var watermarkImage in watermarkImages)
                {
                    var imageBrush = new ImageBrush
                                         {
                                             ImageSource = watermarkImage,
                                             Stretch = Stretch.Uniform,
                                             TileMode = TileMode.None,
                                             AlignmentX = AlignmentX.Center,
                                             AlignmentY = AlignmentY.Center,
                                             Opacity = 1
                                         };
                    dc.DrawRectangle(imageBrush, null /* no pen */, new Rect(0, 0, rtBitmap.Width, rtBitmap.Height));
                }
            }

            rtBitmap.Render(drawVisual);
            return rtBitmap;
        }
    }
}