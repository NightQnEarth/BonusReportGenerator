using System;
using BonusReportGenerator.TableParsers;
using CommandLine;

// ReSharper disable MemberCanBePrivate.Global
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

        [Option('s', "start_date",
                Required = true,
                HelpText = "Start date of report. Date should be represented in the following format: " +
                           Helper.DatePattern)]
        public string StartDateOfReport { get; set; }

        [Option('f', "final_date",
                Required = true,
                HelpText = "Final date of report. Date should be represented in the following format: " +
                           Helper.DatePattern)]
        public string FinalDateOfReport { get; set; }

        public IReportGeneratorOptions ParseOptions() =>
            new BonusReportGenerator.ReportGeneratorOptions
            {
                EmployeesFilepath = EmployeesFilepath,
                ContractsFilepath = ContractsFilepath,
                RedirectReportPrintingToCmd = RedirectReportPrintingToCmd,
                StartDateOfReport = ParseDateParameter(StartDateOfReport, nameof(StartDateOfReport)),
                FinalDateOfReport = ParseDateParameter(FinalDateOfReport, nameof(FinalDateOfReport))
            };

        private static DateTime ParseDateParameter(string date, string paramName) =>
            Helper.ParseDateField(date, $"was passed invalid format date for '{paramName}' parameter.");
    }
}