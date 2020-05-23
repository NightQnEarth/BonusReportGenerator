using System;

namespace BonusReportGenerator
{
    public interface IReportGeneratorOptions
    {
        string EmployeesFilepath { get; set; }
        string ContractsFilepath { get; set; }
        bool RedirectReportPrintingToCmd { get; set; }
        DateTime StartDateOfReport { get; set; }
        DateTime FinalDateOfReport { get; set; }
    }
}