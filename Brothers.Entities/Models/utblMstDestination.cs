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
    public class utblMstDestination
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public long DestinationID { get; set; }
        [Required(ErrorMessage="Please enter the destination name")]
        [StringLength(256)]
        [DisplayName("Destination Name")]
        public string DestinationName { get; set; }
        [StringLength(2048)]
        [DisplayName("Destination Description")]
        public string DestinationDesc { get; set; }
        [Required(ErrorMessage="Select a country")]
        [DisplayName("Country")]
        public long CountryID { get; set; }
    }
}
