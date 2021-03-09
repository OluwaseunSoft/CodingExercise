using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CodingExercise.Validators
{
    public class DateValidator : ValidationAttribute
    {
        //
        public class DateValidatorAttribute : ValidationAttribute
        {
            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                try
                {
                    string strValue = value.ToString();
                    var dateTime = DateTime.Parse(strValue);
                    string pattern = @"(^(((0[1-9]|1[0-9]|2[0-8])[\/](0[1-9]|1[012]))|((29|30|31)[\/](0[13578]|1[02]))|((29|30)[\/](0[4,6,9]|11)))[\/](19|[2-9][0-9])\d\d$)|(^29[\/]02[\/](19|[2-9][0-9])(00|04|08|12|16|20|24|28|32|36|40|44|48|52|56|60|64|68|72|76|80|84|88|92|96)$)";
                    Match match = Regex.Match(strValue, pattern);
                    if (match.Success)
                    {
                        var property = validationContext.ObjectType.GetProperty("ExpirationDate");
                        if (property == null)
                            return new ValidationResult(string.Format("Property '{0}' is Null", "ExpirationDate"));
                        var ExpiryDate = property.GetValue(validationContext.ObjectInstance, null);
                        string strExpiryDate = ExpiryDate == null ? "" : ExpiryDate.ToString();
                        Match matchJoiningDate = Regex.Match(strExpiryDate, pattern);
                        if (matchJoiningDate.Success)
                        {
                            var joiningDate = DateTime.Parse(strExpiryDate);

                            if (DateTime.Now < joiningDate)
                                return new ValidationResult("Expiry Date Cannot Be Past");

                            if (dateTime < DateTime.Now)
                                return new ValidationResult("Expiry Date Cannot Be Past");
                        }
                    }
                    else
                        return new ValidationResult("Invalid date format");
                    return ValidationResult.Success;
                }
                catch (Exception)
                {
                    return new ValidationResult("Invalid date format");
                }
            }
        }
        //
    }
}
