using System;
using TestingPatternsWithDossier.Models;
using TestStack.Dossier;

namespace TestingPatternsWithDossier.Tests._02_TestDataBuilder.Builders
{
    public class MemberBuilder : TestDataBuilder<Member, MemberBuilder>
    {
        public MemberBuilder()
        {
            Set(x => x.Name, "Fred");
            Set(x => x.State, State.Wa);
            Set(x => x.Dob, new DateTime(1970, 1, 1));
        }

        public virtual MemberBuilder InState(State state)
        {
            Set(x => x.State, state);
            return this;
        }

        public virtual MemberBuilder WithDateOfBirth(DateTime dob)
        {
            Set(x => x.Dob, dob);
            return this;
        }

        public virtual MemberBuilder WithAge(int age, DateTime now)
        {
            Set(x => x.Dob, now.AddYears(-age));
            return this;
        }

        protected override Member BuildObject()
        {
            return new Member(Get(x => x.Name), Get(x => x.State), Get(x => x.Dob));
        }
    }
}
