namespace BetSystem
{
    using System;

    public static class Validator
    {
        public static void CheckIfDoubleIsInRange(double number, double max=double.MaxValue, double min = 1, string message = null)
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
