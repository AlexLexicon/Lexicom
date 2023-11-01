namespace Lexicom.Maths;
public static class Mathx
{
    /// <exception cref="ArgumentOutOfRangeException"/>
    public static long CreateNumberOfOnlyDigitRepeatedNTimes(int digit, int n)
    {
        if (digit is < 0 or > 9)
        {
            throw new ArgumentOutOfRangeException(nameof(digit), digit, "Must be a single digit integer (0-9).");
        }

        if (n is <= 0 or > 15)
        {
            //the reason that we limit n to 15
            //is because that is the largest number of nines
            //possible before the double precision fails

            throw new ArgumentOutOfRangeException(nameof(n), n, "Must be greater than '0' and less than '15'.");
        }

        if (digit is 0)
        {
            //the math below will work with zero
            //but it is a waste since creating a number
            //with any number of zeros is still zero
            //so we can make this branch return early
            return 0;
        }

        double pow = Math.Pow(10, n);
        double powMinusOne = pow - 1;
        double dividedByNine = powMinusOne / 9;
        double timesDigit = dividedByNine * digit;

        return (long)timesDigit;
    }
}
