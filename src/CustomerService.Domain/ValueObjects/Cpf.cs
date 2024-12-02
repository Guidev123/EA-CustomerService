using EA.CommonLib.DomainObjects;

namespace CustomerService.Domain.ValueObjects
{
    public class Cpf : ValueObject
    {
        public string Number { get; private set; } = string.Empty;

        public Cpf(string number)
        {
            if (!Validate(number)) throw new DomainException();
            Number = number;
        }
        protected Cpf() { }
        public static string JustNumbers(string input) => new(input.Where(char.IsDigit).ToArray());
        public static bool Validate(string number)
        {
            number = JustNumbers(number);

            if (number.Length > 11)
                return false;

            while (number.Length != 11)
                number = '0' + number;

            var equal = true;
            for (var i = 1; i < 11 && equal; i++)
                if (number[i] != number[0])
                    equal = false;

            if (equal || number == "12345678909")
                return false;

            var numbers = new int[11];

            for (var i = 0; i < 11; i++)
                numbers[i] = int.Parse(number[i].ToString());

            var sum = 0;
            for (var i = 0; i < 9; i++)
                sum += (10 - i) * numbers[i];

            var result = sum % 11;

            if (result == 1 || result == 0)
            {
                if (numbers[9] != 0)
                    return false;
            }
            else if (numbers[9] != 11 - result)
                return false;

            sum = 0;
            for (var i = 0; i < 10; i++)
                sum += (11 - i) * numbers[i];

            result = sum % 11;

            if (result == 1 || result == 0)
            {
                if (numbers[10] != 0)
                    return false;
            }
            else if (numbers[10] != 11 - result)
                return false;

            return true;
        }
    }
}
