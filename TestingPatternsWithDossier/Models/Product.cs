using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace TestingPatternsWithDossier.Models
{
    public class Product
    {
        private readonly ICollection<Campaign> _campaigns = new Collection<Campaign>();

        public Product(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentOutOfRangeException("name", "Please specify a name");
            Name = name;
        }

        public string Name { get; protected set; }
        public Campaign LatestCampaign { get; set; }
        public IEnumerable<Campaign> Campaigns { get { return _campaigns; } }

        public virtual void CreateCampaign(DateTime now, Demographic demographic, DateTime start, DateTime end)
        {
            if (LatestCampaign != null && LatestCampaign.GetStatus(now) != CampaignStatus.Ended)
                throw new InvalidOperationException("You can't start a campaign until the existing one has ended.");

            var campaign = new Campaign(demographic, start, end);
            _campaigns.Add(campaign);
            LatestCampaign = campaign;
        }
    }
}
