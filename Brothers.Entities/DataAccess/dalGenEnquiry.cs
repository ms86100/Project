using Brothers.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brothers.Entities.DataAccess
{
    public class dalGenEnquiry
    {
        private BRTContext _db = new BRTContext();

        public IEnumerable<utblGenEnquiry> MstEnquiryList
        {
            get
            {
                return _db.utblGenEnquiries;
            }
        }
        public IEnumerable<utblGenEnquiry> GetEnquiryPaged(DateTime? EnquiryDate, int PageNo, int PageSize, out int TotalRows)
        {
            List<utblGenEnquiry> obj = new List<utblGenEnquiry>();
            obj = EnquiryDate != null ? _db.utblGenEnquiries.Where(x => x.EnquiryDate == EnquiryDate).OrderByDescending(x => x.EnquiryDate).Skip((PageNo - 1) * PageSize).Take(PageSize).ToList() : _db.utblGenEnquiries.OrderByDescending(x => x.EnquiryDate).Skip((PageNo - 1) * PageSize).Take(PageSize).ToList();
            //obj = _db.utblGenEnquiries.Where(x=>x.EnquiryDate==EnquiryDate).OrderByDescending(x => x.EnquiryDate).Skip((PageNo - 1) * PageSize).Take(PageSize).ToList();
            TotalRows = _db.utblGenEnquiries.Count();
            return obj;
        }
        public int Save(utblGenEnquiry Enquiry)
        {
            int result = 0;
            if (Enquiry.EnquiryID == 0)
            {
                try
                {
                    _db.utblGenEnquiries.Add(Enquiry);
                    result = 1;
                }
                catch (Exception ex)
                {
                    result = 0;
                }
            }
            else
            {
                utblGenEnquiry dbEntry = _db.utblGenEnquiries.Find(Enquiry.EnquiryID);
                if (dbEntry != null)
                {
                    dbEntry.GuestName = Enquiry.GuestName;
                    dbEntry.GuestEmailID = Enquiry.GuestEmailID;
                    dbEntry.EnquiryDetails = Enquiry.EnquiryDetails;
                    dbEntry.EnquiryStatus = Enquiry.EnquiryStatus;
                    dbEntry.EnquiryRepliedOn = Enquiry.EnquiryRepliedOn;
                    dbEntry.EnquiryRepliedBy = Enquiry.EnquiryRepliedBy;
                }
                result = 1;
            }
            _db.SaveChanges();
            return result;
        }
        public utblGenEnquiry GetEnquiryByID(long id)
        {
            utblGenEnquiry obj = _db.utblGenEnquiries.FirstOrDefault(p => p.EnquiryID == id);
            return obj;
        }
    }
}
