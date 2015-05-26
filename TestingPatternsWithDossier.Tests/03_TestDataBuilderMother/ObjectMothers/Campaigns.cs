using System;
using TestingPatternsWithDossier.Tests._03_TestDataBuilderMother.Builders;

namespace TestingPatternsWithDossier.Tests._03_TestDataBuilderMother.ObjectMothers
{
    static partial class ObjectMother
    {
        public static class Campaigns
        {
            public static CampaignBuilder NotStarted(DateTime now)
            {
                return new CampaignBuilder()
                    .StartingAt(now.AddDays(1))
                    .EndingAt(now.AddDays(2));
            }

            public static CampaignBuilder Ended(DateTime now)
            {
                return new CampaignBuilder()
                    .StartingAt(now.AddDays(-2))
                    .EndingAt(now.AddDays(-1));
            }

            public static CampaignBuilder Current(DateTime now)
            {
                return new CampaignBuilder()
                    .StartingAt(now.AddDays(-1))
                    .EndingAt(now.AddDays(1));
            }
        }
    }
}
