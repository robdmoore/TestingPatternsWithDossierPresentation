using System;
using TestingPatternsWithDossier.Models;
using TestStack.Dossier;

namespace TestingPatternsWithDossier.Tests._03_TestDataBuilderMother.Builders
{
    public class MemberBuilder : TestDataBuilder<Member, MemberBuilder>
    {
        public MemberBuilder()
        {
            WithDateOfBirth(new DateTime(1970, 1, 1));
        }

        public virtual MemberBuilder InState(State state)
        {
            return Set(x => x.State, state);
        }

        public virtual MemberBuilder WithDateOfBirth(DateTime dob)
        {
            return Set(x => x.Dob, dob);
        }

        public virtual MemberBuilder WithAge(int age, DateTime now)
        {
            return Set(x => x.Dob, now.AddYears(-age));
        }
    }
}
