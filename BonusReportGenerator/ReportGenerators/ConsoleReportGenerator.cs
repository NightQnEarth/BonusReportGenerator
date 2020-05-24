using System;
using System.Collections.Generic;
using System.Linq;
using BonusReportGenerator.Bonus;
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

            foreach (var employee in Employees)
                table.AddRow(employee.Id, employee.FullName, bonusByEmployeeId[employee.Id].Bonus);

            return table.ToString();
        }
    }
}