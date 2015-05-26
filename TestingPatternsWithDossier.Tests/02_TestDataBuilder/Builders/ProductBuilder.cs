using System;
using System.Collections.Generic;
using TestingPatternsWithDossier.Models;
using TestStack.Dossier;

namespace TestingPatternsWithDossier.Tests._02_TestDataBuilder.Builders
{
    public class ProductBuilder : TestDataBuilder<Product, ProductBuilder>
    {
        List<Tuple<DateTime, Campaign>> _campaigns = new List<Tuple<DateTime, Campaign>>();

        public ProductBuilder()
        {
            Set(x => x.Name, "A product");
        }

        public virtual ProductBuilder WithName(string name)
        {
            Set(x => x.Name, name);
            return this;
        }

        public virtual ProductBuilder WithNoCampaigns()
        {
            _campaigns = new List<Tuple<DateTime, Campaign>>();
            return this;
        }

        public virtual ProductBuilder WithCampaign(DateTime now, CampaignBuilder campaign)
        {
            _campaigns.Add(Tuple.Create(now, campaign.Build()));
            return this;
        }

        protected override Product BuildObject()
        {
            var product = new Product(Get(x => x.Name));

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
