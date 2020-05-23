using System;
using System.Collections.Generic;
using System.Linq;

namespace BonusReportGenerator.Core
{
    public class BonusCalculators
    {
        private readonly IEnumerable<Contract> contracts;
        private readonly DateTime startDate;
        private readonly DateTime finalDate;

        public Func<Employee, double>[] BonusCalculatorByBonusCode { get; }

        public BonusCalculators(IEnumerable<Contract> contracts, DateTime startDate, DateTime finalDate)
        {
            this.contracts = contracts;
            this.startDate = startDate;
            this.finalDate = finalDate;

            BonusCalculatorByBonusCode = new Func<Employee, double>[]
            {
                CalculateFirstBonus,
                CalculateSecondBonus,
                CalculateThirdBonus,
                CalculateFourthBonus
            };
        }

        private double CalculateFirstBonus(Employee employee)
        {
            const double bonusCoefficient = 0.05;

            var contractsSum = CalculateContractsSum(employee, contracts);

            return contractsSum * bonusCoefficient;
        }

        private double CalculateSecondBonus(Employee employee)
        {
            const double bonusCoefficient = 0.10;
            const int maxBonus = 100000;

            var contractsSum = CalculateContractsSum(employee, contracts);

            return Math.Min(contractsSum * bonusCoefficient, maxBonus);
        }

        private double CalculateThirdBonus(Employee employee)
        {
            const double bonusCoefficient = 0.07;
            var minTimeOfWorkToGetBonus = TimeSpan.FromDays(30 * 12 * 2); // two years

            var contractsAfterNecessaryTimeOfWork = contracts
                .Where(contract => contract.ContractDate - employee.RecruitmentDate > minTimeOfWorkToGetBonus);

            var contractsSum = CalculateContractsSum(employee, contractsAfterNecessaryTimeOfWork);

            return contractsSum * bonusCoefficient;
        }

        private double CalculateFourthBonus(Employee employee)
        {
            const double bonusCoefficient = 0.02;

            var startPeriod = new DateTime(Math.Max(employee.RecruitmentDate.Ticks, startDate.Ticks));
            var endPeriod = new DateTime(Math.Min(employee.DismissDate.Ticks, finalDate.Ticks));

            if (startPeriod.Day > 1)
                startPeriod = startPeriod.AddDays(1 - startPeriod.Day).AddMonths(1);

            if (endPeriod.Day > 1)
                endPeriod = endPeriod.AddDays(1 - endPeriod.Day);

            var fullMonthsOfWorkInReportPeriod = GetFullMonthsBetween(startPeriod, endPeriod);

            return fullMonthsOfWorkInReportPeriod * bonusCoefficient * employee.Salary;
        }

        private static int CalculateContractsSum(Employee employee, IEnumerable<Contract> contracts) =>
            contracts
                .Where(contract => contract.EmployeeId == employee.Id)
                .Sum(contract => contract.TheAmountOfTheDeal);

        private static int GetFullMonthsBetween(DateTime from, DateTime to)
        {
            if (to < from)
                throw new ArgumentException("right bound of time interval cannot be less than left bound.", nameof(to));

            var monthDiff = (to.Year - from.Year) * 12 + to.Month - from.Month + (to.Day >= from.Day ? 0 : -1);

            if (DateTime.DaysInMonth(to.Year, to.Month) == to.Day)
                return monthDiff + 1;

            return monthDiff;
        }
    }
}