using System.Collections.Generic;
using BonusReportGenerator.Bonus;
using BonusReportGenerator.TableParsers;

namespace BonusReportGenerator.ReportGenerators
{
    public abstract class ReportGenerator
    {
        protected IEnumerable<Employee> Employees { get; }
        protected IEnumerable<EmployeeBonus> Bonuses { get; }

        protected ReportGenerator(IEnumerable<Employee> employees, IEnumerable<EmployeeBonus> bonuses)
        {
            Employees = employees;
            Bonuses = bonuses;
        }

        public abstract void Generate();
    }
}