using NUnit.Framework;
using Shouldly;
using TestStack.Dossier;
using TestStack.Dossier.EquivalenceClasses;

namespace TestingPatternsWithDossier.Tests._04_ValueGeneration
{
    public class CalculatorTests
    {
        [Test]
        public void ShouldAddSinglePositiveIntegerFromInitialState()
        {
            var x = Any.PositiveInteger();

            var result = _calculator.Add(x);

            result.ShouldBe(x);
        }

        [Test]
        public void ShouldAddTwoPositiveIntegersConsecutivelyFromInitialState()
        {
            var x = Any.PositiveInteger();
            var y = Any.PositiveInteger();
            var expected = x + y;

            _calculator.Add(x);
            var result = _calculator.Add(y);

            result.ShouldBe(expected);
        }

        [SetUp]
        public void Setup()
        {
            _calculator = new Calculator();
        }

        private Calculator _calculator;
        // ReSharper disable once InconsistentNaming
        private AnonymousValueFixture Any = new AnonymousValueFixture();

    }
}
