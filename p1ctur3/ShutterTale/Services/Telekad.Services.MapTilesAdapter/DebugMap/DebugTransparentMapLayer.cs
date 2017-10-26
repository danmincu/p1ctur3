using System.Windows.Media;

namespace Telekad.Services.MapTilesAdapter.Debug
{
    public class DebugTransparentMapLayer : DebugMapLayer
    {
        public DebugTransparentMapLayer(IConfigurationElement configuration) : base(configuration) { }

        protected override Brush Background { get { return Brushes.Transparent; } }
    }
}