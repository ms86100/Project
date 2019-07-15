using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Brothers.Entities.Models;

namespace Brothers.Entities.ViewModels
{
    public class MstTourPackagePhotoModel
    {
        public utblMstTourPackagePic MstTourPackagePhoto { get; set; }
        public MstTourPackageDetailsModel MstTourPackage { get; set; }
        public IEnumerable<MstTourPhotoView> MstTourPackagePhotoList { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}
