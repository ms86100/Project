using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Brothers.Models
{
    public class ContactModel
    {
        [Required(ErrorMessage="Please enter your First Name...")]
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Please enter your First Name...")]
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Please enter your First Name...")]
        [DisplayName("Email Address")]
        [EmailAddress(ErrorMessage="Please enter valid Email Address...")]

        public string EmailAddress { get; set; }
        [DisplayName("Phone Number")]
        public string PhoneNumber { get;set;}
        [Required(ErrorMessage = "Please enter your Message...")]
        public string Message { get;set;}
    }
}