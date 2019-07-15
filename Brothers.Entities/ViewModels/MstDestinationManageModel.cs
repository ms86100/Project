using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Brothers.Entities.Models;

namespace Brothers.Entities.ViewModels
{
    public class MstDestinationManageModel
    {
        public utblMstDestination utblMstDestinations { get; set; }
        public IEnumerable<utblMstCountry> MstCountryDropDown { get; set; }
    }
}
