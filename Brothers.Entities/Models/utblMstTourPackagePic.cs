using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Brothers.Entities.Models
{
   public class utblMstTourPackagePic
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public long PackagePhotoID { get; set; }
        [Required]
        public long PackageID { get; set; }
        [Required(ErrorMessage = "Select photo for Tour Package")]
        public string PhotoNormal { get; set; }
        [Required(ErrorMessage = "Select photo for Tour Package")]
        public string PhotoThumb { get; set; }
        [StringLength(1024)]
        [DisplayName("Photo Description")]
        public string PhotoDescription { get; set; }
        [Required(ErrorMessage = "Make a default photo")]
        public bool IsDefault { get; set; }
    }
}
