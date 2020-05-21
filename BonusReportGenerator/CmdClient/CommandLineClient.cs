using System;
using System.Collections.Generic;
using CommandLine;

namespace BonusReportGenerator.CmdClient
{
    public class CommandLineClient
    {
        // ReSharper disable once ParameterTypeCanBeEnumerable.Global
        // As an input parameter can only be passed command-line arguments from Main(string[] args).
        public static ReportGeneratorOptions GetOptions(string[] args)
        {
            ReportGeneratorOptions reportGeneratorOptions = null;

            Parser.Default.ParseArguments<ReportGeneratorOptions>(args)
                .WithParsed(inputOptions => reportGeneratorOptions = inputOptions)
                .WithNotParsed(HandleParseErrors);

            return reportGeneratorOptions;
        }

        private static void HandleParseErrors(IEnumerable<Error> errors)
        {
            var exitCode = 0;

            foreach (var error in errors)
            {
                Console.Error.WriteLine(error);
                exitCode = (int)error.Tag;
            }

            Environment.Exit(exitCode);
        }
    }
}