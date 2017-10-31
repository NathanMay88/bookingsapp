using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MyBookingsApp.Code
{
    public class NonZero : ValidationAttribute
    {
        public NonZero()
        {
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (!(value is int))
            {
                return new ValidationResult("Not an int");
            }

            if ((int)value == 0)
            {
                return new ValidationResult(validationContext.MemberName + " must be above 0");
            }

            return ValidationResult.Success;
        }
    }
}