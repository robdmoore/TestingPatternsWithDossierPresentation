using System;
using TestingPatternsWithDossier.Models;

namespace TestingPatternsWithDossier.Tests._01_ObjectMother.ObjectMothers
{
    static partial class ObjectMother
    {
        public static class Members
        {
            public static Member Fred
            {
                get { return new Member("Fred", State.Wa, new DateTime(1970, 1, 1)); }
            }

            public static Member ForState(State state)
            {
                return new Member("A Member", state, new DateTime(1970, 1, 1));
            }

            public static Member WithAge(int age, DateTime now)
            {
                return WithDateOfBirth(now.AddYears(-age));
            }

            public static Member WithDateOfBirth(DateTime dob)
            {
                return new Member("A Member", State.Wa, dob);
            }

            public static Member InWaWithAge(int age, DateTime now)
            {
                return new Member("WA member", State.Wa, now.AddYears(-age));
            }

            public static Member WithAgeAndState(int age, DateTime now, State state)
            {
                return new Member("A Member", state, now.AddYears(-age));
            }
        }
    }
}
