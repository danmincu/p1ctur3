using ExifLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Telekad.Mapping;
using System.IO;

namespace P1ctur3.Exif
{
    public class ExifProcessor : P1ctur3.Exif.IExifProcessor
    {
        public ExifPropertyCollection Extract(Stream stream)
        {
            try
            {
                return ImageFile.FromStream(stream).Properties;
            }
            catch (ExifLibrary.NotValidImageFileException)
            {
                return null;
            }
            catch (ExifLibrary.NotValidJPEGFileException)
            {
                return null;
            }
            catch (System.ArgumentException e)
            {
                if (!e.Source.Contains("ExifLibrary"))
                    throw;
                else
                    return null;
            }           
        }

        public ExifPropertyCollection Extract(string fileName)
        {
            try
            {
                return ImageFile.FromFile(fileName).Properties;
            }
            catch (NotValidJPEGFileException)
            {
                return null;
            }
            catch (NotValidImageFileException)
            {
                return null;
            }
            catch (ArgumentException)
            {
                return null;
            }
        }

        public Coordinate Coordinates(ExifPropertyCollection properties)
        {
            if (!(properties.ContainsKey(ExifTag.GPSLatitude) &&
                properties.ContainsKey(ExifTag.GPSLongitude) &&
                properties.ContainsKey(ExifTag.GPSLatitudeRef) &&
                properties.ContainsKey(ExifTag.GPSLongitudeRef))) return null;

            var lat = properties[ExifTag.GPSLatitude].Value as MathEx.UFraction32[];
            var lng = properties[ExifTag.GPSLongitude].Value as MathEx.UFraction32[];
            var latitude = (lat[0].Numerator / (double)lat[0].Denominator) +
                (lat[1].Numerator / (double)(60 * lat[1].Denominator))
                + (lat[2].Numerator / (double)(3600 * lat[2].Denominator));
            latitude = (((ExifLibrary.GPSLatitudeRef)properties[ExifTag.GPSLatitudeRef].Value) == ExifLibrary.GPSLatitudeRef.South) ? -1 * latitude : latitude;

            var longitude = (lng[0].Numerator / (double)lng[0].Denominator) +
                (lng[1].Numerator / (double)(60 * lng[1].Denominator))
                + (lng[2].Numerator / (double)(3600 * lng[2].Denominator));

            longitude = (((ExifLibrary.GPSLongitudeRef)properties[ExifTag.GPSLongitudeRef].Value) == ExifLibrary.GPSLongitudeRef.West) ? -1 * longitude : longitude;

            return new Coordinate(latitude, longitude);
        }

        public DateTime? DateTime(ExifPropertyCollection properties)
        {
            if (properties == null)
                return null;
            
            if (!(properties.ContainsKey(ExifTag.DateTime) || properties.ContainsKey(ExifTag.DateTimeOriginal))) return null;

            if (properties.ContainsKey(ExifTag.DateTimeOriginal))
            {
                var dateTime = (DateTime)properties[ExifTag.DateTimeOriginal].Value;

                if (dateTime != null && dateTime > System.DateTime.MinValue)
                    return dateTime;
                else
                    return null;
            }
                        
            var xdateTime = properties[ExifTag.DateTime].Value as ExifLibrary.ExifDateTime;
            if (xdateTime == null || xdateTime == System.DateTime.MinValue)
                return null;
            return xdateTime.Value;
        }

        public ExifTag[] ExcludedTags()
        {
            return new ExifTag[] { ExifTag.GPSLatitude, ExifTag.GPSLatitudeRef, ExifTag.GPSLongitude, ExifTag.GPSLongitudeRef, ExifTag.DateTime };
        }

    }
}
