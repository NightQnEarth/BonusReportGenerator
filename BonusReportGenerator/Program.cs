using System;
using System.Linq;
using BonusReportGenerator.CmdClient;
using BonusReportGenerator.TableParser;

namespace BonusReportGenerator
{
    public static class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                // var op = CommandLineClient.GetOptions(args);
                var employees = CsvParser<Employee>.Parse(
                    @"C:\Users\Night\Desktop\Languages\C#\TestForAlfaBank\BonusReportGenerator\employees.csv",
                    EmployeeParser.Parse).ToArray();
                var contracts = CsvParser<Contract>.Parse(
                    @"C:\Users\Night\Desktop\Languages\C#\TestForAlfaBank\BonusReportGenerator\contracts.csv",
                    ContractParser.Parse).ToArray();
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