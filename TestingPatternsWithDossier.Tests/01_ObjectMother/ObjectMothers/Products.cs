using System;
using TestingPatternsWithDossier.Models;

namespace TestingPatternsWithDossier.Tests._01_ObjectMother.ObjectMothers
{
    static partial class ObjectMother
    {
        public static class Products
        {
            public static Product NoCampaign
            {
                get { return new Product("Product with no campaign"); }
            }

            public static Product NotStarted(DateTime now)
            {
                var product = new Product("Product with campaign that hasn't started");
                product.CreateCampaign(now, Demographic.AllMembers, now.AddDays(1), now.AddDays(2));
                return product;
            }

            public static Product Ended(DateTime now)
            {
                var product = new Product("Product with campaign that has ended");
                product.CreateCampaign(now, Demographic.AllMembers, now.AddDays(-2), now.AddDays(-1));
                return product;
            }

            public static Product CurrentForAllMembers(DateTime now)
            {
                var product = new Product("Product with current campaign for all members");
                product.CreateCampaign(now, Demographic.AllMembers, now.AddDays(-1), now.AddDays(1));
                return product;
            }

            public static Product CurrentForAllActMembers(DateTime now)
            {
                var product = new Product("Product with current campaign for ACT members");
                product.CreateCampaign(now, new Demographic(State.Act, null, null), now.AddDays(-1), now.AddDays(1));
                return product;
            }

            public static Product CurrentForWaMembersBetween9And11YearsOld(DateTime now)
            {
                var product = new Product("Product with current campaign for WA members between 9 and 11");
                product.CreateCampaign(now, new Demographic(State.Wa, 9, 11), now.AddDays(-1), now.AddDays(1));
                return product;
            }
        }
    }
}
