using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brothers.Entities.ViewModels
{
    public class MstActivityView
    {
        public long ActivityID { get; set; }
        public string ActivityTitle { get; set; }
        public long DestinationID { get; set; }
        public string DestinationName { get; set; }
        public string ActivityDetails { get; set; }
    }
}
