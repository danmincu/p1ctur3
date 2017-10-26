using System.Windows.Media.Imaging;
using Telekad.Mapping;

namespace Telekad.Services.MapTilesAdapter
{
    public interface IMapLayer
    {
        BitmapImage GetBitmap(QuadKey key);
        string Name { get; }
        bool CacheTiles { get; }
        string CacheTilesFolder { set; get; }
    }
}