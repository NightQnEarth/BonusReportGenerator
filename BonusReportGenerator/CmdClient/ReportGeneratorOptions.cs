using CommandLine;

// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReportGeneratorOptions class and it properties instantiated by reflection in CommandLineClient.GetOptions().

namespace BonusReportGenerator.CmdClient
{
    public class ReportGeneratorOptions
    {
        [Value(0, MetaName = "employees_filepath",
               Required = true,
               HelpText = "Expected filepath to csv-file with employees description table in specified format:\r\n" +
                          "EmployeeId,Name,RecruitmentDate,DismissDate,BonusCodes,Salary")]
        public string EmployeesFilepath { get; set; }

        [Value(1, MetaName = "contracts_filepath",
               Required = true,
               HelpText = "Expected filepath to csv-file with contracts description table in specified format:\r\n" +
                          "ContractId,EmployeeId,ContractDate,TheAmountOfTheDeal")]
        public string ContractsFilepath { get; set; }

        [Option('r', "redirect_report_to_stdout",
                Default = false,
                HelpText = "Redirect report printing to command line.")]
        public bool RedirectReportPrintingToCmd { get; set; }

        [Option('f', "from_date",
                HelpText = "Start date of report. Date should be represented in the following format: dd.MM.yyyy")]
        public string FromDate { get; set; }

        [Option('t', "to_date",
                HelpText = "Finish date of report. Date should be represented in the following format: dd.MM.yyyy")]
        public string ToDate { get; set; }
    }
}