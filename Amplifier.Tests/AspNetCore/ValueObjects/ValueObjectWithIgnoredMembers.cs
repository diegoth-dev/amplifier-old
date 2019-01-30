using Amplifier.AspNetCore.ValueObjects;

namespace Amplifier.Tests.AspNetCore.ValueObjects
{
    class ValueObjectWithIgnoredMembers : ValueObject
    {
        [IgnoreMember]
        public int Ignored { get; set; }
        [IgnoreMember]
        public int IgnoredField;
        public int Considered { get; set; }
    }
}
