using System;
namespace P1ctur3.Viewer
{
    public interface IMemoryDataSource
    {
        System.Collections.Generic.IEnumerable<MapBucket> Search(System.Collections.Generic.IEnumerable<Telekad.Mapping.QuadKey> quadkeyList);
    }
}
