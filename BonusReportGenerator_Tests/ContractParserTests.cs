using System;
using BonusReportGenerator.TableParsers;
using FluentAssertions;
using NUnit.Framework;

namespace BonusReportGenerator_Tests
{
    [TestFixture]
    public class ContractParserTests
    {
        private const string CorrectContractTableLine = "1,2,01.02.2001,35000";
        private string invalidContractRow;
        private Action parser;
        private Contract contract;

        [SetUp]
        public void SetUp()
        {
            contract = ContractParser.Parse(CorrectContractTableLine.Split(','));
            parser = () => ContractParser.Parse(invalidContractRow.Split(','));
        }

        [Test]
        public void Parse_OnCorrectContractRow_ReturnsContract() =>
            contract.Should().NotBeNull();

        [Test]
        public void Parse_WhenCorrectRow_ContractHasCorrectEmployeeId() =>
            contract.EmployeeId.Should().Be(2);

        [Test]
        public void Parse_WhenCorrectRow_ContractHasCorrectDate() =>
            contract.ContractDate.Should().Be(new DateTime(2001, 02, 01));

        [Test]
        public void Parse_WhenCorrectRow_ContractHasCorrectAmountOfTheDeal() =>
            contract.TheAmountOfTheDeal.Should().Be(35000);

        [Test]
        public void Parse_OnIncorrectColumnsCount_ThrowsArgumentException()
        {
            invalidContractRow = "1,2,01.02.2001";

            parser.Should().Throw<ArgumentException>()
                .Where(exception => exception.Message.StartsWith("invalid contract table line was passed."));
        }

        [Test]
        public void Parse_OnInvalidContractId_ThrowsArgumentException()
        {
            invalidContractRow = "1##,2,01.02.2001,35000";

            parser.Should().Throw<ArgumentException>()
                .Where(exception => exception.Message.StartsWith("was found incorrect contract ID"));
        }

        [Test]
        public void Parse_OnInvalidEmployeeId_ThrowsArgumentException()
        {
            invalidContractRow = "1,2.5,01.02.2001,35000";

            parser.Should().Throw<ArgumentException>()
                .Where(exception => exception.Message.StartsWith("was found incorrect employee ID"));
        }

        [Test]
        public void Parse_OnInvalidDate_ThrowsArgumentException()
        {
            invalidContractRow = "1,2,01/02/2001,35000";

            parser.Should().Throw<ArgumentException>()
                .Where(exception => exception.Message.StartsWith("was found invalid format date"));
        }

        [Test]
        public void Parse_OnInvalidAmountOfTheDeal_ThrowsArgumentException()
        {
            invalidContractRow = "1,2,01.02.2001,qwerty";

            parser.Should().Throw<ArgumentException>()
                .Where(exception => exception.Message.StartsWith("incorrect the amount of the deal value"));
        }
    }
}