using System;

namespace BonusReportGenerator
{
    public interface IReportGeneratorOptions
    {
        public string EmployeesFilepath { get; set; }
        public string ContractsFilepath { get; set; }
        public bool RedirectReportPrintingToCmd { get; set; }
        public DateTime StartDateOfReport { get; set; }
        public DateTime FinalDateOfReport { get; set; }
    }
}