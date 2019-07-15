using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Brothers.Entities.Models;

namespace Brothers.Entities.ViewModels
{
    public class MstTourPackageManageModel
    {
        public utblMstTourPackage MstTourPackage { get; set; }
        public IEnumerable<utblMstPackageType> MstPackageDropDown { get; set; }
    }
}
