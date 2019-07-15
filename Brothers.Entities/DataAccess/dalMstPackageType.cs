using Brothers.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brothers.Entities.DataAccess
{
    public class dalMstPackageType
    {
        private BRTContext _db = new BRTContext();
        public IEnumerable<utblMstPackageType> MstPackageList
        {
            get
            {
                return _db.utblMstPackageTypes;
            }
        }
        public IEnumerable<utblMstPackageType> GetPackagePaged(int PageNo, int PageSize, out int TotalRows, string searchterm)
        {
            List<utblMstPackageType> obj = new List<utblMstPackageType>();
            obj = _db.utblMstPackageTypes.OrderBy(x => x.PackageTypeName).Where(x => searchterm == null || x.PackageTypeName.Contains(searchterm)).Skip((PageNo - 1) * PageSize).Take(PageSize).ToList();
            if (searchterm == null)
            {
                TotalRows = _db.utblMstPackageTypes.Count();
            }
            else
            {
                TotalRows = _db.utblMstPackageTypes.Where(x=>x.PackageTypeName.Contains(searchterm)).Count();
            }
            return obj;
        }
        public int Save(utblMstPackageType package)
        {
            int result = 0;
            if (package.PackageTypeID == 0)
            {
                try
                {
                    _db.utblMstPackageTypes.Add(package);
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
                utblMstPackageType dbEntry = _db.utblMstPackageTypes.Find(package.PackageTypeID);
                if (dbEntry != null)
                {
                    dbEntry.PackageTypeName = package.PackageTypeName;
                    dbEntry.PackageTypeDesc = package.PackageTypeDesc;
                }
                _db.SaveChanges();
                result = 1;
            }

            return result;
        }
        public utblMstPackageType GetPackageByID(long id)
        {
            utblMstPackageType obj = _db.utblMstPackageTypes.FirstOrDefault(p => p.PackageTypeID == id);
            return obj;
        }
        public int Delete(long id)
        {
            int result = 0;
            utblMstPackageType obj = _db.utblMstPackageTypes.Find(id);
            try
            {
                _db.utblMstPackageTypes.Remove(obj);
                _db.SaveChanges();
                result = 1;
                return result;
            }
            catch (Exception ex)
            {
                return result;
            }
        }
    }
}
