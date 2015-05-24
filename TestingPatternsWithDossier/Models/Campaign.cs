using System;

namespace TestingPatternsWithDossier.Models
{
    public class Campaign
    {
        public Campaign(Demographic demographic, DateTime startDate, DateTime endDate)
        {
            Demographic = demographic;
            StartDate = startDate;
            EndDate = endDate;
        }

        public Demographic Demographic { get; protected set; }
        public DateTime StartDate { get; protected set; }
        public DateTime EndDate { get; protected set; }

        public virtual CampaignStatus GetStatus(DateTime now)
        {
            if (StartDate > now)
                return CampaignStatus.NotStarted;

            if (EndDate < now)
                return CampaignStatus.Running;

            return CampaignStatus.Ended;
        }
    }

    public enum CampaignStatus
    {
        NotStarted,
        Ended,
        Running
    }
}
