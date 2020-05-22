using System;

namespace BonusReportGenerator
{
    public class Contract
    {
        public int Id { get; }
        public int EmployeeId { get; }
        public DateTime ContractDate { get; }
        public int TheAmountOfTheDeal { get; }

        public Contract(int id, int employeeId, DateTime contractDate, int theAmountOfTheDeal)
        {
            Id = id;
            EmployeeId = employeeId;
            ContractDate = contractDate;
            TheAmountOfTheDeal = theAmountOfTheDeal;
        }
    }
}