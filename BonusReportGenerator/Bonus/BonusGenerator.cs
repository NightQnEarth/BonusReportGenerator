using System;
using System.Collections.Generic;
using System.Linq;
using BonusReportGenerator.TableParsers;

namespace BonusReportGenerator.Bonus
{
    public static class BonusGenerator
    {
        public static IEnumerable<EmployeeBonus> Generate(IEnumerable<Employee> employees,
                                                          IEnumerable<Contract> contracts,
                                                          DateTime startDate,
                                                          DateTime finalDate)
        {
            employees = FilterEmployeesByDate(employees, startDate, finalDate);
            contracts = FilterContractsByDate(contracts, startDate, finalDate);

            var calculators = new BonusCalculators(contracts, startDate, finalDate);

            return CalculateEmployeeBonuses(employees, calculators);
        }

        private static IEnumerable<Employee> FilterEmployeesByDate(IEnumerable<Employee> employees,
                                                                   DateTime startDate,
                                                                   DateTime finalDate) =>
            employees.Where(employee => employee.DismissDate >= startDate &&
                                        employee.RecruitmentDate <= finalDate);

        private static IEnumerable<Contract> FilterContractsByDate(IEnumerable<Contract> contracts,
                                                                   DateTime startDate,
                                                                   DateTime finalDate) =>
            contracts.Where(contract => contract.ContractDate >= startDate &&
                                        contract.ContractDate <= finalDate);

        private static IEnumerable<EmployeeBonus> CalculateEmployeeBonuses(IEnumerable<Employee> employees,
                                                                           BonusCalculators calculators) =>
            from employee in employees
            let bonus = employee.BonusCodes
                .Sum(bonusCode => calculators.BonusCalculatorByBonusCode[bonusCode - 1](employee))
            select new EmployeeBonus(employee.Id, bonus);
    }
}