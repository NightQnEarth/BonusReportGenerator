using System;

namespace BonusReportGenerator.TableParsers
{
    public class Employee
    {
        public int Id { get; }
        public string FullName { get; }
        public DateTime RecruitmentDate { get; }
        public DateTime DismissDate { get; }
        public int[] BonusCodes { get; }
        public int Salary { get; }

        public Employee(int id,
                        string fullName,
                        DateTime recruitmentDate,
                        DateTime dismissDate,
                        int[] bonusCodes,
                        int salary)
        {
            Id = id;
            FullName = fullName;
            RecruitmentDate = recruitmentDate;
            DismissDate = dismissDate;
            BonusCodes = bonusCodes;
            Salary = salary;
        }
    }
}