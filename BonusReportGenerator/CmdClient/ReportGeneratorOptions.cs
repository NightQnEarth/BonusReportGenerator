using System;
using BonusReportGenerator.TableParser;
using CommandLine;

// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Local
// ReportGeneratorOptions class and it properties instantiated by reflection in CommandLineClient.GetOptions().

namespace BonusReportGenerator.CmdClient
{
    public class ReportGeneratorOptions
    {
        [Value(0, MetaName = "employees_filepath",
               Required = true,
               HelpText = "Expected filepath to csv-file with employees description table in specified format:\r\n" +
                          "EmployeeId,Name,RecruitmentDate,DismissDate,BonusCodes,Salary")]
        private string EmployeesFilepath { get; set; }

        [Value(1, MetaName = "contracts_filepath",
               Required = true,
               HelpText = "Expected filepath to csv-file with contracts description table in specified format:\r\n" +
                          "ContractId,EmployeeId,ContractDate,TheAmountOfTheDeal")]
        private string ContractsFilepath { get; set; }

        [Option('r', "redirect_report_to_stdout",
                Default = false,
                HelpText = "Redirect report printing to command line.")]
        private bool RedirectReportPrintingToCmd { get; set; }

        [Option('f', "from_date",
                HelpText = "Start date of report. Date should be represented in the following format: " +
                           Helper.DatePattern + "\r\n" +
                           "If this parameter is not specified, report will build from the earliest date.")]
        private string FromDate { get; set; }

        [Option('t', "to_date",
                HelpText = "Finish date of report. Date should be represented in the following format: " +
                           Helper.DatePattern + "\r\n" +
                           "If this parameter is not specified, report will build until the latest date.")]
        private string ToDate { get; set; }

        public IReportGeneratorOptions ParseOptions() =>
            new BonusReportGenerator.ReportGeneratorOptions
            {
                EmployeesFilepath = EmployeesFilepath,
                ContractsFilepath = ContractsFilepath,
                RedirectReportPrintingToCmd = RedirectReportPrintingToCmd,
                FromDate = ParseDate(FromDate, nameof(FromDate)),
                ToDate = ParseDate(ToDate, nameof(ToDate))
            };

        private static DateTime ParseDate(string date, string paramName)
        {
            if (Helper.ParseDate(date, out var parsedDate))
                return parsedDate;

            throw new ArgumentException($"was passed invalid format date for '{paramName}' parameter.", date);
        }
    }
}