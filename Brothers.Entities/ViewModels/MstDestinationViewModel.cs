using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Brothers.Entities.Models;

namespace Brothers.Entities.ViewModels
{
    public class MstDestinationViewModel
    {
        public IEnumerable<MstDestinationView> MstDestinationList { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}
