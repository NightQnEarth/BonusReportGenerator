using System;
using System.Collections.Generic;
using System.Linq;
using BonusReportGenerator.Bonus;
using BonusReportGenerator.CmdClient;
using BonusReportGenerator.ReportGenerators;
using BonusReportGenerator.TableParsers;

namespace BonusReportGenerator
{
    public static class Program
    {
        private static readonly Dictionary<GeneratorType, ReportGenerator> reportGenerators =
            new Dictionary<GeneratorType, ReportGenerator>();

        private static void Main(string[] args)
        {
            try
            {
                var generatorOptions = CommandLineClient.GetOptions(args);

                var employees = CsvParser<Employee>.Parse(generatorOptions.EmployeesFilepath, EmployeeParser.Parse)
                    .ToArray();
                var contracts = CsvParser<Contract>.Parse(generatorOptions.ContractsFilepath, ContractParser.Parse)
                    .ToArray();

                var bonuses = BonusGenerator.Generate(employees,
                                                      contracts,
                                                      generatorOptions.StartDateOfReport,
                                                      generatorOptions.FinalDateOfReport)
                    .ToArray();

                reportGenerators.Add(GeneratorType.Console, new ConsoleReportGenerator(employees, bonuses));
                reportGenerators.Add(GeneratorType.Csv, new CsvReportGenerator(
                                         employees, bonuses, generatorOptions.RedirectReportPrintingToCsv));

                var generatorType = generatorOptions.RedirectReportPrintingToCsv is null
                                        ? GeneratorType.Console
                                        : GeneratorType.Csv;

                reportGenerators[generatorType].Generate();
            }
            catch (ArgumentParserException exception)
            {
                Console.WriteLine(exception.Message);
            }
            catch (Exception exception)
            {
                var errorMessage = string.Concat("Exception was thrown:",
                                                 Environment.NewLine,
                                                 exception.GetType().FullName + ": ",
                                                 char.ToUpper(exception.Message[0]),
                                                 exception.Message.Substring(1));

                Console.WriteLine(errorMessage);
            }
        }
    }
}