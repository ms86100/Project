using Brothers.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brothers.Entities.DataAccess
{
    public class dalTourPackageMap
    {
        private BRTContext _db = new BRTContext();
        public int Save(utblMstTourPackageMap map)
        {
            int result = 0;
            if(map.TourMapID==0)
            {
                try 
                {
                    _db.utblMstTourPackageMaps.Add(map);
                    _db.SaveChanges();
                    result = 1;
                }
               catch(Exception ex)
                {
                    result = 0;
                }
            }
            else
            {
                utblMstTourPackageMap obj = _db.utblMstTourPackageMaps.Find(map.TourMapID);
                if(obj!=null)
                {
                    obj.MapNormal = map.MapNormal;
                    obj.MapThumb = map.MapThumb;
                }
                _db.SaveChanges();
                result = 1;
            }
            return result;
        }
        public utblMstTourPackageMap GetTourMapByID(long packid)
        {
            utblMstTourPackageMap obj = _db.utblMstTourPackageMaps.Where(r => r.PackageID == packid).FirstOrDefault();
            return obj;
        }
        public int Delete(long id)
        {
            int result = 0;
            try
            {
                _db.utblMstTourPackageMaps.RemoveRange(_db.utblMstTourPackageMaps.Where(m => m.PackageID == id));
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
