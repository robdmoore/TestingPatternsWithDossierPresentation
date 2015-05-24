using System;

namespace TestingPatternsWithDossier.Models
{
    public class Member
    {
        public Member(string name, State state, DateTime dob)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentOutOfRangeException("name", "Please supply a name");
            // Yes DateTime.Now should generally be abstracted away - this is demo code
            if (dob > DateTime.Now.Date)
                throw new ArgumentOutOfRangeException("dob", "Please supply a date of birth in the past");

            Name = name;
            State = state;
            Dob = dob;
        }

        public string Name { get; protected set; }
        public State State { get; protected set; }
        public DateTime Dob { get; protected set; }

        public int GetAge(DateTime now)
        {
            var age = now.Year - Dob.Year;
            if (now.AddYears(-1 * age) < Dob)
                age--;
            return age;
        }
    }
}
