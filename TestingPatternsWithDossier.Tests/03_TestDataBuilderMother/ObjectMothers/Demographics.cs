using TestingPatternsWithDossier.Tests._03_TestDataBuilderMother.Builders;

namespace TestingPatternsWithDossier.Tests._03_TestDataBuilderMother.ObjectMothers
{
    static partial class ObjectMother
    {
        public static class Demographics
        {
            public static DemographicBuilder Blank
            {
                get { return new DemographicBuilder(); }
            }

            public static DemographicBuilder AllMembers
            {
                get { return new DemographicBuilder().ForAllMembers(); }
            }
        }
    }
}
