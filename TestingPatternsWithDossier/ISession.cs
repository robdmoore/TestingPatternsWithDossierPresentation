using System.Linq;

namespace TestingPatternsWithDossier
{
    public interface ISession
    {
        IQueryable<T> Query<T>();
        void Save(object o);
    }
}
