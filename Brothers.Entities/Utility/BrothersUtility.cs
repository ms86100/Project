using Brothers.Entities.DataAccess;
using Brothers.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brothers.Entities.Utility
{
   public class BrothersUtility
    {

        BRTContext objDB = new BRTContext();

        public string generateUniqueCode(string tablename)
        {
            int SlNo = 0;
            string UniqueCode = "";
            int RangeAssci = 0;

            switch (tablename)
            {
                case "utblBlogs":
                    utblGenCodeSeed utblBlog = objDB.utblGenCodeSeeds.FirstOrDefault(p => p.TableName == tablename);
                    if (utblBlog != null)
                    {
                        SlNo = utblBlog.CodeSlNo;
                        UniqueCode = utblBlog.CharRange;
                        RangeAssci = System.Convert.ToInt32(char.Parse(utblBlog.CharRange));
                    }
                    SlNo = SlNo + 1;
                    UniqueCode = UniqueCode + SlNo.ToString("00000");

                    if (SlNo == 999)
                    {
                        SlNo = 0;
                        RangeAssci = RangeAssci + 1;
                        utblBlog.CodeSlNo = SlNo;
                        utblBlog.CharRange = Char.ConvertFromUtf32(RangeAssci).ToString();
                    }
                    else
                    {
                        utblBlog.CodeSlNo = SlNo;
                    }
                    objDB.SaveChanges();
                    return UniqueCode;


              
            }
            return null;
        }
    }
}
