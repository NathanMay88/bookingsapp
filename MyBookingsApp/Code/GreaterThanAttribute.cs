using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace MyBookingsApp.Code
{
    public class GreaterThan : ValidationAttribute
    {
        private string _propertyName;
        private int propertyValue;
        public GreaterThan(string PropertyName)
        {
            _propertyName = PropertyName;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var propertyTestedInfo = validationContext.ObjectType.GetProperty(this._propertyName);
            if (propertyTestedInfo == null)
            {
                return new ValidationResult(string.Format("unknown property {0}", this._propertyName));
            }

            var propertyTestedValue = propertyTestedInfo.GetValue(validationContext.ObjectInstance, null);

            if (value == null || !(value is int))
            {
                return ValidationResult.Success;
            }

            if (propertyTestedValue == null || !(propertyTestedValue is int))
            {
                return ValidationResult.Success;
            }

            // Compare values
            if ((int)value >= (int)propertyTestedValue)
            {

                return ValidationResult.Success;

            }

            return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var rule = new ModelClientValidationRule
            {
                ErrorMessage = this.ErrorMessageString,
                ValidationType = "greaterthan"
            };
            rule.ValidationParameters["propertytested"] = this._propertyName;
            yield return rule;
        }
    }
}