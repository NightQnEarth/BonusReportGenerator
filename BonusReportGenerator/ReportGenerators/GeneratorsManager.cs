using System.Collections.Generic;
using System.Linq;
using BonusReportGenerator.Bonus;
using BonusReportGenerator.TableParsers;

namespace BonusReportGenerator.ReportGenerators
{
    public class GeneratorsManager
    {
        private readonly Dictionary<GeneratorType, ReportGenerator> reportGenerators;

        public GeneratorsManager(IReportGeneratorOptions generatorOptions)
        {
            var contracts = CsvParser<Contract>.Parse(generatorOptions.ContractsFilepath, ContractParser.Parse)
                .ToArray();
            var employees = CsvParser<Employee>.Parse(generatorOptions.EmployeesFilepath, EmployeeParser.Parse)
                .ToArray();
            var bonuses = BonusGenerator.Generate(
                    employees, contracts, generatorOptions.StartDateOfReport, generatorOptions.FinalDateOfReport)
                .ToArray();

            reportGenerators = new Dictionary<GeneratorType, ReportGenerator>
            {
                [GeneratorType.Console] = new ConsoleReportGenerator(employees, bonuses),
                [GeneratorType.Csv] = new CsvReportGenerator(employees, bonuses, generatorOptions.OutputCsvFilepath)
            };
        }

        public void Generate(GeneratorType generatorType) =>
            reportGenerators[generatorType].Generate();
    }
}