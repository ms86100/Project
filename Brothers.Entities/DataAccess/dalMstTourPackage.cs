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
    public class dalMstTourPackage
    {
        private BRTContext _db = new BRTContext();
        public MstTourPackageView MstTourPackageView(long id)
        {
            MstTourPackageView obj = new MstTourPackageView();
            var parPackID = new SqlParameter("@PackID", id);
            obj = _db.Database.SqlQuery<MstTourPackageView>("udspMstTourPackageView @PackID", parPackID).FirstOrDefault();
            return obj;
        }
        public List<short> GetDurationList()
        {
            List<short> duration = new List<short>();
            duration = _db.utblMstTourPackages.Select(m => m.TotalDays).Distinct().ToList();
            return duration;
        }
        public IEnumerable<MstTourPackageView> GetTopTenPackages()
        {
            List<MstTourPackageView> obj = new List<MstTourPackageView>();
            obj = _db.Database.SqlQuery<MstTourPackageView>("udspMstTopTenTourPackage").ToList();
            return obj;
        }
        public IEnumerable<MstTourPackageView> GetTourPackagePaged(int PageNo, int PageSize, out int TotalRows, string searchterm)
        {
            List<MstTourPackageView> obj = new List<MstTourPackageView>();
            //calling stored procedure to get the total result count
            var parTourName = new SqlParameter("@TourName", searchterm ?? "");
            var parStart = new SqlParameter("@Start", (PageNo - 1) * PageSize);
            var parEnd = new SqlParameter("@PageSize", PageSize);

            // setting stored procedure OUTPUT value
            // This return total number of rows, and avoid two database call for data and total number of rows 
            var spOutput = new SqlParameter
            {
                ParameterName = "@TotalCount",
                SqlDbType = System.Data.SqlDbType.BigInt,
                Direction = System.Data.ParameterDirection.Output
            };

            //calling stored procedure to get paged data.
            try
            {
                obj = _db.Database.SqlQuery<MstTourPackageView>("udspMstTourPackageGetPaged @TourName,@Start,@PageSize,@TotalCount out", parTourName, parStart, parEnd, spOutput).ToList();

            }
            catch (Exception ex)
            {
            }
            // setting total number of records
            TotalRows = int.Parse(spOutput.Value.ToString());
            return obj;
        }
        public IEnumerable<MstTourPackageView> SearchTourPackage(string destIds, string duration, string typeIds)
        {
            var parDestID = new SqlParameter("@DestID", destIds);
            parDestID.Value = destIds != null ? (object)destIds : DBNull.Value;
            var parDuration = new SqlParameter("Duration", duration);
            parDuration.Value = duration != null ? (object)duration : DBNull.Value;
            var parTypeID = new SqlParameter("@TypeID",typeIds);
            parTypeID.Value = typeIds != null ? (object)typeIds : DBNull.Value;
            List<MstTourPackageView> list = new List<MstTourPackageView>();
            try
            {
                list = _db.Database.SqlQuery<MstTourPackageView>("udspTourPackageSearch @DestID,@Duration,@TypeID", parDestID, parDuration, parTypeID).ToList();
            }
            catch(Exception ex)
            { }
            return list;
        }
        public MstTourPackageDetailsModel GetTourPackageDetailsByID(long id)
        {
            MstTourPackageDetailsModel obj = _db.utblMstTourPackages.Select(r => new MstTourPackageDetailsModel { PackageID = r.PackageID, PackageName = r.PackageName, TotalDays = r.TotalDays })
                .FirstOrDefault(r => r.PackageID == id);
            return obj;
        }
        public int Save(utblMstTourPackage tour)
        {
            int result = 0;
            if (tour.PackageID == 0)
            {
                try
                {
                    _db.utblMstTourPackages.Add(tour);
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
                utblMstTourPackage dbEntry = _db.utblMstTourPackages.Find(tour.PackageID);
                if (dbEntry != null)
                {
                    dbEntry.PackageName = tour.PackageName;
                    dbEntry.PackageRouting = tour.PackageRouting;
                    dbEntry.PickupPoint = tour.PickupPoint;
                    dbEntry.DropPoint = tour.DropPoint;
                    dbEntry.TotalDays = tour.TotalDays;
                    dbEntry.Price = tour.Price;

                    dbEntry.PackageTypeID = tour.PackageTypeID;
                }
                _db.SaveChanges();
                result = 1;
            }
            return result;
        }
        public utblMstTourPackage GetTourPackageByID(long id)
        {
            utblMstTourPackage obj = _db.utblMstTourPackages.FirstOrDefault(p => p.PackageID == id);
            return obj;
        }
        public int Delete(long id)
        {
            int result = 0;
            utblMstTourPackage obj = _db.utblMstTourPackages.Find(id);
            try
            {
                _db.utblMstTourPackages.Remove(obj);
                _db.SaveChanges();
                result = 1;
                return result;
            }
            catch (Exception ex)
            {
                return result;
            }
        }
        public int SavePackageRouting(long id, string route)
        {
            int result = 0;
            utblMstTourPackage obj = _db.utblMstTourPackages.Find(id);
            if (obj != null)
            {
                obj.PackageRouting = route;
                _db.SaveChanges();
                result = 7;
            }
            return result;
        }
        public Int16 GetTotalDays(long id)
        {
            short days = 0;
            try
            {
                days = _db.utblMstTourPackages.Where(r => r.PackageID == id).Select(r => r.TotalDays).First();
                return days;
            }
            catch (Exception ex)
            {
                return days;
            }
        }
        public void RaiseHitCount(long id)
        {
            long count = _db.utblMstTourPackages.Where(r => r.PackageID == id).Select(r => r.PackageHitCount).First();
            count++;
            utblMstTourPackage obj = _db.utblMstTourPackages.Find(id);
            if (obj != null)
            {
                try
                {
                    obj.PackageHitCount = count;
                    _db.SaveChanges();
                }
                catch (Exception ex)
                {

                }
            }
        }
    }
}
