using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Users
{
   public static class Validation
    {
        public const string EmailPattern =
         @"^([0-9a-zA-Z]" +
         @"([\+\-_\.][0-9a-zA-Z]+)*" +
         @")+" +
         @"@(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]*\.)+[a-zA-Z0-9]{2,17})$";

        public const string UserNamePattern = @"^(?=[a-zA-Z])[-\w.]{0,23}([a-zA-Z\d]|(?<![-.])_)$";

        public const string NamePattern = @"[a-zA-Z]";

        public const string SsnPattern = @"[0-9]";

        public const decimal MaxDeposit = 100000;

        public const string PassPattern = @"(.*?).{4,15}";

        public const int DaysInYear = 365;

        public const int MinYearsForRegistration = 18;

        public static bool IsValid(string input, string pattern)
        {
            return Regex.IsMatch(input, pattern);
        }

        public static void CheckIfDoubleIsInRange(double number, double max = double.MaxValue, double min = 1, string message = null)
        {
            if (number < min || max < number)
            {
                throw new ArgumentOutOfRangeException(message);
            }
        }

        public static void CheckIfIntIsInRange(int number, int max = int.MaxValue, double min = 1, string message = null)
        {
            if (number < min || max < number)
            {
                throw new ArgumentOutOfRangeException(message);
            }
        }
    }
}