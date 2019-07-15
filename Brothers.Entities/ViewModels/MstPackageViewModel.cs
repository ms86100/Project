using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Brothers.Entities.Models;

namespace Brothers.Entities.ViewModels
{
    public class MstPackageViewModel
    {
        public IEnumerable<utblMstPackageType> MstPackageList { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}
