using System;

namespace BonusReportGenerator
{
    public interface IReportGeneratorOptions
    {
        public string EmployeesFilepath { get; }
        public string ContractsFilepath { get; }
        public string RedirectReportPrintingToCsv { get; }
        public DateTime StartDateOfReport { get; }
        public DateTime FinalDateOfReport { get; }
    }
}