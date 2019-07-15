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
    public class utblTourPackageBooking
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public long BookingID { get; set; }
        [Required]
        public long PackageID { get; set; }
        [Required(ErrorMessage="Please enter arrival date")]
        [DataType(DataType.DateTime, ErrorMessage = "Invalid date")]
        [DateGreaterThanToday]
        public DateTime ArrivalDate { get; set; }


        [Required(ErrorMessage = "Please enter departure date")]
        [DataType(DataType.DateTime, ErrorMessage = "Invalid date")]
        [DateGreaterThanToday]
        public DateTime DepartureDate { get; set; }
        [Required(ErrorMessage="Select a number")]
        [Range(1,10)]
        public Int16 AdultPax { get; set; }
        [Required(ErrorMessage = "Select a number")]
        [Range(0,10)]
        public Int16 ChildPax { get; set; }
        [Required(ErrorMessage = "Select a number")]
        [Range(0,10)]
        public Int16 InfantPax { get; set; }
        [Required(ErrorMessage="Please enter your Name")]
        [StringLength(128)]
        public string ClientName { get; set; }
        [Required(ErrorMessage="Please enter your Email ID")]
        [StringLength(50)]
        [EmailAddress(ErrorMessage="Enter a valid Email ID")]
        public string ClientEmailID { get; set; }
        [Required(ErrorMessage="Please enter your Contact No")]
        [StringLength(50)]
        public string ClientContactNo { get; set; }
        public string ClientRequirement { get; set; }
        [Required]
        public DateTime BookingDate { get; set; }
        [StringLength(50)]
        public string Status { get; set; }
    }
}
