using System;

namespace BonusReportGenerator.ReportGenerators
{
    public class ReportGeneratorOptions : IReportGeneratorOptions
    {
        public string EmployeesFilepath { get; set; }
        public string ContractsFilepath { get; set; }
        public string OutputCsvFilepath { get; set; }
        public DateTime StartDateOfReport { get; set; }
        public DateTime FinalDateOfReport { get; set; }
    }
}