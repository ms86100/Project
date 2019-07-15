using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Brothers.Entities.Models;

namespace Brothers.Entities.ViewModels
{
    public class MstBookingExportModel
    {
        public MstTourPackageView MstTourPackages { get; set; }
        public IEnumerable<MstTourPackageActivityView> MstTourActivityList { get; set; }
        public utblTourPackageBooking MstTourBooking { get; set; }
    }
}
