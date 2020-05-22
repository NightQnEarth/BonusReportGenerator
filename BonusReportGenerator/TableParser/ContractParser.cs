using System;

namespace BonusReportGenerator.TableParser
{
    public static class ContractParser
    {
        public static Contract Parse(string[] lineFields)
        {
            if (lineFields.Length != 4)
                throw new ArgumentException();

            if (!int.TryParse(lineFields[0], out var contractId))
                throw new ArgumentException("incorrect contract ID.");

            if (!int.TryParse(lineFields[1], out var employeeId))
                throw new ArgumentException("incorrect employee ID.");

            if (!Helper.ParseDate(lineFields[2], out var contractDate))
                throw new ArgumentException("was found invalid format date.");

            if (!int.TryParse(lineFields[3], out var theAmountOfTheDeal))
                throw new ArgumentException("incorrect the amount of the deal value.");

            return new Contract(contractId, employeeId, contractDate, theAmountOfTheDeal);
        }
    }
}