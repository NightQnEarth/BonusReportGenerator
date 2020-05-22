using System;
using BonusReportGenerator.CmdClient;

namespace BonusReportGenerator
{
    public static class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                var op = CommandLineClient.GetOptions(args);
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