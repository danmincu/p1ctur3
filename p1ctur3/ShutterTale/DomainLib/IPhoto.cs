using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLib
{
    public interface IPhoto
    {
        System.String Make { set; get; } //Apple
        System.String Model { set; get; } //iPhone 4
        ExifLibrary.Orientation Orientation { set; get; } //RotatedRight
        ExifLibrary.MathEx.UFraction32 XResolution { set; get; } //72/1
        ExifLibrary.MathEx.UFraction32 YResolution { set; get; } //72/1
        ExifLibrary.ResolutionUnit ResolutionUnit { set; get; } //Inches
        System.String Software { set; get; } //4.3.1
        System.DateTime DateTime { set; get; } //2011-06-22 2:10:00 PM
        ExifLibrary.YCbCrPositioning YCbCrPositioning { set; get; } //Centered
        System.UInt32 EXIFIFDPointer { set; get; } //204
        System.UInt32 GPSIFDPointer { set; get; } //554
        ExifLibrary.MathEx.UFraction32 ExposureTime { set; get; } //1/60
        ExifLibrary.MathEx.UFraction32 FNumber { set; get; } //12/5
        ExifLibrary.ExposureProgram ExposureProgram { set; get; } //Normal
        System.UInt16 ISOSpeedRatings { set; get; } //125
        System.String ExifVersion { set; get; } //0221
        System.DateTime DateTimeOriginal { set; get; } //2011-06-22 2:10:00 PM
        System.DateTime DateTimeDigitized { set; get; } //2011-06-22 2:10:00 PM
        System.Byte[] ComponentsConfiguration { set; get; } //System.Byte[]
        ExifLibrary.MathEx.Fraction32 ShutterSpeedValue { set; get; } //10285/1741
        ExifLibrary.MathEx.UFraction32 ApertureValue { set; get; } //4845/1918
        ExifLibrary.MeteringMode MeteringMode { set; get; } //Pattern
        ExifLibrary.Flash Flash { set; get; } //NoFlashFunction
        ExifLibrary.MathEx.UFraction32 FocalLength { set; get; } //77/20
        System.String FlashpixVersion { set; get; } //0100
        ExifLibrary.ColorSpace ColorSpace { set; get; } //sRGB
        System.UInt32 PixelXDimension { set; get; } //640
        System.UInt32 PixelYDimension { set; get; } //480
        ExifLibrary.SensingMethod SensingMethod { set; get; } //OneChipColorAreaSensor
        ExifLibrary.ExposureMode ExposureMode { set; get; } //Auto
        ExifLibrary.WhiteBalance WhiteBalance { set; get; } //Auto
        ExifLibrary.SceneCaptureType SceneCaptureType { set; get; } //Standard
        ExifLibrary.Sharpness Sharpness { set; get; } //Normal
        ExifLibrary.GPSLatitudeRef GPSLatitudeRef { set; get; } //North
        ExifLibrary.MathEx.UFraction32[] GPSLatitude { set; get; } //ExifLibrary.MathEx+UFraction32[]
        ExifLibrary.GPSLongitudeRef GPSLongitudeRef { set; get; } //West
        ExifLibrary.MathEx.UFraction32[] GPSLongitude { set; get; } //ExifLibrary.MathEx+UFraction32[]
        ExifLibrary.GPSAltitudeRef GPSAltitudeRef { set; get; } //AboveSeaLevel
        ExifLibrary.MathEx.UFraction32 GPSAltitude { set; get; } //77831/1003
        ExifLibrary.MathEx.UFraction32[] GPSTimeStamp { set; get; } //ExifLibrary.MathEx+UFraction32[]
        ExifLibrary.GPSDirectionRef GPSImgDirectionRef { set; get; } //TrueDirection
        ExifLibrary.MathEx.UFraction32 GPSImgDirection { set; get; } //31377/104
        ExifLibrary.Compression ThumbnailCompression { set; get; } //JPEG
        ExifLibrary.MathEx.UFraction32 ThumbnailXResolution { set; get; } //72/1
        ExifLibrary.MathEx.UFraction32 ThumbnailYResolution { set; get; } //72/1
        ExifLibrary.ResolutionUnit ThumbnailResolutionUnit { set; get; } //Inches
        System.UInt32 ThumbnailJPEGInterchangeFormat { set; get; } //850
        System.UInt32 ThumbnailJPEGInterchangeFormatLength { set; get; } //9138

    }
}
