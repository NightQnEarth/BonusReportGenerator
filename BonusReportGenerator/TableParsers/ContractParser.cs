using System;

namespace BonusReportGenerator.TableParsers
{
    public static class ContractParser
    {
        private const int ContractTableColumnCount = 4;

        public static Contract Parse(string[] lineFields)
        {
            if (lineFields.Length != ContractTableColumnCount)
                throw new ArgumentException("invalid contract table line was passed. Expected " +
                                            $"'{ContractTableColumnCount}' columns, but got '{lineFields.Length}'.");

            var employeeId = ParseEmployeeId(lineFields[1]);
            var contractDate = ParseContractDate(lineFields[2]);
            var theAmountOfTheDeal = ParseTheAmountOfTheDeal(lineFields[3]);

            return new Contract(employeeId, contractDate, theAmountOfTheDeal);
        }

        private static int ParseEmployeeId(string employeeIdField) =>
            Helper.ParseIntField(employeeIdField, $"was found incorrect employee ID '{employeeIdField}'.");

        private static DateTime ParseContractDate(string contractDateField) =>
            Helper.ParseDateField(contractDateField, $"was found invalid format date '{contractDateField}'.");

        private static int ParseTheAmountOfTheDeal(string theAmountOfTheDealField) =>
            Helper.ParseIntField(theAmountOfTheDealField,
                                 $"incorrect the amount of the deal value '{theAmountOfTheDealField}'.");
    }
}