using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Brothers.Entities.Models;

namespace Brothers.Entities.ViewModels
{
    public class MstTourPackageGeneralViewAndSearch
    {
        public IEnumerable<utblMstDestination> MstDestinationList { get; set; }
        public IEnumerable<utblMstPackageType> MstPackageTypeList { get; set; }
        public IEnumerable<MstTourPackageView> MstTourPackageList { get; set; }
        public List<short> MstDurationList { get; set; }

        public IEnumerable<BlogView> BlogList { get; set; }


    }
}
