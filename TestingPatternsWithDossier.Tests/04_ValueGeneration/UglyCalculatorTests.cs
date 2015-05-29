using NUnit.Framework;
using Shouldly;
using TestStack.Dossier;

namespace TestingPatternsWithDossier.Tests._04_ValueGeneration
{
    public class UglyCalculatorTests
    {
        [Test]
        public void ShouldAdd1FromInitialState()
        {
            var result = _calculator.Add(1);

            result.ShouldBe(1);
        }

        [Test]
        public void ShouldAdd1And2ConsecutivelyFromInitialState()
        {
            _calculator.Add(1);
            var result = _calculator.Add(2);

            result.ShouldBe(3);
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
