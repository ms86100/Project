using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brothers.Entities.Models
{
    public class utblMail
    {
        public string To { get; set; }
        [Required(ErrorMessage="Subject field cannot be empty..")]
        public string Subject { get; set; }
        [Required(ErrorMessage="Please enter your reply..")]
        [DisplayName("Message")]
        public string MessageBody { get; set; }
    }
}
