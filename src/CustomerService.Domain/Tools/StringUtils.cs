namespace CustomerService.Domain.Tools
{
    public static class StringUtils
    {
        public static string JustNumbers(this string str, string input) => new(input.Where(char.IsDigit).ToArray());
    }
}
