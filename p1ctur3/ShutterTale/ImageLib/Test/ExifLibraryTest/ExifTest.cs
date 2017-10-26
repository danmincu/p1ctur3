using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ExifLibrary;
using System.Text;
using System.IO;
using System.Linq;

namespace ExifLibraryTest
{
    [TestClass]
    public class ExifTest
    {
        [TestMethod]
        public void TestComputeDecimalLatandLog()
        {
            var data = ImageFile.FromFile(@"img_0160.jpg");
            var properties = data.Properties;
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


            var googlelink = string.Format(@"https://maps.google.ca/?ll={0},{1}&spn=1.363259,2.999268&t=h&z=19", latitude, longitude);
        }

        [TestMethod]
        public void TestSerializeExifInfo()
        {
            var data = ImageFile.FromFile(@"img_0160.jpg");
            var properties = data.Properties;

            var myCollection = from property in properties
                               select new { Tag = property.Key.ToString(), TypeOfValue = property.Value.GetType().ToString(), Value = property.Value.ToString() };



            System.Xml.Serialization.XmlSerializer x = new System.Xml.Serialization.XmlSerializer(typeof(ExifPropertyCollection));

            using (var stream = new FileStream("Exif.xml", FileMode.OpenOrCreate))
            {
                x.Serialize(stream, properties);
                stream.Close();
            }

        }

        [TestMethod]
        public void TestMissingLatandLog()
        {
            var data = ImageFile.FromFile(@"NoExif.jpg");
            var properties = data.Properties;
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


            var googlelink = string.Format(@"https://maps.google.ca/?ll={0},{1}&spn=1.363259,2.999268&t=h&z=19", latitude, longitude);
        }



        [TestMethod]
        public void TestNoExifUseCase()
        {
            var data = ImageFile.FromFile(@"NoExif.bmp");
            var properties = data.Properties;

            var myCollection = from property in properties
                               select new { Tag = property.Key.ToString(), TypeOfValue = property.Value.GetType().ToString(), Value = property.Value.ToString() };



            System.Xml.Serialization.XmlSerializer x = new System.Xml.Serialization.XmlSerializer(typeof(ExifPropertyCollection));

            using (var stream = new FileStream("Exif.xml", FileMode.OpenOrCreate))
            {
                x.Serialize(stream, properties);
                stream.Close();
            }

        }




        [TestMethod]
        public void TestComputeDecimalLatandLog1()
        {
            var data = ImageFile.FromFile(@"img_0160.jpg");
            var properties = data.Properties;
            StringBuilder s = new StringBuilder();
            //foreach (var key in properties.Keys)
            //{
            //    s.AppendLine(key + "=" + properties[key].Value + "    " + properties[key].Value.GetType().ToString());
            //}

            foreach (var key in properties.Keys)
            {
                s.AppendLine(properties[key].Value.GetType().ToString() + "  " + key + "{ set; get; } //" + properties[key].Value);
            }



            using (var f = new StreamWriter(@"allprop.txt"))
            {
                f.Write(s.ToString());
            }


            var ss = s.ToString();
        }
    }
}
