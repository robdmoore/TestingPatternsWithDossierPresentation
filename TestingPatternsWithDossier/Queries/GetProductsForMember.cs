using System;
using System.Collections.Generic;
using System.Linq;
using TestingPatternsWithDossier.Models;

namespace TestingPatternsWithDossier.Queries
{
    public class GetProductsForMember : IQuery<Product, IList<Product>>
    {
        private readonly DateTime _now;
        private readonly Member _member;

        public GetProductsForMember(DateTime now, Member member)
        {
            _now = now;
            _member = member;
        }

        public IList<Product> Query(IQueryable<Product> source)
        {
            var productsFromDb = source.Where(p => p.LatestCampaign != null)
                .Where(p => p.LatestCampaign.StartDate <= _now)
                .Where(p => p.LatestCampaign.EndDate >= _now)
                .ToList();
            
            return productsFromDb
                .Where(p => p.LatestCampaign.Demographic.Contains(_member, _now))
                .ToList();
        }
    }
}
