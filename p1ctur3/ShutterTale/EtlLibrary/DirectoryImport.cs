using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using ExifLibrary;

namespace EtlLibrary
{
    public class DirectoryImport
    {
        private string directoryPath;
        public DirectoryImport(string directoryPath)
        {
            this.directoryPath = directoryPath;
        }

        public async void Import()
        {
            string[] files = Directory.GetFiles(this.directoryPath, "*.jpg");
            System.Diagnostics.Trace.WriteLine("Files count " + files.Count().ToString());
            await Task.Run(() =>
            {
                using (var stmc = new SqlProvider.ShutterTaleDataModelContainer())
                {
                    // files = new string[] { @"\\Xeon\y\Important pictures\Poze romania 2012\poze telefon\IMG_20120803_181449.jpg" };

                    int i = 0;
                    foreach (var file in files)
                    {
                        System.Diagnostics.Trace.WriteLine(string.Format("Importing {0}/{1} {2}", i + 1, files.Count(), file));
                        var data = ImageFile.FromFile(file);
                        var image = this.FromImageFile(data, file);


                        if (data.Thumbnail != null)
                        {
                            var preview = new SqlProvider.Preview()
                            {
                                Medium = image,
                                PreviewType = string.Format("image/{0}", data.Properties.TryGet(ExifTag.ThumbnailCompression).ToString().ToLowerInvariant()),
                                Id = Guid.NewGuid(),

                            };

                            using (var ms = new MemoryStream())
                            {
                                data.Thumbnail.Save(ms);
                                //populating level one, zero is reserved for iconic image representations
                                preview.Level1 = ms.ToArray();
                            }

                            //experimental for now
                            //using (var ms = new MemoryStream())
                            //{
                            //    data.Save(ms);
                            //    //populating level 3 - full size image
                            //    preview.Level3 = ms.ToArray();
                            //}


                            stmc.Previews.Add(preview);
                        }

                        stmc.Media.Add(image);
                        if (i++ % 25 == 0)
                            stmc.SaveChanges();
                    }
                    stmc.SaveChanges();
                }
            });
        }

     

        public SqlProvider.Image FromImageFile(ImageFile imageFile, string fileName)
        {
            double? latitude = null;
            double? longitude = null;
            var properties = imageFile.Properties;
            if (properties.TryGet(ExifTag.GPSLatitude) != null && properties.TryGet(ExifTag.GPSLongitude) != null)
            {
                var lat = properties.TryGet(ExifTag.GPSLatitude).Value as MathEx.UFraction32[];
                var lng = properties.TryGet(ExifTag.GPSLongitude).Value as MathEx.UFraction32[];
                latitude = (lat[0].Numerator / (double)lat[0].Denominator) +
                   (lat[1].Numerator / (double)(60 * lat[1].Denominator))
                   + (lat[2].Numerator / (double)(3600 * lat[2].Denominator));
                latitude = (((ExifLibrary.GPSLatitudeRef)properties[ExifTag.GPSLatitudeRef].Value) == ExifLibrary.GPSLatitudeRef.South) ? -1 * latitude : latitude;

                longitude = (lng[0].Numerator / (double)lng[0].Denominator) +
                   (lng[1].Numerator / (double)(60 * lng[1].Denominator))
                   + (lng[2].Numerator / (double)(3600 * lng[2].Denominator));

                longitude = (((ExifLibrary.GPSLongitudeRef)properties[ExifTag.GPSLongitudeRef].Value) == ExifLibrary.GPSLongitudeRef.West) ? -1 * longitude : longitude;
            }

            return new SqlProvider.Image()
            {
                Id = Guid.NewGuid(),
                Location = (latitude == null || longitude == null) ? null : System.Data.Spatial.DbGeography.PointFromText(string.Format("POINT({0} {1})", longitude, latitude), 4326),
                CaptureDateTime = properties.TryGet(ExifTag.DateTime) == null ? null : properties[ExifTag.DateTime].Value as DateTime?,
                FileDateTime = DateTime.UtcNow,
                ImportDateTime = DateTime.UtcNow,
                PixelX = properties.TryGet(ExifTag.PixelXDimension) == null ? 0 : (int)((uint)properties[ExifTag.PixelXDimension].Value),
                PixelY = properties.TryGet(ExifTag.PixelYDimension) == null ? 0 : (int)((uint)properties[ExifTag.PixelYDimension].Value),
                Dpi = properties.TryGet(ExifTag.XResolution) == null ? 0 : (uint)(properties[ExifTag.XResolution] as ExifURational).Value.Numerator,
                ContentType = @"image/jpeg",
                Make = properties.TryGet(ExifTag.Make) == null ? "" : (string)properties[ExifTag.Make].Value,
                Model = properties.TryGet(ExifTag.Model) == null ? "" : (string)properties[ExifTag.Model].Value,
                SoftwareVersion = properties.TryGet(ExifTag.Software) == null ? "" : (string)properties[ExifTag.Software].Value,
                Origin = fileName,
                Quadkey18 = (latitude == null || longitude == null) ? "" : Telekad.Mapping.QuadKey.ConvertCoordinateToQuadKey(new Telekad.Mapping.Coordinate((double)latitude, (double)longitude), 18).ToString(),
                Size = 0
            };
        }

    }

    public static class ExifPropertyCollectionExtension
    {
        public static ExifProperty TryGet(this ExifPropertyCollection collection, ExifTag tag)
        {
            ExifProperty result = null;
            collection.TryGetValue(tag, out result);
            return result;
        }
    }
}
