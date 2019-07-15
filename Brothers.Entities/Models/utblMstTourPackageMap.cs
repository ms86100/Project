using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Brothers.Entities.Models
{
    public class utblMstTourPackageMap
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public long TourMapID { get; set; }
        [Required]
        public long PackageID { get; set; }
        public string MapThumb { get; set; }
        public string MapNormal { get; set; }
    }
}
