using System;
using System.Linq;
using BonusReportGenerator.CmdClient;
using BonusReportGenerator.Core;
using BonusReportGenerator.TableParser;

namespace BonusReportGenerator
{
    public static class Program
    {
        private static void Main(string[] args)
        {
            var op = CommandLineClient.GetOptions(args);

            var employees = CsvParser<Employee>.Parse(op.EmployeesFilepath, EmployeeParser.Parse).ToArray();
            var contracts = CsvParser<Contract>.Parse(op.ContractsFilepath, ContractParser.Parse).ToArray();

            var bonuses = BonusGenerator.Generate(employees, contracts, op.StartDateOfReport, op.FinalDateOfReport);

            foreach (var bonus in bonuses)
                Console.WriteLine($"{bonus.EmployeeId}: {bonus.Bonus}");

            try { }
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