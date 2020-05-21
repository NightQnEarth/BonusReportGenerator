using System;
using System.Globalization;
using System.Text;
using Microsoft.VisualBasic.FileIO;

namespace BonusReportGenerator
{
    public static class Program
    {
        private static void Main(string[] args)
        {
            var d = DateTime.TryParseExact("12.05.1998",
                                           "dd.MM.yyyy",
                                           CultureInfo.InvariantCulture,
                                           DateTimeStyles.None,
                                           out var date);

            using TextFieldParser parser = new TextFieldParser(
                @"C:\Users\Night\Desktop\Languages\C#\TestForAlfaBank\BonusReportGenerator\employees.csv",
                Encoding.UTF8)
            {
                TextFieldType = FieldType.Delimited,
                Delimiters = new[] { "," }
            };

            while (!parser.EndOfData)
            {
                var fields = parser.ReadFields();

                foreach (var field in fields)
                    Console.Write(field + " ");

                Console.WriteLine();
                Console.ReadLine();
            }
        }
    }
}