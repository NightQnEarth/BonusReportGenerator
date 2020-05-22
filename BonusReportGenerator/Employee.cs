using System;

namespace BonusReportGenerator
{
    public class Employee
    {
        public int Id { get; }
        public string Name { get; }
        public DateTime RecruitmentDate { get; }
        public DateTime DismissDate { get; }
        public int[] BonusCodes { get; }
        public int Salary { get; }

        public Employee(int id,
                        string name,
                        DateTime recruitmentDate,
                        DateTime dismissDate,
                        int[] bonusCodes,
                        int salary)
        {
            Id = id;
            Name = name;
            RecruitmentDate = recruitmentDate;
            DismissDate = dismissDate;
            BonusCodes = bonusCodes;
            Salary = salary;
        }
    }
}