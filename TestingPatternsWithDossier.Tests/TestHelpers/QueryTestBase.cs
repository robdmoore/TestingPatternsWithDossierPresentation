using NUnit.Framework;

namespace TestingPatternsWithDossier.Tests.TestHelpers
{
    abstract class QueryTestBase
    {
        protected InMemorySession Session;

        [SetUp]
        public void Setup()
        {
            Session = new InMemorySession();
        }

        protected TOut Execute<TIn, TOut>(IQuery<TIn, TOut> query)
        {
            return query.Query(Session.Query<TIn>());
        }
    }
}
