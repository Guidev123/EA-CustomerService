using SharedLib.Domain.DomainObjects;
using System.Text.RegularExpressions;

namespace CustomerService.Domain.ValueObjects
{
    public class Email : ValueObject
    {
        public const int ADDRESS_MAX_LENGTH = 254;
        public const int ADDRESS_MIN_LENGTH = 5;
        protected Email() { }
        public Email(string address)
        {
            if (!Validate(address)) throw new DomainException();
            Address = address;
        }
        public string Address { get; } = string.Empty;
        public static bool Validate(string address)
        {
            if (string.IsNullOrEmpty(address) || address.Length < 5) return false;

            address.ToLower().Trim();
            const string pattern = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";

            if (!Regex.IsMatch(address, pattern)) return false;
            
            return true;
        }
    }
}
