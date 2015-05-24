using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using TestingPatternsWithDossier.Models;

namespace TestingPatternsWithDossier.Tests._00_NoPatterns
{
    class DemographicTests
    {
        private static IEnumerable<State> AllStates()
        {
            return Enum.GetValues(typeof(State)).Cast<int>().Select(s => (State)s).ToArray();
        }

        [Test]
        public void GivenDemographicForAllMembers_WhenCheckingIfTheDemographicAppliesToAMember_ThenReturnTrue()
        {
            var member = new Member("Name", State.Wa, new DateTime(1970, 1, 1));
            var demographic = Demographic.AllMembers;

            var applies = demographic.Contains(member, DateTime.UtcNow);

            Assert.That(applies, Is.True);
        }

        [Test]
        [Combinatorial]
        public void GivenDemographicForAnyAgeAndSpecificState_WhenCheckingIfTheDemographicAppliesToAMember_ThenReturnTrueOnlyIfMemberIsInThatState
            ([ValueSource("AllStates")] State memberState, [ValueSource("AllStates")] State demographicState)
        {
            var member = new Member("Name", memberState, new DateTime(1970, 1, 1));
            var demographic = new Demographic(demographicState, null, null);

            var applies = demographic.Contains(member, DateTime.UtcNow);

            Assert.That(applies, Is.EqualTo(memberState == demographicState));
        }

        [Test]
        [Combinatorial]
        public void GivenDemographicForAnyStateWithMinimumAge_WhenCheckingIfTheDemographicAppliesToAMember_ThenReturnTrueOnlyIfMemberIsThatAgeOrOlder
            ([Range(1, 25)] int memberAge, [Range(1, 25)] int minimumAge)
        {
            var now = DateTime.UtcNow;
            var member = new Member("Name", State.Wa, now.AddYears(-(memberAge+1)).AddSeconds(1));
            var demographic = new Demographic(null, minimumAge, null);

            var applies = demographic.Contains(member, now);

            Assert.That(applies, Is.EqualTo(memberAge >= minimumAge));
        }

        [Test]
        [Combinatorial]
        public void GivenDemographicForAnyStateWithMaximumAgeAndMemberOlderThanThatAge_WhenCheckingIfTheDemographicAppliesToAMember_ThenReturnTrueOnlyIfMemberIsThatAgeOrYounger
            ([Range(1, 25)] int memberAge, [Range(1, 25)] int maximumAge)
        {
            var now = DateTime.UtcNow;
            var member = new Member("Name", State.Wa, now.AddYears(-memberAge));
            var demographic = new Demographic(null, null, maximumAge);

            var applies = demographic.Contains(member, now);

            Assert.That(applies, Is.EqualTo(memberAge <= maximumAge));
        }

        [Test]
        [Combinatorial]
        public void GivenDemographicWithStateAndAgeRange_WhenCheckingIfTheDemographicAppliesToAMember_ThenReturnTrueOnlyIfTheMemberConformsToAllParameters
            ([Range(1, 25)] int age, [ValueSource("AllStates")] State state)
        {
            var now = DateTime.UtcNow;
            var member = new Member("Name", state, now.AddYears(-age));
            var demographic = new Demographic(State.Wa, 18, 19);

            var applies = demographic.Contains(member, now);

            Assert.That(applies, Is.EqualTo(state == State.Wa && (age == 18 || age == 19)));
        }
    }
}
