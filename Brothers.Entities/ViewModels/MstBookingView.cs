using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brothers.Entities.ViewModels
{
    public class MstBookingView
    {
        public long BookingID { get; set; }
        public long PackageID { get; set; }
        public string PackageName { get; set; }
        public DateTime ArrivalDate { get; set; }
        public DateTime DepartureDate { get; set; }

        public Int16 AdultPax { get; set; }
        public Int16 ChildPax { get; set; }
        public Int16 InfantPax { get; set; }
        public string ClientName { get; set; }
        public string ClientEmailID { get; set; }
        public string ClientContactNo { get; set; }
        public string ClientRequirement { get; set; }
        public DateTime BookingDate { get; set; }
        public string Status { get; set; }
    }
}
