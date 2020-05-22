using System;

namespace BonusReportGenerator
{
    public class ReportGeneratorOptions : IReportGeneratorOptions
    {
        public string EmployeesFilepath { get; set; }
        public string ContractsFilepath { get; set; }
        public bool RedirectReportPrintingToCmd { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }
}