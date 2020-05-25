using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using BonusReportGenerator.Bonus;
using BonusReportGenerator.TableParsers;

namespace BonusReportGenerator.ReportGenerators
{
    public class CsvReportGenerator : ReportGenerator
    {
        private readonly string filePath;

        public CsvReportGenerator(IEnumerable<Employee> employees,
                                  IEnumerable<EmployeeBonus> bonuses,
                                  string filePath) : base(employees, bonuses) =>
            this.filePath = filePath;

        public override void Generate()
        {
            var report = ComposeReport();

            File.WriteAllText(filePath, report, Encoding.UTF8);
        }

        private string ComposeReport()
        {
            var bonusByEmployeeId = Bonuses.ToDictionary(bonus => bonus.EmployeeId, bonus => bonus);

            var reportBuilder = new StringBuilder();
            reportBuilder.AppendJoin(',', "Code", "Full name", "Bonus");
            reportBuilder.AppendLine();

            foreach (var employeeId in bonusByEmployeeId.Keys)
            {
                var fullName = Employees.First(employee => employee.Id == employeeId).FullName;
                var formattedBonus = $"{bonusByEmployeeId[employeeId].Bonus:0.00}";

                reportBuilder.AppendJoin(',', employeeId, fullName, formattedBonus);
                reportBuilder.AppendLine();
            }

            return reportBuilder.ToString();
        }
    }
}