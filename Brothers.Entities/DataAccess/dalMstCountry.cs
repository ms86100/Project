using Brothers.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brothers.Entities.DataAccess
{
    public class dalMstCountry
    {
        private BRTContext _db = new BRTContext();
        public IEnumerable<utblMstCountry> MstCountryList
        {
            get
            {
                return _db.utblMstCountries;
            }
        }
        public IEnumerable<utblMstCountry> GetCountryPaged(int PageNo, int PageSize, out int TotalRows,string searchterm)
        {
            List<utblMstCountry> obj = new List<utblMstCountry>();
            obj = _db.utblMstCountries.OrderBy(x => x.CountryName).Where(x=> (searchterm == null || x.CountryName.Contains(searchterm)) && x.CountryID != 40).Skip((PageNo - 1) * PageSize).Take(PageSize).ToList();
            if(searchterm==null)
            {
                TotalRows = _db.utblMstCountries.Where(x => x.CountryID != 40).Count();
            }
            else
            {
                TotalRows = _db.utblMstCountries.Where(x => x.CountryName.Contains(searchterm) && x.CountryID != 40).Count();
            }
            return obj;
        }
        public int Save(utblMstCountry country)
        {
            int result = 0;
            if (country.CountryID == 0)
            {
                try
                {
                    _db.utblMstCountries.Add(country);
                    _db.SaveChanges();
                    result = 1;
                }
                catch (Exception ex)
                {
                    result = 0;
                }
            }
            else
            {
                utblMstCountry dbEntry = _db.utblMstCountries.Find(country.CountryID);
                if (dbEntry != null)
                {
                    dbEntry.CountryName = country.CountryName;
                    dbEntry.NationalityName = country.NationalityName;
                }
                _db.SaveChanges();
                result = 1;
            }
            
            return result;
        }
        public utblMstCountry GetCountryByID(long id)
        {
            utblMstCountry obj = _db.utblMstCountries.FirstOrDefault(p => p.CountryID == id);
            return obj;
        }
        public int Delete(long id)
        {
            int result = 0;
            utblMstCountry obj = _db.utblMstCountries.Find(id);
            try
            {
                _db.utblMstCountries.Remove(obj);
                _db.SaveChanges();
                result = 1;
                return result;
            }
            catch(Exception ex)
            {
                return result;
            }
        }
    }
}
