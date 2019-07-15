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
    public class utblMstTourPackageActivity
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public long PackageActivityID { get; set; }
        [DisplayName("Package Name")]
        public long PackageID { get; set; }
        [Required(ErrorMessage="Select an activity")]
        [DisplayName("Activity Name")]
        public long ActivityID { get; set; }
        [Required(ErrorMessage="Select a Day")]
        [DisplayName("Day")]
        public Int16 DayNo { get; set; }
        [DisplayName("Overnight Destination")]
        public long? OvernightDestinationID { get; set; }
        [StringLength(128)]
        [DisplayName("New Overnight Destination")]
        public string OvernightDestination { get; set; }
        [DisplayName("Activity Remarks")]
        public string ActivityRemarks { get; set; }
    }
}
