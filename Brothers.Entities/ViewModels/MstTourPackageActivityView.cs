using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brothers.Entities.ViewModels
{
    public class MstTourPackageActivityView
    {
        public long PackageActivityID { get; set; }
        public long PackageID { get; set; }
        public string PackageName { get; set; }
        public long ActivityID { get; set; }
        public string ActivityTitle { get; set; }
        public Int16 DayNo { get; set; }
        public long? DestinationID { get; set; }
        public string DestinationName { get; set; }
        public string OvernightDestination { get; set; }
        public string ActivityRemarks { get; set; }
    }
}
