using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DomainLib
{
    public enum Orientation
    {
        Unknown,
        Normal,
        Rotated90,
        Rotated180,
        Rotated270
    }

    public class Dimensions
    {
        public int PixelX { set; get; }
        public int PixelY { set; get; }
        public int Dpi { set; get; }
        //public double XResolution { set; get; }
        //public double YResolution { set; get; }

        //public string ResolutionUnit { set; get; } //e.g. Inches

    }

    public class ImageCharacteristics
    {
       // public int Dpi { set; get; }
        public int FocalLenght { set; get; }
        public bool Flash { set; get; }

        //etc
    }

    public class VideoCharacteristics
    {
        public TimeSpan Duration { set; get; }
    }


    public class Location
    {
        public double Latitude { set; get; }
        public double Longitude { set; get; }
        public double Altitude { set; get; }
    }

    public class MediaHardware
    {
        public string Name { set; get; }
        public string Make { set; get; }
        public string Model { set; get; }
        public string SoftwareVersion { set; get; }
    }

    interface IMedia
    {
        Guid Id { get; set; }
        DateTime CaptureDateTime { set; get; }
        DateTime FileDateTime { set; get; }
        DateTime ImportDateTime { set; get; }
        //represents the media datetime uses capture date time if known or the file date time otherwise
        DateTime DateTime { get; }
        Orientation Orientation { set; get; }
        Location Location { set; get; }
        Dimensions Dimensions { set; get; }
        string ContentType { set; get; } //e.g image/jpg sound/wav video/avi
        MediaHardware MediaHardware { set; get; }
        string Origin { set; get; } //hdd import, camera import
        Int32 Size { set; get; }
    }



}
