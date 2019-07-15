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
    public class dalTourPackageBooking
    {
        private BRTContext _db = new BRTContext();
        public int Save(utblTourPackageBooking book)
        {
            int result = 0;
            if(book.BookingID==0)
            {
                try
                {
                    _db.utblTourPackageBookings.Add(book);
                    _db.SaveChanges();
                    result = 1;
                }
               catch(Exception ex)
                {
                    result = 0;
                }
            }
            utblTourPackageBooking dbentry = _db.utblTourPackageBookings.Find(book.BookingID);
            if(dbentry!=null)
            {
                dbentry.Status = book.Status;
                _db.SaveChanges();
                result = 1;
            }
            return result;
        }
        public IEnumerable<MstBookingView> GetBookingPaged(int PageNo, int PageSize, out int TotalRows, string searchterm)
        {
            List<MstBookingView> obj = new List<MstBookingView>();
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
            obj = _db.Database.SqlQuery<MstBookingView>("udspBookingGetPaged @TourName,@Start,@PageSize,@TotalCount out", parTourName, parStart, parEnd, spOutput).ToList();
            // setting total number of records
            TotalRows = int.Parse(spOutput.Value.ToString());
            return obj;
        }
        public utblTourPackageBooking GetBookingByID(long id)
        {
            utblTourPackageBooking obj = new utblTourPackageBooking();
            obj = _db.utblTourPackageBookings.Where(r => r.BookingID == id).FirstOrDefault();
            return obj;
        }
    }
}
