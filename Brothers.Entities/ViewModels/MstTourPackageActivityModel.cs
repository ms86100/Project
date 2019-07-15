using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Brothers.Entities.Models;

namespace Brothers.Entities.ViewModels
{
    public class MstTourPackageActivityModel
    {
        public utblMstTourPackageActivity MstTourPackageActivity { get; set; }
        public MstTourPackageDetailsModel MstTourPackage { get; set; }
        public IEnumerable<utblMstDestination> MstDestDropDownList { get; set; }
        public IEnumerable<utblMstActivity> MstActDropDownList { get; set; }
        public IEnumerable<MstTourPackageActivityView> MstTourPackageActivityList { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}
