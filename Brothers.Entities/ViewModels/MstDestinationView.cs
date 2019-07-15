using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brothers.Entities.ViewModels
{
    public class MstDestinationView
    {
        public long DestinationID { get; set; }
        public string DestinationName { get; set; }
        public long CountryID { get; set; }
        public string CountryName { get; set; }
        public string DestinationDesc { get; set; }

    }
}
