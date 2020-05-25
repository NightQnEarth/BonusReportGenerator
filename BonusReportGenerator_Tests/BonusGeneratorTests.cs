using System;
using System.Collections.Generic;
using System.Linq;
using BonusReportGenerator.Bonus;
using BonusReportGenerator.TableParsers;
using FluentAssertions;
using NUnit.Framework;

namespace BonusReportGenerator_Tests
{
    [TestFixture]
    public class BonusGeneratorTests
    {
        private Employee[] employees;
        private Contract[] contracts;
        private DateTime startDate;
        private DateTime finalDate;
        private Func<IEnumerable<EmployeeBonus>> generator;

        [SetUp]
        public void SetUp()
        {
            employees = new[]
            {
                new Employee(0, "Some Full Name", new DateTime(2016, 01, 01),
                             new DateTime(2019, 01, 01), new[] { 1 }, 30000),
                new Employee(1, "Some Full Name", new DateTime(2016, 01, 01),
                             new DateTime(2019, 01, 01), new[] { 2 }, 30000),
                new Employee(2, "Some Full Name", new DateTime(2016, 01, 01),
                             new DateTime(2019, 01, 01), new[] { 3 }, 30000),
                new Employee(3, "Some Full Name", new DateTime(2016, 01, 01),
                             new DateTime(2019, 01, 01), new[] { 4 }, 30000),
                new Employee(4, "Some Full Name", new DateTime(2016, 01, 01),
                             new DateTime(2019, 01, 01), new[] { 1, 2, 3, 4 }, 30000)
            };
            contracts = new[]
            {
                new Contract(0, new DateTime(2018, 01, 02), 100000),
                new Contract(1, new DateTime(2018, 01, 02), 100000),
                new Contract(2, new DateTime(2018, 01, 02), 100000),
                new Contract(3, new DateTime(2018, 01, 02), 100000),
                new Contract(4, new DateTime(2018, 01, 02), 100000)
            };
            startDate = new DateTime(2017, 01, 01);
            finalDate = new DateTime(2020, 01, 01);

            generator = () => BonusGenerator.Generate(employees, contracts, startDate, finalDate);
        }

        [Test]
        public void Generator_OnCorrectInput_ReturnsFirstBonus()
        {
            const double expectedBonus = 100000 * 0.05;

            generator().Should().ContainEquivalentOf(new EmployeeBonus(0, expectedBonus));
        }

        [Test]
        public void Generator_OnCorrectInput_ReturnsSecondBonus()
        {
            const double expectedBonus = 100000 * 0.10;

            generator().Should().ContainEquivalentOf(new EmployeeBonus(1, expectedBonus));
        }

        [Test]
        public void Generator_OnCorrectInput_ReturnsThirdBonus()
        {
            const double expectedBonus = 100000 * 0.07;

            generator().Should().ContainEquivalentOf(new EmployeeBonus(2, expectedBonus));
        }

        [Test]
        public void Generator_OnCorrectInput_ReturnsFourthBonus()
        {
            var fullMonthsBetween = (employees[3].DismissDate.Year - startDate.Year) * 12;
            var expectedBonus = 30000 * 0.02 * fullMonthsBetween;

            generator().Should().ContainEquivalentOf(new EmployeeBonus(3, expectedBonus));
        }

        [Test]
        public void Generator_WhenEmployeeHasSomeBonuses_ReturnsSumOfAllBonuses()
        {
            const double expectedFirstBonus = 100000 * 0.05;
            const double expectedSecondBonus = 100000 * 0.10;
            const double expectedThirdBonus = 100000 * 0.07;
            var fullMonthsBetween = (employees[4].DismissDate.Year - startDate.Year) * 12;
            var expectedFourthBonus = 30000 * 0.02 * fullMonthsBetween;

            var bonusesSum = expectedFirstBonus + expectedSecondBonus + expectedThirdBonus + expectedFourthBonus;

            generator().Should().ContainEquivalentOf(new EmployeeBonus(4, bonusesSum));
        }

        [Test]
        public void Generate_OnEmptyContractsList_AllBonusesAreZero()
        {
            employees = employees.Take(3).ToArray();
            contracts = new Contract[] { };

            var bonuses = generator();

            bonuses.Select(bonus => bonus.Bonus).Should().AllBeEquivalentTo(0);
        }

        [Test]
        public void Generate_OnFinalDateLessThanStartDate_ThrowsArgumentException()
        {
            startDate = DateTime.MaxValue;
            finalDate = DateTime.MinValue;

            generator.Should().Throw<ArgumentException>()
                .Where(exception => exception.Message.Contains("cannot be less than"));
        }
    }
}