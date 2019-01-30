using Amplifier.AspNetCore.ValueObjects;

namespace Amplifier.Tests.AspNetCore.ValueObjects
{
    class ValueObjectWithRecursiveValue : ValueObject
    {
        public ValueObjectWithRecursiveValue Recurse { get; set; }
        public string Terminal;
    }
}
