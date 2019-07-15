using Brothers.Entities.Models;
using Brothers.Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Brothers.Entities.DataAccess
{
    public class dalMstEnquire
    {
        private BRTContext objDB = new BRTContext();
       

        public string ArticleEnquire(utblMstArticleEnquire Item)
        {
            SPErrorViewModel objStatus = new SPErrorViewModel();
            Item.Name = Regex.Replace(Item.Name.Trim(), @"\s+", " ");
            var parName = new SqlParameter("@Name", Item.Name);
            var parEmail = new SqlParameter("@Email", Item.Email);
            var parPhone = new SqlParameter("@Phone", "");
            if (Item.Phone != null)
                parPhone = new SqlParameter("@Phone", Item.Phone);

            var parMessage = new SqlParameter("@Message", Item.Message);
            var parPostedOn = new SqlParameter("@PostedOn", System.DateTime.Now);
            objStatus = objDB.Database.SqlQuery<SPErrorViewModel>("udspMstArticleEnquireInsert @Name,@Email,@Phone,@Message,@PostedOn", parName, parEmail, parPhone, parMessage, parPostedOn).FirstOrDefault();
            return objStatus.ErrorCode + objStatus.ErrorMessage;
        }



    }
}
