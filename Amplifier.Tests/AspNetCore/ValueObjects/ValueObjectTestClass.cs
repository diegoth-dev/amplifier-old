using Amplifier.AspNetCore.ValueObjects;

namespace Amplifier.Tests.AspNetCore.ValueObjects
{
    class ValueObjectTestClass : ValueObject
    {
        private readonly int privateField;
        protected int protectedField;

        private int PrivateProperty { get; set; }
        protected int ProtectedProperty { get; set; }

        public string Property1 { get; set; }
        public int Property2 { get; set; }
        public int Field;

        public ValueObjectTestClass() { }

        public ValueObjectTestClass(int nonPublicValue)
        {
            ProtectedProperty = nonPublicValue;
            PrivateProperty = nonPublicValue;
            privateField = nonPublicValue;
            protectedField = nonPublicValue;
        }        
    }
}
