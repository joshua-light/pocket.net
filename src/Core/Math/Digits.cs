namespace Pocket.Common
{
    public static class Digits
    {
        public static int Count(long value)
        {
            // Digits count are hardcoded,
            // because this is more efficient way than taking Log10.
            var digits = 19;

            if (value < 10) digits = 1;
            else if (value < 100)
                digits = 2;
            else if (value < 1000)
                digits = 3;
            else if (value < 10000)
                digits = 4;
            else if (value < 100000)
                digits = 5;
            else if (value < 1000000)
                digits = 6;
            else if (value < 10000000)
                digits = 7;
            else if (value < 100000000)
                digits = 8;
            else if (value < 1000000000)
                digits = 9;
            else if (value < 10000000000)
                digits = 10;
            else if (value < 100000000000)
                digits = 11;
            else if (value < 1000000000000)
                digits = 12;
            else if (value < 10000000000000)
                digits = 13;
            else if (value < 100000000000000)
                digits = 14;
            else if (value < 1000000000000000)
                digits = 15;
            else if (value < 10000000000000000)
                digits = 16;
            else if (value < 100000000000000000)
                digits = 17;
            else if (value < 1000000000000000000)
                digits = 18;

            return digits;
        }

        public static int Count(float value)
        {
            var digits = 39;

            if (value < 10f) digits = 1;
            else if (value < 100f)
                digits = 2;
            else if (value < 1000f)
                digits = 3;
            else if (value < 10000f)
                digits = 4;
            else if (value < 100000f)
                digits = 5;
            else if (value < 1000000f)
                digits = 6;
            else if (value < 10000000f)
                digits = 7;
            else if (value < 100000000f)
                digits = 8;
            else if (value < 1000000000f)
                digits = 9;
            else if (value < 10000000000f)
                digits = 10;
            else if (value < 100000000000f)
                digits = 11;
            else if (value < 1000000000000f)
                digits = 12;
            else if (value < 10000000000000f)
                digits = 13;
            else if (value < 100000000000000f)
                digits = 14;
            else if (value < 1000000000000000f)
                digits = 15;
            else if (value < 10000000000000000f)
                digits = 16;
            else if (value < 100000000000000000f)
                digits = 17;
            else if (value < 1000000000000000000f)
                digits = 18;
            else if (value < 10000000000000000000f)
                digits = 19;
            else if (value < 100000000000000000000f)
                digits = 20;
            else if (value < 1000000000000000000000f)
                digits = 21;
            else if (value < 10000000000000000000000f)
                digits = 22;
            else if (value < 100000000000000000000000f)
                digits = 23;
            else if (value < 1000000000000000000000000f)
                digits = 24;
            else if (value < 10000000000000000000000000f)
                digits = 25;
            else if (value < 100000000000000000000000000f)
                digits = 26;
            else if (value < 1000000000000000000000000000f)
                digits = 27;
            else if (value < 10000000000000000000000000000f)
                digits = 28;
            else if (value < 100000000000000000000000000000f)
                digits = 29;
            else if (value < 1000000000000000000000000000000f)
                digits = 30;
            else if (value < 10000000000000000000000000000000f)
                digits = 31;
            else if (value < 100000000000000000000000000000000f)
                digits = 32;
            else if (value < 1000000000000000000000000000000000f)
                digits = 33;
            else if (value < 10000000000000000000000000000000000f)
                digits = 34;
            else if (value < 100000000000000000000000000000000000f)
                digits = 35;
            else if (value < 1000000000000000000000000000000000000f)
                digits = 36;
            else if (value < 10000000000000000000000000000000000000f)
                digits = 37;
            else if (value < 100000000000000000000000000000000000000f)
                digits = 38;

            return digits;
        }
    }
}