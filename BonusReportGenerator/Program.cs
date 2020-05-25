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
            catch (Exception exception)
            {
                var errorMessage = string.Concat("Exception was thrown.",
                                                 Environment.NewLine,
                                                 exception.GetType().FullName + ": ",
                                                 exception.Message);

                Console.WriteLine(errorMessage);
            }
        }
    }
}