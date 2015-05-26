using TestingPatternsWithDossier.Models;
using TestStack.Dossier;

namespace TestingPatternsWithDossier.Tests._02_TestDataBuilder.Builders
{
    public class DemographicBuilder : TestDataBuilder<Demographic, DemographicBuilder>
    {
        public DemographicBuilder()
        {
            ForAllMembers();
        }

        public virtual DemographicBuilder ForAllMembers()
        {
            Set(x => x.State, default(State?));
            Set(x => x.MinimumAge, default(int?));
            Set(x => x.MaximumAge, default(int?));
            return this;
        }

        public virtual DemographicBuilder ForState(State state)
        {
            return Set(x => x.State, state);
        }

        public virtual DemographicBuilder WithMinimumAge(int minimumAge)
        {
            return Set(x => x.MinimumAge, minimumAge);
        }

        public virtual DemographicBuilder WithMaximumAge(int maximumAge)
        {
            return Set(x => x.MaximumAge, maximumAge);
        }
    }
}
