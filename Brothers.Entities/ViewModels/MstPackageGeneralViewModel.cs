using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Brothers.Entities.Models;

namespace Brothers.Entities.ViewModels
{
    public class MstPackageGeneralViewModel
    {
        public MstTourPackageView MstTourPackage { get; set; }
        public IEnumerable<MstTourPackageActivityView> MstTourActivityList { get; set; }
        public IEnumerable<MstTourPhotoView> MstTourPhotoList { get; set; }
        public utblTourPackageBooking MstPackageBooking { get; set; }
        public utblMstTourPackageMap MstTourMap { get; set; }
    }
}
