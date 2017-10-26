using ExifLibrary;
using System;
namespace P1ctur3.Exif
{
    public interface IExifProcessor
    {
        Telekad.Mapping.Coordinate Coordinates(ExifLibrary.ExifPropertyCollection properties);
        DateTime? DateTime(ExifLibrary.ExifPropertyCollection properties);
        ExifLibrary.ExifPropertyCollection Extract(System.IO.Stream stream);
        ExifLibrary.ExifPropertyCollection Extract(string fileName);
        ExifTag[] ExcludedTags();
    }
}
