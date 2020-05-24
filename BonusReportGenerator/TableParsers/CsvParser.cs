using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualBasic.FileIO;

namespace BonusReportGenerator.TableParsers
{
    public static class CsvParser<T>
    {
        public static IEnumerable<T> Parse(string filePath, Func<string[], T> lineParser)
        {
            using TextFieldParser parser = new TextFieldParser(filePath, Encoding.UTF8)
            {
                TextFieldType = FieldType.Delimited,
                Delimiters = new[] { "," }
            };

            while (!parser.EndOfData)
                yield return lineParser(parser.ReadFields());
        }
    }
}