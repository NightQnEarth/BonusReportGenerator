namespace BonusReportGenerator.Bonus
{
    public class EmployeeBonus
    {
        public int EmployeeId { get; }
        public double Bonus { get; }

        public EmployeeBonus(int employeeId, double bonus)
        {
            EmployeeId = employeeId;
            Bonus = bonus;
        }
    }
}