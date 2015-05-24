using System.Linq;

namespace TestingPatternsWithDossier
{
    public interface IQuery<in TIn, out TOut>
    {
        TOut Query(IQueryable<TIn> source);
    }
}
