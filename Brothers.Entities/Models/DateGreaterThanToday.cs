using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Brothers.Entities.Models
{
    public class DateGreaterThanTodayAttribute : ValidationAttribute
    {
        DateTime today = DateTime.Now.Date;
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
               
                var arrDate = (DateTime)value;
                if (arrDate < today)
                {
                    return new ValidationResult("Date must be greater than today");
                }
            }
            return ValidationResult.Success;
        }
    }
}
