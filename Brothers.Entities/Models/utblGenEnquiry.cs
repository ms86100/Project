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
    public class utblGenEnquiry
    {
        [Key]
        [HiddenInput(DisplayValue=false)]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public long EnquiryID { get; set; }
        [StringLength(128)]
        public string GuestName { get; set; }
        [Required(ErrorMessage="Please provide your Email address")]
        [StringLength(50)]
        [EmailAddress(ErrorMessage = "Please enter valid email address")]
        public string GuestEmailID { get; set; }
        [StringLength(50)]
        public string GuestMobileNo { get; set; }
        [Required(ErrorMessage="Please enter your enquiry")]
        public string EnquiryDetails { get; set; }
        public DateTime EnquiryDate { get; set; }
        public string EnquiryStatus { get; set; }
        public DateTime? EnquiryRepliedOn { get; set; }
        [StringLength(50)]
        public string EnquiryRepliedBy { get; set; }
    }
}
