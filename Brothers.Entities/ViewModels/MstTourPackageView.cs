using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brothers.Entities.ViewModels
{
    public class MstTourPackageView
    {
        public long PackageID { get; set; }
        public string PackageName { get; set; }
        public string PackageRouting { get; set; }
        public string PickupPoint { get; set; }
        public string DropPoint { get; set; }
        public Int16 TotalDays { get; set; }

        public double Price { get; set; }
        public long PackageTypeID { get; set; }
        public string PackageTypeName { get; set; }
        public string PackageRemarks { get; set; }
        public string DefaultPhoto { get; set; }
        public long PackageHitCount { get; set; }
    }
}
