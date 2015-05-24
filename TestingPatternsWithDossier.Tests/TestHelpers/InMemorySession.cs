using System.Collections;
using System.Linq;

namespace TestingPatternsWithDossier.Tests.TestHelpers
{
    class InMemorySession : ISession
    {
        readonly ArrayList _database = new ArrayList();

        public IQueryable<T> Query<T>()
        {
            return _database.OfType<T>().AsQueryable();
        }

        public void Save(object o)
        {
            _database.Add(o);
        }
    }
}
