using Brothers.Entities.Models;
using Brothers.Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brothers.Entities.DataAccess
{
    public class dalMstTourPackagePhoto
    {
        private BRTContext _db = new BRTContext();
        public IEnumerable<MstTourPhotoView> MstTourPackagePhotoList(long id)
        {
            List<MstTourPhotoView> photo = new List<MstTourPhotoView>();
            var parPackID = new SqlParameter("@PackID", id);
            photo = _db.Database.SqlQuery<MstTourPhotoView>("udspMstTourPhotoList @PackID", parPackID).ToList();
            return photo;
        }
        public IEnumerable<MstTourPhotoView> GetTourPackagePhotoPaged(int PageNo, int PageSize, out int TotalRows, long id)
        {
            List<MstTourPhotoView> obj = new List<MstTourPhotoView>();
            var parPackID = new SqlParameter("@PackID", id);
            var parStart = new SqlParameter("@Start", (PageNo - 1) * PageSize);
            var parEnd = new SqlParameter("@PageSize", PageSize);
            var spOutput = new SqlParameter
            {
                ParameterName = "@TotalCount",
                SqlDbType = System.Data.SqlDbType.BigInt,
                Direction = System.Data.ParameterDirection.Output
            };
            obj = _db.Database.SqlQuery<MstTourPhotoView>("udspMstTourPhotoGetPaged @PackID,@Start,@PageSize,@TotalCount out", parPackID, parStart, parEnd, spOutput).ToList();
            //obj = _db.utblMstTourPackagePics.Where(r => r.PackageID == id).OrderBy(r => r.PhotoDescription).Skip((PageNo - 1) * PageSize).Take(PageSize).ToList();
            TotalRows = int.Parse(spOutput.Value.ToString());
            //TotalRows = _db.utblMstTourPackagePics.Where(r => r.PackageID == id).Count();
            return obj;
        }
        public int Save(utblMstTourPackagePic photo)
        {
            int result = 0;
            if (photo.PackagePhotoID == 0)
            {
                try
                {
                    _db.utblMstTourPackagePics.Add(photo);
                    _db.SaveChanges();
                    result = 1;
                }
                catch (Exception ex)
                {
                    result = 0;
                }
            }
            else
            {
                utblMstTourPackagePic dbEntry = _db.utblMstTourPackagePics.Find(photo.PackagePhotoID);
                if (dbEntry != null)
                {
                    dbEntry.PhotoNormal = photo.PhotoNormal;
                    dbEntry.PhotoThumb = photo.PhotoThumb;
                    dbEntry.PhotoDescription = photo.PhotoDescription;
                }
                _db.SaveChanges();
                result = 1;
            }
            return result;
        }
        public int Delete(long id)
        {
            int result = 0;
            utblMstTourPackagePic obj = _db.utblMstTourPackagePics.Find(id);
            try
            {
                _db.utblMstTourPackagePics.Remove(obj);
                _db.SaveChanges();
                result = 1;
                return result;
            }
            catch (Exception ex)
            {
                return result;
            }
        }
        public int DeleteByPackageID(long packid)
        {
            int result = 0;
            try
            {
                _db.utblMstTourPackagePics.RemoveRange(_db.utblMstTourPackagePics.Where(m => m.PackageID == packid));
                _db.SaveChanges();
                result = 1;
                return result;
            }
            catch (Exception ex)
            {
                return result;
            }
        }
        public utblMstTourPackagePic GetPhotoDetailsByID(long id)
        {
            utblMstTourPackagePic obj = _db.utblMstTourPackagePics.FirstOrDefault(m => m.PackagePhotoID == id);
            return obj;
        }
        public void MakeDefaultPhoto(long id, long packid)
        {

            utblMstTourPackagePic obj = _db.utblMstTourPackagePics.Find(id);
            utblMstTourPackagePic objOldPic = _db.utblMstTourPackagePics.Where(x => x.PackageID == packid && x.IsDefault == true).FirstOrDefault();
            if (objOldPic != null)
            {
                objOldPic.IsDefault = false;
            }
            obj.IsDefault = true;
            _db.SaveChanges();
        }
        public utblMstTourPackagePic GetDefaultPhoto(long packid)
        {
            utblMstTourPackagePic obj = _db.utblMstTourPackagePics.Where(x => x.PackageID == packid && x.IsDefault == true).FirstOrDefault();
            return obj;
        }
    }
}
