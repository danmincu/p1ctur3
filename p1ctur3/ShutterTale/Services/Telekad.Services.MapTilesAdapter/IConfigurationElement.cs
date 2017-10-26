namespace Telekad.Services.MapTilesAdapter
{
    public interface IConfigurationElement
    {
        string this[string index] { get; }
    }
}