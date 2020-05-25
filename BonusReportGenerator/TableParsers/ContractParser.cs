﻿using System;
using JetBrains.Annotations;

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

            CheckContractId(lineFields[0]);

            var employeeId = ParseEmployeeId(lineFields[1]);
            var contractDate = ParseContractDate(lineFields[2]);
            var theAmountOfTheDeal = ParseTheAmountOfTheDeal(lineFields[3]);

            return new Contract(employeeId, contractDate, theAmountOfTheDeal);
        }

        [AssertionMethod]
        private static void CheckContractId(string contractIdField) =>
            Helper.ParseIntField(contractIdField, $"was found incorrect contract ID '{contractIdField}'.");

        private static int ParseEmployeeId(string employeeIdField) =>
            Helper.ParseIntField(employeeIdField, $"was found incorrect employee ID '{employeeIdField}'.");

        private static DateTime ParseContractDate(string contractDateField) =>
            Helper.ParseDateField(contractDateField, $"was found invalid format date '{contractDateField}'." +
                                                     $"Use correct format: {Helper.DatePattern}");

        private static int ParseTheAmountOfTheDeal(string theAmountOfTheDealField) =>
            Helper.ParseIntField(theAmountOfTheDealField,
                                 $"incorrect the amount of the deal value '{theAmountOfTheDealField}'.");
    }
}