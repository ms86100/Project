using Brothers.Entities.Models;
using Brothers.Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brothers.Entities.ViewModels
{
    public class MstEnquiryViewModel
    {
        public IEnumerable<utblGenEnquiry> MstEnquiryList { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}
