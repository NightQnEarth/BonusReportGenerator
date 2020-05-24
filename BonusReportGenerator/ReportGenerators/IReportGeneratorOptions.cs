using System;

namespace BonusReportGenerator.ReportGenerators
{
    public interface IReportGeneratorOptions
    {
        public string EmployeesFilepath { get; }
        public string ContractsFilepath { get; }
        public string OutputCsvFilepath { get; }
        public DateTime StartDateOfReport { get; }
        public DateTime FinalDateOfReport { get; }
    }
}