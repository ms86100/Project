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
    public class utblMstPackageType
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public long PackageTypeID { get; set; }
        [Required(ErrorMessage="Enter the package type name")]
        [StringLength(128)]
        [DisplayName("Package Type Name")]
        public string PackageTypeName { get; set; }
        [StringLength(2048)]
        [DisplayName("Package Type Description")]
        public string PackageTypeDesc { get; set; }
    }
}
