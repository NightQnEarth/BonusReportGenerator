using System;

namespace BonusReportGenerator.TableParsers
{
    public class Contract : IEquatable<Contract>
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

        public bool Equals(Contract other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return EmployeeId == other.EmployeeId;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;

            return obj.GetType() == GetType() && Equals((Contract)obj);
        }

        public override int GetHashCode() => EmployeeId;
    }
}