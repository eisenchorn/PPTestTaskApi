using System.ComponentModel.DataAnnotations;

namespace WebApi.ValidationAttributes
{
    public class PhoneValidationAttribute : RegularExpressionAttribute
    {
        public PhoneValidationAttribute(string pattern) : base(pattern)
        {
        }

        public override string FormatErrorMessage(string name)
        {
            return $"Value of the field {name} must match mask «+7XXX-XXX-XX-XX», where X represents digit character.";
        }
    }
}