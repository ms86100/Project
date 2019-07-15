using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Brothers.Entities.Models;

namespace Brothers.Entities.ViewModels
{
    public class MstTourPackageMapModel
    {
        public utblMstTourPackageMap MstTourPackageMap { get; set; }
        public MstTourPackageDetailsModel MstTourPackage { get; set; }
    }
}
