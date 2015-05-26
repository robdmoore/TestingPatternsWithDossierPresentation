﻿using System;
using TestingPatternsWithDossier.Models;
using TestStack.Dossier;

namespace TestingPatternsWithDossier.Tests._02_TestDataBuilder.Builders
{
    public class CampaignBuilder : TestDataBuilder<Campaign, CampaignBuilder>
    {
        private readonly DemographicBuilder _demographic = new DemographicBuilder();

        public CampaignBuilder()
        {
            _demographic.ForAllMembers();
            Set(x => x.StartDate, new DateTime(2000, 1, 1));
            Set(x => x.EndDate, new DateTime(2000, 1, 2));
        }

        public virtual CampaignBuilder ForAllMembers()
        {
            _demographic.ForAllMembers();
            return this;
        }

        public virtual CampaignBuilder ForState(State state)
        {
            _demographic.ForState(state);
            return this;
        }

        public virtual CampaignBuilder WithMinimumAge(int minimumAge)
        {
            _demographic.WithMinimumAge(minimumAge);
            return this;
        }

        public virtual CampaignBuilder WithMaximumAge(int maximumAge)
        {
            _demographic.WithMaximumAge(maximumAge);
            return this;
        }

        public virtual CampaignBuilder StartingAt(DateTime startDate)
        {
            Set(x => x.StartDate, startDate);
            return this;
        }

        public virtual CampaignBuilder EndingAt(DateTime endDate)
        {
            Set(x => x.EndDate, endDate);
            return this;
        }

        protected override Campaign BuildObject()
        {
            return new Campaign(_demographic.Build(), Get(x => x.StartDate), Get(x => x.EndDate));
        }
    }
}