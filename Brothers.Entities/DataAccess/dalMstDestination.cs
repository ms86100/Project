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
    public class dalMstDestination
    {
        private BRTContext _db = new BRTContext();

        public IEnumerable<utblMstDestination> MstDestinationList
        {
            get
            {
                return _db.utblMstDestinations.Where(x => x.DestinationID != 5);
            }
        }
        public IEnumerable<utblMstDestination> MstAllDestinations
        {
            get
            {
                return _db.utblMstDestinations;
            }
        }
        public IEnumerable<MstDestinationView> GetDestinationPaged(int PageNo, int PageSize, out int TotalRows, string searchterm)
        {
            List<MstDestinationView> obj = new List<MstDestinationView>();
            //calling stored procedure to get the total result count
            var parDestName = new SqlParameter("@DestName", searchterm ?? "");
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
            obj = _db.Database.SqlQuery<MstDestinationView>("udspMstDestinationGetPaged @DestName,@Start,@PageSize,@TotalCount out", parDestName, parStart, parEnd, spOutput).ToList();
            // setting total number of records
            TotalRows = int.Parse(spOutput.Value.ToString());
            return obj;
        }
        public int Save(utblMstDestination destination)
        {
            int result = 0;
            if (destination.DestinationID == 0)
            {
                try
                {
                    _db.utblMstDestinations.Add(destination);
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
                utblMstDestination dbEntry = _db.utblMstDestinations.Find(destination.DestinationID);
                if (dbEntry != null)
                {
                    dbEntry.DestinationName = destination.DestinationName;
                    dbEntry.CountryID = destination.CountryID;
                    dbEntry.DestinationDesc = destination.DestinationDesc;
                }
                _db.SaveChanges();
                result = 1;
            }
            return result;
        }
        public utblMstDestination GetDestinationByID(long id)
        {
            utblMstDestination obj = _db.utblMstDestinations.FirstOrDefault(p => p.DestinationID == id);
            return obj;
        }
        public int Delete(long id)
        {
            int result = 0;
            utblMstDestination obj = _db.utblMstDestinations.Find(id);
            try
            {
                _db.utblMstDestinations.Remove(obj);
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
