using ExifLibrary;
using P1ctur3.Exif;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P1ctur3.Types
{
    public class ImageProcessor
    {
        IExifProcessor exifProcessor;
        public ImageProcessor(IExifProcessor exifProcessor)
        {
            this.exifProcessor = exifProcessor;
        }

        public ImageMetadata Image(Stream stream, string originalFileName, Guid? collectionId)
        {
            return this.Image(stream, null, originalFileName, System.Environment.MachineName + "@" + Path.GetDirectoryName(originalFileName), collectionId);
        }

        public ImageMetadata Image(Stream stream, DateTime? fileCreationDate, string originalFileName, string fullPath, Guid? collectionId)
        {
            var properties = this.exifProcessor.Extract(stream);
            var imageMetadata = FromExifPropertyCollection(properties, fileCreationDate, originalFileName, collectionId);
            imageMetadata.FullPath = fullPath;
            return imageMetadata;
        }

        public ImageMetadata Image(String fileName, Guid? collectionId)
        {
            var properties = this.exifProcessor.Extract(fileName);
            var fileDate = System.IO.File.GetCreationTime(fileName);
            var imageMetadata = FromExifPropertyCollection(properties, fileDate, fileName, collectionId);
            imageMetadata.FullPath = System.Environment.MachineName + "@" + Path.GetDirectoryName(fileName);
            return imageMetadata;
        }

        private ImageMetadata FromExifPropertyCollection(ExifPropertyCollection properties, DateTime? fileDate, string fileName, Guid? collectionId)
        {
            return new ImageMetadata()
            {
                Id = Guid.NewGuid(),
                DateTime = this.exifProcessor.DateTime(properties),
                FileDateTime = fileDate,
                ImportDateTime = DateTime.UtcNow,
                OriginalFileName = fileName,
                Coordinate = properties == null ? null : this.exifProcessor.Coordinates(properties),
                Tags = properties == null ? null : properties.Where(p => !this.exifProcessor.ExcludedTags().Contains(p.Key)).ToDictionary(p => p.Key.ToString(), p => p.Value.Value.ToString()),
                CollectionId = collectionId
            };
        }
    }
}
