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
            Set(x => x.State, state);
            return this;
        }

        public virtual DemographicBuilder WithMinimumAge(int minimumAge)
        {
            Set(x => x.MinimumAge, minimumAge);
            return this;
        }

        public virtual DemographicBuilder WithMaximumAge(int maximumAge)
        {
            Set(x => x.MaximumAge, maximumAge);
            return this;
        }

        protected override Demographic BuildObject()
        {
            return new Demographic(Get(x => x.State), Get(x => x.MinimumAge), Get(x => x.MaximumAge));
        }
    }
}
