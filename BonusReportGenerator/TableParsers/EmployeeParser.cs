using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace BonusReportGenerator.TableParsers
{
    public static class EmployeeParser
    {
        private const int EmployeeTableColumnCount = 6;
        private static readonly Regex bonusCodeValidator = new Regex(@"^\d+(?:,\d+)*$");

        public static Employee Parse(string[] lineFields)
        {
            if (lineFields.Length != EmployeeTableColumnCount)
                throw new ArgumentException("invalid employee table line was passed. Expected " +
                                            $"'{EmployeeTableColumnCount}' columns, but got '{lineFields.Length}'.");

            var employeeId = ParseEmployeeId(lineFields[0]);
            var name = ParseName(lineFields[1]);
            var recruitmentDate = ParseRecruitmentDate(lineFields[2]);
            var dismissDate = ParseDismissDate(lineFields[3]);
            var bonusCode = ParseBonusCode(lineFields[4]);
            var salary = ParseSalary(lineFields[5]);

            return new Employee(employeeId, name, recruitmentDate, dismissDate, bonusCode, salary);
        }

        private static int ParseEmployeeId(string employeeIdField) =>
            Helper.ParseIntField(employeeIdField, $"was found incorrect employee ID '{employeeIdField}'.");

        private static string ParseName(string nameField) =>
            string.IsNullOrWhiteSpace(nameField) || nameField.Length == 0
                ? throw new ArgumentException("cannot found employee name.")
                : nameField;

        private static DateTime ParseRecruitmentDate(string recruitmentDateField) =>
            Helper.ParseDateField(recruitmentDateField, $"was found invalid format date '{recruitmentDateField}'.");

        private static DateTime ParseDismissDate(string dismissDateField)
        {
            if (!string.IsNullOrEmpty(dismissDateField))
                return Helper.ParseDateField(dismissDateField, $"was found invalid format date '{dismissDateField}'.");

            if (dismissDateField is null)
                throw new ArgumentNullException(nameof(dismissDateField));

            return DateTime.MaxValue;
        }

        private static int[] ParseBonusCode(string bonusCodeField)
        {
            if (!bonusCodeValidator.IsMatch(bonusCodeField))
                throw new ArgumentException($"incorrect bonus code format '{bonusCodeField}'.");

            var bonusCode = bonusCodeField.Split(',').Select(int.Parse).ToArray();
            return bonusCode;
        }

        private static int ParseSalary(string salaryField) =>
            Helper.ParseIntField(salaryField, $"was found incorrect salary format '{salaryField}'.");
    }
}