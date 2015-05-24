using System;

namespace TestingPatternsWithDossier.Models
{
    public class Demographic
    {
        public Demographic(State? state, int? minimumAge, int? maximumAge)
        {
            State = state;
            MinimumAge = minimumAge;
            MaximumAge = maximumAge;
        }

        public State? State { get; protected set; }
        public int? MinimumAge { get; protected set; }
        public int? MaximumAge { get; protected set; }

        public static Demographic AllMembers { get{ return new Demographic(null, null, null); } }

        public bool Contains(Member member, DateTime now)
        {
            var age = member.GetAge(now);

            if (MinimumAge != null && age < MinimumAge)
                return false;

            if (MaximumAge != null && age > MaximumAge)
                return false;

            if (State != null && member.State != State)
                return false;

            return true;
        }
    }
}
