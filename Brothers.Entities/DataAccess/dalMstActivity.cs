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
    public class dalMstActivity
    {
        private BRTContext _db = new BRTContext();
        public IEnumerable<utblMstActivity> MstActivityList
        {
            get
            {
                return _db.utblMstActivities;
            }
        }
      
        public IEnumerable<MstActivityView> GetActivityPaged(int PageNo, int PageSize, out int TotalRows, string searchterm)
        {
            List<MstActivityView> obj = new List<MstActivityView>();
            //calling stored procedure to get the total result count
            var parActName = new SqlParameter("@ActName", searchterm ?? "");
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
            obj = _db.Database.SqlQuery<MstActivityView>("udspMstActivityGetPaged @ActName,@Start,@PageSize,@TotalCount out", parActName, parStart, parEnd, spOutput).ToList();
            // setting total number of records
            TotalRows = int.Parse(spOutput.Value.ToString());
            return obj;
        }
        public int Save(utblMstActivity activity)
        {
            int result = 0;
            if (activity.ActivityID == 0)
            {
                try
                {
                    _db.utblMstActivities.Add(activity);
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
                utblMstActivity dbEntry = _db.utblMstActivities.Find(activity.ActivityID);
                if (dbEntry != null)
                {
                    dbEntry.ActivityTitle = activity.ActivityTitle;
                    dbEntry.ActivityDetails = activity.ActivityDetails;
                    dbEntry.DefaultDestinationID = activity.DefaultDestinationID;
                }
                _db.SaveChanges();
                result = 1;
            }
            return result;
        }
        public utblMstActivity GetActivityByID(long id)
        {
            utblMstActivity obj = _db.utblMstActivities.FirstOrDefault(p => p.ActivityID == id);
            return obj;
        }
        public int Delete(long id)
        {
            int result = 0;
            utblMstActivity obj = _db.utblMstActivities.Find(id);
            try
            {
                _db.utblMstActivities.Remove(obj);
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
