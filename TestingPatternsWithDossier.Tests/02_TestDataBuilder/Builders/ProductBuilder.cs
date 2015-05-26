using System;
using System.Collections.Generic;
using TestingPatternsWithDossier.Models;
using TestStack.Dossier;
using TestStack.Dossier.Factories;

namespace TestingPatternsWithDossier.Tests._02_TestDataBuilder.Builders
{
    public class ProductBuilder : TestDataBuilder<Product, ProductBuilder>
    {
        List<Tuple<DateTime, Campaign>> _campaigns = new List<Tuple<DateTime, Campaign>>();

        public virtual ProductBuilder WithName(string name)
        {
            return Set(x => x.Name, name);
        }

        public virtual ProductBuilder WithNoCampaigns()
        {
            _campaigns = new List<Tuple<DateTime, Campaign>>();
            return this;
        }

        public virtual ProductBuilder WithCampaign(DateTime now, Func<CampaignBuilder, CampaignBuilder> modifier = null)
        {
            var c = GetChildBuilder<Campaign, CampaignBuilder>(modifier).Build();
            _campaigns.Add(Tuple.Create(now, c));
            return this;
        }

        protected override Product BuildObject()
        {
            var product = BuildUsing<CallConstructorFactory>();

            foreach (var campaign in _campaigns)
                product.CreateCampaign(
                    campaign.Item1,
                    campaign.Item2.Demographic,
                    campaign.Item2.StartDate,
                    campaign.Item2.EndDate
                );

            return product;
        }
    }
}
