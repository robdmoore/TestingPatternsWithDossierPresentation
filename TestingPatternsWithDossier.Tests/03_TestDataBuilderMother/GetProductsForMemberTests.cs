using System;
using System.Linq;
using NUnit.Framework;
using TestingPatternsWithDossier.Models;
using TestingPatternsWithDossier.Queries;
using TestingPatternsWithDossier.Tests.TestHelpers;
using TestingPatternsWithDossier.Tests._02_TestDataBuilder.Builders;
using TestingPatternsWithDossier.Tests._03_TestDataBuilderMother.ObjectMothers;
using TestStack.Dossier.Lists;

namespace TestingPatternsWithDossier.Tests._03_TestDataBuilderMother
{
    class GetProductsForMemberTests : QueryTestBase
    {
        private readonly Member _member = ObjectMother.Members.Fred.Build();
        private readonly DateTime _now = DateTime.UtcNow;

        [Test]
        public void GivenProductsWithNoCampaignOrACampaignThatIsntCurrent_WhenQuerying_ThenReturnNoResults()
        {
            var products = ProductBuilder.CreateListOfSize(3)
                .TheFirst(1)
                    .WithNoCampaigns()
                .TheNext(1)
                    .WithCampaign(_now, c => c.StartingAt(_now.AddDays(1)).EndingAt(_now.AddDays(2)))
                .TheNext(1)
                    .WithCampaign(_now, c => c.StartingAt(_now.AddDays(-2)).EndingAt(_now.AddDays(-1)))
                .BuildList();
            products.ToList().ForEach(p => Session.Save(p));

            var result = Execute(new GetProductsForMember(_now, _member));

            Assert.That(result, Is.Empty);
        }

        [Test]
        public void GivenProductsWithCurrentCampaignWithSomeThatApplyToTheMember_WhenQuerying_ThenReturnTheProductsThatApplyToTheMember()
        {
            var member = new MemberBuilder().InState(State.Wa).WithAge(10, _now).Build();
            var products = ProductBuilder.CreateListOfSize(3)
                .TheFirst(1).WithCampaign(_now, x => x
                    .ForAllMembers()
                    .StartingAt(_now.AddDays(-1))
                    .EndingAt(_now.AddDays(1))
                )
                .TheNext(1).WithCampaign(_now, x => x
                    .ForState(State.Act)
                    .StartingAt(_now.AddDays(-1))
                    .EndingAt(_now.AddDays(1))
                )
                .TheNext(1).WithCampaign(_now, x => x
                    .ForState(State.Wa)
                    .WithMinimumAge(9)
                    .WithMaximumAge(11)
                    .StartingAt(_now.AddDays(-1))
                    .EndingAt(_now.AddDays(1))
                )
                .BuildList();
            products.ToList().ForEach(p => Session.Save(p));

            var result = Execute(new GetProductsForMember(_now, member));

            Assert.That(result.Select(p => p.Name).ToArray(),
                Is.EqualTo(new[]{products[0].Name, products[2].Name}));
        }
    }
}
