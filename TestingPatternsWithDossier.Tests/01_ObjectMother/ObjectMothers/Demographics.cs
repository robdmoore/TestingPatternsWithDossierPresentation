using TestingPatternsWithDossier.Models;

namespace TestingPatternsWithDossier.Tests._01_ObjectMother.ObjectMothers
{
    static partial class ObjectMother
    {
        public static class Demographics
        {
            public static Demographic AllMembers
            {
                get { return Demographic.AllMembers; }
            }

            public static Demographic WithStateAndAgeRange
            {
                get { return new Demographic(State.Wa, 18, 19); }
            }

            public static Demographic ForState(State state)
            {
                return new Demographic(state, null, null);
            }

            public static Demographic WithMinimumAge(int minimumAge)
            {
                return new Demographic(null, minimumAge, null);
            }

            public static Demographic WithMaximumAge(int maximumAge)
            {
                return new Demographic(null, null, maximumAge);
            }
        }
    }
}
