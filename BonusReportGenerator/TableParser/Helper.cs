using System;
using System.Globalization;

namespace BonusReportGenerator.TableParser
{
    public static class Helper
    {
        public const string DatePattern = "dd.MM.yyyy";

        public static bool ParseDate(string date, out DateTime parsedDate) =>
            DateTime.TryParseExact(date,
                                   DatePattern,
                                   CultureInfo.InvariantCulture,
                                   DateTimeStyles.None,
                                   out parsedDate);
    }
}