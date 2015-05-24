using System;
using System.Linq;
using NUnit.Framework;
using TestingPatternsWithDossier.Models;
using TestingPatternsWithDossier.Queries;
using TestingPatternsWithDossier.Tests.TestHelpers;

namespace TestingPatternsWithDossier.Tests._00_NoPatterns
{
    class GetProductsForMemberTests : QueryTestBase
    {
        private readonly Member _member = new Member("Name", State.Wa, new DateTime(1970, 1, 1));
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
                new Product("Product with no campaign"),
                new Product("Campaign that hasn't started"),
                new Product("Campaign that has ended")
            };
            products[1].CreateCampaign(_now, Demographic.AllMembers, _now.AddDays(1), _now.AddDays(2));
            products[2].CreateCampaign(_now, Demographic.AllMembers, _now.AddDays(-2), _now.AddDays(-1));
            products.ToList().ForEach(p => Session.Save(p));

            var result = Execute(new GetProductsForMember(_now, _member));

            Assert.That(result, Is.Empty);
        }

        [Test]
        public void GivenProductsWithCurrentCampaignWithSomeThatApplyToTheMember_WhenQuerying_ThenReturnTheProductsThatApplyToTheMember()
        {
            var member = new Member("Name", State.Wa, _now.AddYears(-10).AddDays(-1));
            var products = new[]
            {
                new Product("Product 1 (applies)"),
                new Product("Product 2 (doesn't apply)"),
                new Product("Product 3 (applies)")
            };
            products[0].CreateCampaign(_now, Demographic.AllMembers, _now.AddDays(-1), _now.AddDays(1));
            products[1].CreateCampaign(_now, new Demographic(State.Act, null, null), _now.AddDays(-1), _now.AddDays(1));
            products[2].CreateCampaign(_now, new Demographic(State.Wa, 9, 11), _now.AddDays(-1), _now.AddDays(1));
            products.ToList().ForEach(p => Session.Save(p));

            var result = Execute(new GetProductsForMember(_now, member));

            Assert.That(result.Select(p => p.Name).ToArray(), Is.EqualTo(new[]{products[0].Name, products[2].Name}));
        }
    }
}
