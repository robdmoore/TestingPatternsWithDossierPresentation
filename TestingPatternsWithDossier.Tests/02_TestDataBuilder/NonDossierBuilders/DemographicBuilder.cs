using TestingPatternsWithDossier.Models;

namespace TestingPatternsWithDossier.Tests._02_TestDataBuilder.NonDossierBuilders
{
    public class DemographicBuilder
    {
        private State? _state;
        private int? _minimumAge;
        private int? _maximumAge;

        public DemographicBuilder()
        {
            ForAllMembers();
        }

        public DemographicBuilder ForAllMembers()
        {
            _state = null;
            _minimumAge = null;
            _maximumAge = null;
            return this;
        }

        public DemographicBuilder ForState(State state)
        {
            _state = state;
            return this;
        }

        public DemographicBuilder WithMinimumAge(int minimumAge)
        {
            _minimumAge = minimumAge;
            return this;
        }

         DemographicBuilder WithMaximumAge(int maximumAge)
        {
            _maximumAge = maximumAge;
            return this;
        }

        public Demographic Build()
        {
            return new Demographic(_state, _minimumAge, _maximumAge);
        }
    }
}
