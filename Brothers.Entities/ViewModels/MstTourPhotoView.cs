using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brothers.Entities.ViewModels
{
    public class MstTourPhotoView
    {
        public long PackagePhotoID { get; set; }
        public long PackageID { get; set; }
        public string PhotoThumb { get; set; }
        public string PhotoDescription { get; set; }
        public bool IsDefault { get; set; }

    }
}
