using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brothers.Entities.ViewModels
{
    public class MstBookingViewModel
    {
        public IEnumerable<MstBookingView> MstBookingList { get; set; }
        public PagingInfo pagingInfo { get; set; }
    }
}
