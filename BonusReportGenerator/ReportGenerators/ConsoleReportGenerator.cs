using System;
using System.Collections.Generic;
using System.Linq;
using BonusReportGenerator.Bonus;
using BonusReportGenerator.TableParsers;
using ConsoleTables;

namespace BonusReportGenerator.ReportGenerators
{
    public class ConsoleReportGenerator : ReportGenerator
    {
        public ConsoleReportGenerator(IEnumerable<Employee> employees,
                                      IEnumerable<EmployeeBonus> bonuses) : base(employees, bonuses) { }

        public override void Generate()
        {
            var report = ComposeReport();

            Console.WriteLine(report);
        }

        private string ComposeReport()
        {
            var bonusByEmployeeId = Bonuses.ToDictionary(bonus => bonus.EmployeeId, bonus => bonus);

            var table = new ConsoleTable("Code", "Full name", "Bonus")
                .Configure(options => options.EnableCount = false);

            foreach (var employeeId in bonusByEmployeeId.Keys)
            {
                var fullName = Employees.First(employee => employee.Id == employeeId).FullName;
                table.AddRow(employeeId, fullName, $"{bonusByEmployeeId[employeeId].Bonus:0.00}");
            }

            return table.ToString();
        }
    }
}