using FluentAssertions;
using NUnit.Framework;
using System;
using System.Linq;

namespace DispenserTask.Models.Tests
{
    [TestFixture]
    public class AtmTests : BaseTests
    {
        [TestCase(0)]
        [TestCase(-23)]
        public void Dispense_Throws_If_MoneyAmount_Is_Ivalid(int amount)
        {
            FluentActions.Invoking(() => Atm.Dispense(amount))
                         .Should()
                         .Throw<ArgumentException>()
                         .WithParameterName("amount");
        }

        [TestCase(50)]
        [TestCase(235)]
        [TestCase(1000)]
        public void DispenseCash_Returns_Bills(int moneyAmount)
        {
            // Act
            var result = Atm.Dispense(moneyAmount);

            // Assert
            result.Sum(x => x.NominalValue).Should().Be(moneyAmount);
        }

        [Test]
        public void Dispense_Throws_If_Dispense_Max_Count_Exceeded()
        {
            // Act & Assert
            FluentActions.Invoking(() => Atm.Dispense(10000))
                         .Should()
                         .Throw<ArgumentException>()
                         .WithMessage("It is impossible to withdraw cash from an ATM*")
                         .WithMessage($"*{Atm.MaxBillsCountToProcess}*")
                         .WithParameterName("amount");
        }
    }
}