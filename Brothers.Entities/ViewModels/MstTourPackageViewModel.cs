using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brothers.Entities.ViewModels
{
    public class MstTourPackageViewModel
    {
        public IEnumerable<MstTourPackageView> MstTourList { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}
