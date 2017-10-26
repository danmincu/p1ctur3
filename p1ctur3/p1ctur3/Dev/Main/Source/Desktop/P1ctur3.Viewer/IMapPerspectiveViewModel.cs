using System;
namespace P1ctur3.Viewer
{
    public interface IMapPerspectiveViewModel
    {
        void Add(MapItemPresenterBase bucket);
        void Clear();
    }
}
