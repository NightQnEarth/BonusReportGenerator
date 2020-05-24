using System;
using BonusReportGenerator.TableParsers;
using FluentAssertions;
using NUnit.Framework;

namespace BonusReportGenerator_Tests
{
    [TestFixture]
    public class EmployeeParserTests
    {
        private readonly string[] correctEmployeeTableLine =
            { "1", "Some Full Name", "01.05.2016", "", "1,4", "30000" };

        private string[] invalidEmployeeRow;
        private Action parser;
        private Employee employee;

        [SetUp]
        public void SetUp()
        {
            employee = EmployeeParser.Parse(correctEmployeeTableLine);
            parser = () => EmployeeParser.Parse(invalidEmployeeRow);
        }

        [Test]
        public void Parse_OnCorrectEmployeeRow_ReturnsEmployee() =>
            employee.Should().NotBeNull();

        [Test]
        public void Parse_WhenCorrectRow_EmployeeHasCorrectId() =>
            employee.Id.Should().Be(1);

        [Test]
        public void Parse_WhenCorrectRow_EmployeeHasCorrectFullName() =>
            employee.FullName.Should().Be("Some Full Name");

        [Test]
        public void Parse_WhenCorrectRow_EmployeeHasCorrectRecruitmentDate() =>
            employee.RecruitmentDate.Should().Be(new DateTime(2016, 05, 01));

        [Test]
        public void Parse_WhenNotPassedDismissDate_EmployeeHasMaxDismissDate() =>
            employee.DismissDate.Should().Be(DateTime.MaxValue);

        [Test]
        public void Parse_WhenPassedDismissDate_EmployeeCorrectDismissDate()
        {
            string[] employeeRowWithDismissDate = { "1", "Some Full Name", "01.05.2016", "22.05.2019", "1,4", "30000" };

            employee = EmployeeParser.Parse(employeeRowWithDismissDate);

            employee.DismissDate.Should().Be(new DateTime(2019, 05, 22));
        }

        [Test]
        public void Parse_WhenCorrectRow_EmployeeHasCorrectBonusCode() =>
            employee.BonusCodes.Should().Equal(1, 4);

        [Test]
        public void Parse_WhenCorrectRow_EmployeeHasCorrectSalary() =>
            employee.Salary.Should().Be(30000);

        [Test]
        public void Parse_OnIncorrectColumnsCount_ThrowsArgumentException()
        {
            invalidEmployeeRow = new[] { "1", "Some Full Name", "01.05.2016", "" };

            parser.Should().Throw<ArgumentException>()
                .Where(exception => exception.Message.StartsWith("invalid employee table line was passed."));
        }

        [Test]
        public void Parse_OnInvalidEmployeeId_ThrowsArgumentException()
        {
            invalidEmployeeRow = new[] { "1.3", "Some Full Name", "01.05.2016", "", "1,4", "30000" };

            parser.Should().Throw<ArgumentException>()
                .Where(exception => exception.Message.StartsWith("was found incorrect employee ID"));
        }

        [Test]
        public void Parse_OnInvalidFullName_ThrowsArgumentException()
        {
            invalidEmployeeRow = new[] { "1", "\t  ", "01.05.2016", "", "1,4", "30000" };

            parser.Should().Throw<ArgumentException>().WithMessage("cannot found employee name.");
        }

        [Test]
        public void Parse_OnInvalidRecruitmentDate_ThrowsArgumentException()
        {
            invalidEmployeeRow = new[] { "1", "Some Full Name", "01-05-2016", "", "1,4", "30000" };

            parser.Should().Throw<ArgumentException>()
                .Where(exception => exception.Message.StartsWith("was found invalid format date"));
        }

        [Test]
        public void Parse_OnInvalidDismissDate_ThrowsArgumentException()
        {
            invalidEmployeeRow = new[] { "1", "Some Full Name", "01.05.2016", " ", "1,4", "30000" };

            parser.Should().Throw<ArgumentException>()
                .Where(exception => exception.Message.StartsWith("was found invalid format date"));
        }

        [Test]
        public void Parse_OnInvalidBonusCode_ThrowsArgumentException()
        {
            invalidEmployeeRow = new[] { "1", "Some Full Name", "01.05.2016", "", "1-4", "30000" };

            parser.Should().Throw<ArgumentException>()
                .Where(exception => exception.Message.StartsWith("incorrect bonus code format"));
        }

        [Test]
        public void Parse_OnInvalidSalary_ThrowsArgumentException()
        {
            invalidEmployeeRow = new[] { "1", "Some Full Name", "01.05.2016", "", "1,4", null };

            parser.Should().Throw<ArgumentException>()
                .Where(exception => exception.Message.StartsWith("was found incorrect salary format"));
        }
    }
}