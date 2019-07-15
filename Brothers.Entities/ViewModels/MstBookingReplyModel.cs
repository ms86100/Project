using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Brothers.Entities.Models;

namespace Brothers.Entities.ViewModels
{
    public class MstBookingReplyModel
    {
        public utblTourPackageBooking MstTourBook { get; set; }
        public utblMail MstMail { get; set; }
    }
}
