using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace GigHub.Validations
{
    public class ValidTime : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            DateTime dateTime;
            var isValid = DateTime.TryParseExact(
                                                    value.ToString(),
                                                    "HH:mm",
                                                    CultureInfo.CurrentCulture,
                                                    DateTimeStyles.None,
                                                    out dateTime);


            return isValid;
        }
    }
}