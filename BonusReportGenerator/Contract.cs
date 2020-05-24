using System;

namespace BonusReportGenerator
{
    public class Contract
    {
        public int EmployeeId { get; }
        public DateTime ContractDate { get; }
        public int TheAmountOfTheDeal { get; }

        public Contract(int employeeId, DateTime contractDate, int theAmountOfTheDeal)
        {
            EmployeeId = employeeId;
            ContractDate = contractDate;
            TheAmountOfTheDeal = theAmountOfTheDeal;
        }
    }
}