using Brothers.Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Brothers.Models
{
    public class UserListViewModel
    {
        public IEnumerable<ApplicationUser> UserList { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}