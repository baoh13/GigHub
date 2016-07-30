using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace GigHub.Validations
{
    public class FutureDate : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            DateTime dateTime;
            var isValid = DateTime.TryParseExact(
                                                    value.ToString(), 
                                                    "d MMM yyyy",
                                                    CultureInfo.CurrentCulture,
                                                    DateTimeStyles.None,
                                                    out dateTime);


            return isValid && dateTime > DateTime.Now;
        }
    }
}