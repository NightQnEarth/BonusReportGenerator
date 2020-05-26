using System;
using System.Collections.Generic;
using System.Linq;
using BonusReportGenerator.TableParsers;

namespace BonusReportGenerator.Bonus
{
    public class BonusCalculators
    {
        private readonly Dictionary<int, HashSet<Contract>> contractsByEmployeeId;
        private readonly DateTime startDate;
        private readonly DateTime finalDate;

        public Func<Employee, double>[] BonusCalculatorByBonusCode { get; }

        public BonusCalculators(IEnumerable<Contract> contracts, DateTime startDate, DateTime finalDate)
        {
            contractsByEmployeeId = ToAssociativeCollection(contracts);
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

        private static Dictionary<int, HashSet<Contract>> ToAssociativeCollection(IEnumerable<Contract> contracts)
        {
            var contractsByEmployeeId = new Dictionary<int, HashSet<Contract>>();

            foreach (var contract in contracts)
                if (contractsByEmployeeId.TryGetValue(contract.EmployeeId, out var employeeContracts))
                    employeeContracts.Add(contract);
                else
                    contractsByEmployeeId[contract.EmployeeId] = new HashSet<Contract>(new[] { contract });

            return contractsByEmployeeId;
        }

        private double CalculateFirstBonus(Employee employee)
        {
            const double bonusCoefficient = 0.05;

            var contractsSum = CalculateContractsSum(employee);

            return contractsSum * bonusCoefficient;
        }

        private double CalculateSecondBonus(Employee employee)
        {
            const double bonusCoefficient = 0.10;
            const int maxBonus = 100000;

            var contractsSum = CalculateContractsSum(employee);

            return Math.Min(contractsSum * bonusCoefficient, maxBonus);
        }

        private double CalculateThirdBonus(Employee employee)
        {
            const double bonusCoefficient = 0.07;
            const int monthsOfWorkToGetBonus = 12 * 2;

            var contractsAfterNecessaryTimeOfWork = GetEmployeeContracts(employee)
                .Where(contract => employee.RecruitmentDate.AddMonths(monthsOfWorkToGetBonus) < contract.ContractDate);

            var contractsSum = contractsAfterNecessaryTimeOfWork.Sum(contract => contract.TheAmountOfTheDeal);

            return contractsSum * bonusCoefficient;
        }

        private double CalculateFourthBonus(Employee employee)
        {
            const double bonusCoefficient = 0.02;

            var startPeriod = new DateTime(Math.Max(employee.RecruitmentDate.Ticks, startDate.Ticks));
            var endPeriod = new DateTime(Math.Min(employee.DismissDate.Ticks, finalDate.Ticks));

            var fullMonthsOfWorkInReportPeriod = GetFullMonthsBetween(startPeriod, endPeriod);

            return fullMonthsOfWorkInReportPeriod * bonusCoefficient * employee.Salary;
        }

        private HashSet<Contract> GetEmployeeContracts(Employee employee)
        {
            if (contractsByEmployeeId.TryGetValue(employee.Id, out var employeeContracts))
                return employeeContracts;

            return contractsByEmployeeId[employee.Id] = new HashSet<Contract>();
        }

        private int CalculateContractsSum(Employee employee) =>
            GetEmployeeContracts(employee).Sum(contract => contract.TheAmountOfTheDeal);

        private static int GetFullMonthsBetween(DateTime from, DateTime to)
        {
            if (to < from)
                throw new ArgumentException("right bound of time interval cannot be less than left bound.", nameof(to));

            if (from.Day > 1)
                from = from.AddDays(1 - from.Day).AddMonths(1);
            if (to.Day > 1)
                to = to.AddDays(1 - to.Day);

            var fullMonths = (to.Year - from.Year) * 12 + to.Month - from.Month;

            return fullMonths;
        }
    }
}