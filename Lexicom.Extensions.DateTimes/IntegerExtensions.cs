namespace Lexicom.Extensions.DateTimes;
public static class IntegerExtensions
{
    public static string ToDayOfMonthSuffix(this int dayOfMonth)
    {
        double below100 = dayOfMonth % 10;
        double above100 = dayOfMonth % 100;

        if (below100 is 1 && above100 is not 11)
        {
            return "st";
        }

        if (below100 is 2 && above100 is not 12)
        {
            return "nd";
        }

        if (below100 is 3 && above100 is not 13)
        {
            return "rd";
        }

        return "th";
    }
}
