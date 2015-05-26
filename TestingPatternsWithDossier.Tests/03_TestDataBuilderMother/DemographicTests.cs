using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using TestingPatternsWithDossier.Models;
using TestingPatternsWithDossier.Tests._03_TestDataBuilderMother.ObjectMothers;

namespace TestingPatternsWithDossier.Tests._03_TestDataBuilderMother
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
            var member = ObjectMother.Members.Fred.Build();
            var demographic = ObjectMother.Demographics.AllMembers.Build();

            var applies = demographic.Contains(member, DateTime.UtcNow);

            Assert.That(applies, Is.True);
        }

        [Test]
        [Combinatorial]
        public void GivenDemographicForAnyAgeAndSpecificState_WhenCheckingIfTheDemographicAppliesToAMember_ThenReturnTrueOnlyIfMemberIsInThatState
            ([ValueSource("AllStates")] State memberState, [ValueSource("AllStates")] State demographicState)
        {
            var member = ObjectMother.Members.Fred.InState(memberState).Build();
            var demographic = ObjectMother.Demographics.Blank.ForState(demographicState).Build();

            var applies = demographic.Contains(member, DateTime.UtcNow);

            Assert.That(applies, Is.EqualTo(memberState == demographicState));
        }

        [Test]
        [Combinatorial]
        public void GivenDemographicForAnyStateWithMinimumAge_WhenCheckingIfTheDemographicAppliesToAMember_ThenReturnTrueOnlyIfMemberIsThatAgeOrOlder
            ([Range(1, 25)] int memberAge, [Range(1, 25)] int minimumAge)
        {
            var now = DateTime.UtcNow;
            var member = ObjectMother.Members.Fred.WithDateOfBirth(now.AddYears(-(memberAge + 1)).AddSeconds(1)).Build();
            var demographic = ObjectMother.Demographics.Blank.WithMinimumAge(minimumAge).Build();

            var applies = demographic.Contains(member, now);

            Assert.That(applies, Is.EqualTo(memberAge >= minimumAge));
        }

        [Test]
        [Combinatorial]
        public void GivenDemographicForAnyStateWithMaximumAgeAndMemberOlderThanThatAge_WhenCheckingIfTheDemographicAppliesToAMember_ThenReturnTrueOnlyIfMemberIsThatAgeOrYounger
            ([Range(1, 25)] int memberAge, [Range(1, 25)] int maximumAge)
        {
            var now = DateTime.UtcNow;
            var member = ObjectMother.Members.Fred.WithAge(memberAge, now).Build();
            var demographic = ObjectMother.Demographics.Blank.WithMaximumAge(maximumAge).Build();

            var applies = demographic.Contains(member, now);

            Assert.That(applies, Is.EqualTo(memberAge <= maximumAge));
        }

        [Test]
        [Combinatorial]
        public void GivenDemographicWithStateAndAgeRange_WhenCheckingIfTheDemographicAppliesToAMember_ThenReturnTrueOnlyIfTheMemberConformsToAllParameters
            ([Range(1, 25)] int age, [ValueSource("AllStates")] State state)
        {
            var now = DateTime.UtcNow;
            var member = ObjectMother.Members.Fred
                .InState(state)
                .WithAge(age, now)
                .Build();
            var demographic = ObjectMother.Demographics.Blank
                .ForState(State.Wa)
                .WithMinimumAge(18)
                .WithMaximumAge(19)
                .Build();

            var applies = demographic.Contains(member, now);

            Assert.That(applies, Is.EqualTo(state == State.Wa && (age >= 18 && age <= 19)));
        }
    }
}
