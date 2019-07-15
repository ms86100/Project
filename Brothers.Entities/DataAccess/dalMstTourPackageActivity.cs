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
    public class dalMstTourPackageActivity
    {
        private BRTContext _db = new BRTContext();
        public IEnumerable<MstTourPackageActivityView> MstTourPackageActivityList(long id)
        {
            List<MstTourPackageActivityView> obj = new List<MstTourPackageActivityView>();
            var parPackID = new SqlParameter("@PackID", id);
            obj = _db.Database.SqlQuery<MstTourPackageActivityView>("udspMstTourActivity @PackID", parPackID).ToList();
            return obj;
        }
        public IEnumerable<MstTourPackageActivityView> GetTourActivityPaged(int PageNo, int PageSize, out int TotalRows, long id)
        {
            List<MstTourPackageActivityView> obj = new List<MstTourPackageActivityView>();
            //calling stored procedure to get the total result count
            var parPackID = new SqlParameter("@PackID", id);
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
            obj = _db.Database.SqlQuery<MstTourPackageActivityView>("udspMstTourActivityGetPaged @PackID,@Start,@PageSize,@TotalCount out", parPackID, parStart, parEnd, spOutput).ToList();
            // setting total number of records
            TotalRows = int.Parse(spOutput.Value.ToString());
            return obj;
        }
        public int Save(utblMstTourPackageActivity activity)
        {
            int result = 0;
            if (activity.PackageActivityID == 0)
            {
                try
                {
                    _db.utblMstTourPackageActivities.Add(activity);
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
                utblMstTourPackageActivity dbEntry = _db.utblMstTourPackageActivities.Find(activity.PackageActivityID);
                if (dbEntry != null)
                {
                    dbEntry.ActivityID = activity.ActivityID;
                    dbEntry.OvernightDestinationID = activity.OvernightDestinationID;
                    dbEntry.OvernightDestination = activity.OvernightDestination;
                    dbEntry.ActivityRemarks = activity.ActivityRemarks;
                }
                _db.SaveChanges();
                result = 1;
            }
            return result;
        }
        public utblMstTourPackageActivity GetTourPackageActivityByID(long id)
        {
            utblMstTourPackageActivity obj = _db.utblMstTourPackageActivities.FirstOrDefault(p => p.PackageActivityID == id);
            return obj;
        }
        public Int16 GetDayNo(long id)
        {
            short day = 0;
            try
            {
                day = _db.utblMstTourPackageActivities.OrderByDescending(r => r.DayNo).Where(r => r.PackageID == id).Select(r => r.DayNo).First();
                return day;
            }
            catch (Exception ex)
            {
                return day;
            }

        }
        public int ManageDayLost(short totalDays, short newDays, long id)
        {
            int result = 0;
            utblMstTourPackageActivity obj = _db.utblMstTourPackageActivities.Where(R => R.DayNo == totalDays && R.PackageID == id).FirstOrDefault();
            utblMstTourPackageActivity dbentry = _db.utblMstTourPackageActivities.Where(r => r.DayNo == newDays && r.PackageID == id).First();
            int i = Convert.ToInt32(newDays + 1);
            try
            {
                dbentry.ActivityID = obj.ActivityID;
                dbentry.ActivityRemarks = obj.ActivityRemarks;
                dbentry.OvernightDestinationID = obj.OvernightDestinationID;
                for (int j = i; j <= totalDays; j++)
                {
                    utblMstTourPackageActivity removeEntry = _db.utblMstTourPackageActivities.Where(r => r.DayNo == j && r.PackageID == id).First();
                    _db.utblMstTourPackageActivities.Remove(removeEntry);
                }
                _db.SaveChanges();
                result = 1;
            }
            catch (Exception ex)
            {
                result = 2;
            }
            return result;
        }
        public Int16 GetTotalDayList(long id)
        {
            short total = 0;
            try
            {
                int to = _db.utblMstTourPackageActivities.Where(r => r.PackageID == id).Select(r => r.DayNo).Count();
                total = Convert.ToInt16(to);
                return total;
            }
            catch (Exception ex)
            {
                return total;
            }
        }
        public int Delete(long packid)
        {
            int result = 0;
            try
            {
                _db.utblMstTourPackageActivities.RemoveRange(_db.utblMstTourPackageActivities.Where(m => m.PackageID == packid));
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
