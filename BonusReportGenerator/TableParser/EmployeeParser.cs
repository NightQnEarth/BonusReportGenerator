using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace BonusReportGenerator.TableParser
{
    public static class EmployeeParser
    {
        private static readonly Regex bonusCodeValidator = new Regex(@"\d(?:,\d)*");

        public static Employee Parse(string[] lineFields)
        {
            if (lineFields.Length != 6)
                throw new ArgumentException();

            var employeeId = ParseEmployeeId(lineFields[0]);
            var name = lineFields[1];
            var recruitmentDate = ParseRecruitmentDate(lineFields[2]);
            var dismissDate = ParseDismissDate(lineFields[3]);
            var bonusCode = ParseBonusCode(lineFields[4]);
            var salary = ParseSalary(lineFields[5]);

            return new Employee(employeeId, name, recruitmentDate, dismissDate, bonusCode, salary);
        }

        private static int ParseEmployeeId(string field)
        {
            if (!int.TryParse(field, out var employeeId))
                throw new ArgumentException("incorrect employee ID.");
            return employeeId;
        }

        private static DateTime ParseRecruitmentDate(string field)
        {
            if (!Helper.ParseDate(field, out var recruitmentDate))
                throw new ArgumentException("was found invalid format date.");
            return recruitmentDate;
        }

        private static DateTime ParseDismissDate(string field)
        {
            if (!string.IsNullOrEmpty(field) & !Helper.ParseDate(field, out var dismissDate))
                throw new ArgumentException("was found invalid format date.");
            return dismissDate;
        }

        private static int[] ParseBonusCode(string field)
        {
            if (!bonusCodeValidator.IsMatch(field))
                throw new ArgumentException("incorrect bonus code.");

            var bonusCode = field.Split(',').Select(int.Parse).ToArray();

            return bonusCode;
        }

        private static int ParseSalary(string field)
        {
            if (!int.TryParse(field, out var salary))
                throw new ArgumentException("incorrect salary.");
            return salary;
        }
    }
}