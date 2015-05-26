using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using TestingPatternsWithDossier.Models;
using TestingPatternsWithDossier.Tests._01_ObjectMother.ObjectMothers;

namespace TestingPatternsWithDossier.Tests._01_ObjectMother
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
            var member = ObjectMother.Members.Fred;
            var demographic = ObjectMother.Demographics.AllMembers;

            var applies = demographic.Contains(member, DateTime.UtcNow);

            Assert.That(applies, Is.True);
        }

        [Test]
        [Combinatorial]
        public void GivenDemographicForAnyAgeAndSpecificState_WhenCheckingIfTheDemographicAppliesToAMember_ThenReturnTrueOnlyIfMemberIsInThatState
            ([ValueSource("AllStates")] State memberState, [ValueSource("AllStates")] State demographicState)
        {
            var member = ObjectMother.Members.ForState(memberState);
            var demographic = ObjectMother.Demographics.ForState(demographicState);

            var applies = demographic.Contains(member, DateTime.UtcNow);

            Assert.That(applies, Is.EqualTo(memberState == demographicState));
        }

        [Test]
        [Combinatorial]
        public void GivenDemographicForAnyStateWithMinimumAge_WhenCheckingIfTheDemographicAppliesToAMember_ThenReturnTrueOnlyIfMemberIsThatAgeOrOlder
            ([Range(1, 25)] int memberAge, [Range(1, 25)] int minimumAge)
        {
            var now = DateTime.UtcNow;
            var member = ObjectMother.Members.WithDateOfBirth(now.AddYears(-(memberAge + 1)).AddSeconds(1));
            var demographic = ObjectMother.Demographics.WithMinimumAge(minimumAge);

            var applies = demographic.Contains(member, now);

            Assert.That(applies, Is.EqualTo(memberAge >= minimumAge));
        }

        [Test]
        [Combinatorial]
        public void GivenDemographicForAnyStateWithMaximumAgeAndMemberOlderThanThatAge_WhenCheckingIfTheDemographicAppliesToAMember_ThenReturnTrueOnlyIfMemberIsThatAgeOrYounger
            ([Range(1, 25)] int memberAge, [Range(1, 25)] int maximumAge)
        {
            var now = DateTime.UtcNow;
            var member = ObjectMother.Members.WithAge(memberAge, now);
            var demographic = ObjectMother.Demographics.WithMaximumAge(maximumAge);

            var applies = demographic.Contains(member, now);

            Assert.That(applies, Is.EqualTo(memberAge <= maximumAge));
        }

        [Test]
        [Combinatorial]
        public void GivenDemographicWithStateAndAgeRange_WhenCheckingIfTheDemographicAppliesToAMember_ThenReturnTrueOnlyIfTheMemberConformsToAllParameters
            ([Range(1, 25)] int age, [ValueSource("AllStates")] State state)
        {
            var now = DateTime.UtcNow;
            var member = ObjectMother.Members.WithAgeAndState(age, now, state);
            var demographic = ObjectMother.Demographics.WithStateAndAgeRange;

            var applies = demographic.Contains(member, now);

            Assert.That(applies, Is.EqualTo(state == demographic.State && (age >= demographic.MinimumAge && age <= demographic.MaximumAge)));
        }
    }
}
