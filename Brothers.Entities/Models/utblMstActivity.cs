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
    public class utblMstActivity
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public long ActivityID { get; set; }
        [Required(ErrorMessage="Please enter the Activity Title")]
        [StringLength(512)]
        [DisplayName("Activity Title")]
        public string ActivityTitle { get; set; }
        [Required(ErrorMessage="Enter the activity details")]
        [StringLength(2048)]
        [DisplayName("Activity Details")]
        public string ActivityDetails { get; set; }
        [Required(ErrorMessage="Select a destination")]
        [DisplayName("Destination")]
        public long DefaultDestinationID { get; set; }
    }
}
