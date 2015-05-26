using System;
using System.Linq;
using NUnit.Framework;
using TestingPatternsWithDossier.Models;
using TestingPatternsWithDossier.Queries;
using TestingPatternsWithDossier.Tests.TestHelpers;
using TestingPatternsWithDossier.Tests._01_ObjectMother.ObjectMothers;

namespace TestingPatternsWithDossier.Tests._01_ObjectMother
{
    class GetProductsForMemberTests : QueryTestBase
    {
        private readonly Member _member = ObjectMother.Members.Fred;
        private readonly DateTime _now = DateTime.UtcNow;

        [Test]
        public void GivenNoProducts_WhenQuerying_ThenReturnNoResults()
        {
            var result = Execute(new GetProductsForMember(_now, _member));

            Assert.That(result, Is.Empty);
        }

        [Test]
        public void GivenProductsWithNoCampaignOrACampaignThatIsntCurrent_WhenQuerying_ThenReturnNoResults()
        {
            var products = new[]
            {
                ObjectMother.Products.NoCampaign,
                ObjectMother.Products.NotStarted(_now),
                ObjectMother.Products.Ended(_now)
            };
            products.ToList().ForEach(p => Session.Save(p));

            var result = Execute(new GetProductsForMember(_now, _member));

            Assert.That(result, Is.Empty);
        }

        [Test]
        public void GivenProductsWithCurrentCampaignWithSomeThatApplyToTheMember_WhenQuerying_ThenReturnTheProductsThatApplyToTheMember()
        {
            var member = ObjectMother.Members.InWaWithAge(10, _now);
            var products = new[]
            {
                ObjectMother.Products.CurrentForAllMembers(_now),
                ObjectMother.Products.CurrentForAllActMembers(_now),
                ObjectMother.Products.CurrentForWaMembersBetween9And11YearsOld(_now)
            };
            products.ToList().ForEach(p => Session.Save(p));

            var result = Execute(new GetProductsForMember(_now, member));

            Assert.That(result.Select(p => p.Name).ToArray(), Is.EqualTo(new[]{products[0].Name, products[2].Name}));
        }
    }
}
