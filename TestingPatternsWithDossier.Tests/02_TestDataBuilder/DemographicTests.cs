using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using TestingPatternsWithDossier.Models;
using TestingPatternsWithDossier.Tests._02_TestDataBuilder.Builders;

namespace TestingPatternsWithDossier.Tests._02_TestDataBuilder
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
            var member = new MemberBuilder().Build();
            var demographic = new DemographicBuilder().ForAllMembers().Build();

            var applies = demographic.Contains(member, DateTime.UtcNow);

            Assert.That(applies, Is.True);
        }

        [Test]
        [Combinatorial]
        public void GivenDemographicForAnyAgeAndSpecificState_WhenCheckingIfTheDemographicAppliesToAMember_ThenReturnTrueOnlyIfMemberIsInThatState
            ([ValueSource("AllStates")] State memberState, [ValueSource("AllStates")] State demographicState)
        {
            var member = new MemberBuilder().InState(memberState).Build();
            var demographic = new DemographicBuilder().ForState(demographicState).Build();

            var applies = demographic.Contains(member, DateTime.UtcNow);

            Assert.That(applies, Is.EqualTo(memberState == demographicState));
        }

        [Test]
        [Combinatorial]
        public void GivenDemographicForAnyStateWithMinimumAge_WhenCheckingIfTheDemographicAppliesToAMember_ThenReturnTrueOnlyIfMemberIsThatAgeOrOlder
            ([Range(1, 25)] int memberAge, [Range(1, 25)] int minimumAge)
        {
            var now = DateTime.UtcNow;
            var member = new MemberBuilder().WithDateOfBirth(now.AddYears(-(memberAge + 1)).AddSeconds(1)).Build();
            var demographic = new DemographicBuilder().WithMinimumAge(minimumAge).Build();

            var applies = demographic.Contains(member, now);

            Assert.That(applies, Is.EqualTo(memberAge >= minimumAge));
        }

        [Test]
        [Combinatorial]
        public void GivenDemographicForAnyStateWithMaximumAgeAndMemberOlderThanThatAge_WhenCheckingIfTheDemographicAppliesToAMember_ThenReturnTrueOnlyIfMemberIsThatAgeOrYounger
            ([Range(1, 25)] int memberAge, [Range(1, 25)] int maximumAge)
        {
            var now = DateTime.UtcNow;
            var member = new MemberBuilder().WithAge(memberAge, now).Build();
            var demographic = new DemographicBuilder().WithMaximumAge(maximumAge).Build();

            var applies = demographic.Contains(member, now);

            Assert.That(applies, Is.EqualTo(memberAge <= maximumAge));
        }

        [Test]
        [Combinatorial]
        public void GivenDemographicWithStateAndAgeRange_WhenCheckingIfTheDemographicAppliesToAMember_ThenReturnTrueOnlyIfTheMemberConformsToAllParameters
            ([Range(1, 25)] int age, [ValueSource("AllStates")] State state)
        {
            var now = DateTime.UtcNow;
            var member = new MemberBuilder()
                .InState(state)
                .WithAge(age, now)
                .Build();
            var demographic = new DemographicBuilder()
                .ForState(State.Wa)
                .WithMinimumAge(18)
                .WithMaximumAge(19)
                .Build();

            var applies = demographic.Contains(member, now);

            Assert.That(applies, Is.EqualTo(state == State.Wa && (age >= 18 && age <= 19)));
        }
    }
}
