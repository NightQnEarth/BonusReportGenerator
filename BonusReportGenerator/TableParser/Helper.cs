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

        public static int ParseIntField(string integerField, string exceptionMessage)
        {
            if (!int.TryParse(integerField, out var parsedField))
                throw new ArgumentException(exceptionMessage);
            return parsedField;
        }

        public static DateTime ParseDateField(string dateField, string exceptionMessage)
        {
            if (!ParseDate(dateField, out var parsedDate))
                throw new ArgumentException(exceptionMessage);
            return parsedDate;
        }
    }
}