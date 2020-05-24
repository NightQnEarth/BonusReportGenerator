using System;
using BonusReportGenerator.CmdClient;
using BonusReportGenerator.ReportGenerators;

namespace BonusReportGenerator
{
    public static class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                var generatorOptions = CommandLineClient.GetOptions(args);
                var generatorsManager = new GeneratorsManager(generatorOptions);

                var generatorType = generatorOptions.OutputCsvFilepath is null
                                        ? GeneratorType.Console
                                        : GeneratorType.Csv;

                generatorsManager.Generate(generatorType);
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