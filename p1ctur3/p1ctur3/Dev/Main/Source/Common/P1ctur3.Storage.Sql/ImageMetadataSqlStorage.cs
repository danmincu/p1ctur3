using P1ctur3.Types;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P1ctur3.Storage.Sql
{
    public class ImageMetadataSqlStorage : IImageMetadataStorage
    {
        private RemoteInfo RemoteInfo(string email)
        {
            using (var context = new ImageMetadataContext())
            {
                var lowerEmail = email.ToLowerInvariant();
                var ri = context.RemoteInfos.Where(x => x.Email == lowerEmail).FirstOrDefault();

                if (ri == null)
                {
                    throw new Exception("Email has not been registered");
                }
                else
                    return ri;
            }
        }

        public void Save(P1ctur3.Types.RemoteInfo remoteInfo)
        {
            using (var context = new ImageMetadataContext())
            {
                context.RemoteInfos.Add(remoteInfo);
                context.SaveChanges();
            }
        }

        public bool IsEmailRegistered(string email)
        {
            using (var context = new ImageMetadataContext())
            {
                var lowerEmail = email.ToLowerInvariant();
                return context.RemoteInfos.Any(ri => ri.Email == lowerEmail);
            }
        }

        public void Save(P1ctur3.Types.ImageMetadata imageMetadata, string email)
        {
            using (var context = new ImageMetadataContext())
            {
                imageMetadata.RemoteInfo = this.RemoteInfo(email);
                context.Entry(imageMetadata.RemoteInfo).State = EntityState.Unchanged;
                context.Images.Add(imageMetadata);
                context.SaveChanges();
            }
        }

        public P1ctur3.Types.ImageMetadata Get(Guid imageMetadataId)
        {
            using (var context = new ImageMetadataContext())
            {
                return context.Images.Where(imdt => imdt.Id == imageMetadataId).FirstOrDefault();
            }
        }

        public P1ctur3.Types.RemoteInfo Get(string email)
        {
            using (var context = new ImageMetadataContext())
            {
                var lowerEmail = email.ToLowerInvariant();
                return context.RemoteInfos.Where(ri => ri.Email == lowerEmail).FirstOrDefault();
            }
        }

        public IEnumerable<P1ctur3.Types.ImageMetadata> Get(IImageMetadataFilter filter)
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, int> UniqueFolders(string machineName)
        {
            using (var context = new ImageMetadataContext())
            {
             var resultSet = context.Database.SqlQuery(typeof(object), string.Format(@"select count(fullPath), fullPath from ImageMetadatas
                    where fullPath like '{0}%'
                    group by fullPath", machineName));
             return new Dictionary<string, int>();    
            }

        }

    }


    public class ImageMetadataContext : DbContext
    {

        public ImageMetadataContext()
            : base("name=ImageMetadataContext")
        { }

        public DbSet<ImageMetadata> Images { set; get; }
        public DbSet<RemoteInfo> RemoteInfos { set; get; }

    }
}
