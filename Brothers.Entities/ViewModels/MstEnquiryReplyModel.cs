using Brothers.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Brothers.Entities.ViewModels
{
    public class MstEnquiryReplyModel
    {
        public utblGenEnquiry utblGenEnquiries { get; set; }
        public utblMail utblMails { get; set; }
    }
}