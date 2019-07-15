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
    public class utblMstCountry
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public long CountryID { get; set; }
        [Required(ErrorMessage="Country Name is required.")]
        [StringLength(128)]
        [DisplayName("Country Name")]
        public string CountryName { get; set; }
        [Required(ErrorMessage = "Nationality Name is required.")]
        [StringLength(128)]
        [DisplayName("Nationality")]
        public string NationalityName { get; set; }
    }
}
