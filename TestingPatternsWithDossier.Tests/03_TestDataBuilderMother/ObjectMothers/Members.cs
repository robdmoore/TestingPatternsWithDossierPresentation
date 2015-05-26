using TestingPatternsWithDossier.Tests._03_TestDataBuilderMother.Builders;

namespace TestingPatternsWithDossier.Tests._03_TestDataBuilderMother.ObjectMothers
{
    static partial class ObjectMother
    {
        public static class Members
        {
            public static MemberBuilder Fred
            {
                get { return new MemberBuilder(); }
            }
        }
    }
}
