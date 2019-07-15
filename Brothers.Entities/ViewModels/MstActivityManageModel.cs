using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Brothers.Entities.Models;
namespace Brothers.Entities.ViewModels
{
    public class MstActivityManageModel
    {
        public utblMstActivity MstActivities { get; set; }
        public IEnumerable<utblMstDestination> MstDestDropDown { get; set; }
    }
}
