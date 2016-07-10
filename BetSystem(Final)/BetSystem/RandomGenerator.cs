namespace BetSystem
{
    using System;

    public static class RandomGenerator
    {
        //fields
        private static Random rand;

        //methods
        public static int RandInt(int value)
        {
            return rand.Next(value);
        }

        static RandomGenerator()
        {
            rand = new Random();
        }
    }
}
