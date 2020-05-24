using System;
using System.Collections.Generic;
using System.Text;
using BonusReportGenerator.ReportGenerators;
using CommandLine;

namespace BonusReportGenerator.CmdClient
{
    public static class CommandLineClient
    {
        // ReSharper disable once ParameterTypeCanBeEnumerable.Global
        // As an input parameter can only be passed command-line arguments from Main(string[] args).
        public static IReportGeneratorOptions GetOptions(string[] args)
        {
            ReportGeneratorOptions reportGeneratorOptions = null;

            Parser.Default.ParseArguments<ReportGeneratorOptions>(args)
                .WithParsed(inputOptions => reportGeneratorOptions = inputOptions)
                .WithNotParsed(HandleParseErrors);

            return reportGeneratorOptions.ParseOptions();
        }

        private static void HandleParseErrors(IEnumerable<Error> errors)
        {
            var errorMessageBuilder = new StringBuilder("following command line parser errors was handled:");
            errorMessageBuilder.AppendLine();
            errorMessageBuilder.AppendJoin(Environment.NewLine, errors);

            throw new ArgumentParserException(errorMessageBuilder.ToString());
        }
    }
}