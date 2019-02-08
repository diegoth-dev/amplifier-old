using Amplifier.AspNetCore.ValueObjects;
using Amplifier.Tests.AspNetCore.ValueObjects;
using System;
using Xunit;

namespace Amplifier.Tests
{
    public class ValueObjectTests
    {
        [Fact]
        public void Equals_NullIsConsideredEqual()
        {
            var value1 = new ValueObjectTestClass();
            var value2 = new ValueObjectTestClass();

            AssertEqual(value1, value2);
        }

        [Fact]
        public void Equals_OnlyOneValueIsNull_DoesNotThrow_NotEqual()
        {
            var value1 = new ValueObjectTestClass();
            var value2 = new ValueObjectTestClass { Property1 = "value" };

            AssertNotEqual(value1, value2);
        }

        [Fact]
        public void Equals_ComparesAllPropertiesAndFields_Equal()
        {
            var value1 = new ValueObjectTestClass { Property1 = "test", Property2 = 10, Field = 3 };
            var value2 = new ValueObjectTestClass { Property1 = "test", Property2 = 10, Field = 3 };

            AssertEqual(value1, value2);
        }

        [Fact]
        public void Equals_ComparesAllPropertiesAndFields_PropertyDifferent_NotEqual()
        {
            var value1 = new ValueObjectTestClass { Property1 = "test", Property2 = 10 };
            var value2 = new ValueObjectTestClass { Property1 = "Test", Property2 = 10 };

            AssertNotEqual(value1, value2);
        }

        [Fact]
        public void Equals_ComparesAllPropertiesAndFields_FieldDifferent_NotEqual()
        {
            var value1 = new ValueObjectTestClass { Property1 = "test", Property2 = 10, Field = 8 };
            var value2 = new ValueObjectTestClass { Property1 = "test", Property2 = 10, Field = 9 };

            AssertNotEqual(value1, value2);
        }

        [Fact]
        public void Equals_IgnoresPrivatePropertiesAndFields()
        {
            var value1 = new ValueObjectTestClass(5);
            var value2 = new ValueObjectTestClass(8);

            AssertEqual(value1, value2);
        }

        [Fact]
        public void Equals_ComparingWithNull_ReturnsFalse()
        {
            var value = new ValueObjectTestClass { Property1 = "string" };

            Assert.False(value.Equals(null as object));
        }

        [Fact]
        public void Equals_ComparingWithWrongType_ReturnsFalse()
        {
            var value = new ValueObjectTestClass { Property1 = "string" };

            Assert.False(value.Equals(10));
        }

        [Fact]
        public void OperatorEquals_LeftSideNull_ReturnsFalse()
        {
            var value = new ValueObjectTestClass();

            Assert.False(value == null);
        }

        [Fact]
        public void OperatorEquals_RightSideNull_ReturnsFalse()
        {
            var value = new ValueObjectTestClass();

            Assert.False(value == null);
        }

        [Fact]
        public void OperatorEquals_BothValuesNull_ReturnsTrue()
        {
            Assert.True((ValueObjectTestClass)null == (ValueObjectTestClass)null);
        }

        [Fact]
        public void OperatorNotEquals_LeftSideNull_ReturnsTrue()
        {
            var value = new ValueObjectTestClass();

            Assert.True(value != null);
        }

        [Fact]
        public void OperatorNotEquals_RightSideNull_ReturnsTrue()
        {
            var value = new ValueObjectTestClass();

            Assert.True(value != null);
        }

        [Fact]
        public void OperatorNotEquals_BothValuesNull_ReturnsFalse()
        {
            Assert.False((ValueObjectTestClass)null != (ValueObjectTestClass)null);
        }

        [Fact]
        public void GetHashCode_AlwaysEqualForEqualObjects()
        {
            var value1 = new ValueObjectTestClass { Property1 = "string", Property2 = 4 };
            var value2 = new ValueObjectTestClass { Property1 = "string", Property2 = 4 };

            Assert.Equal(value1.GetHashCode(), value2.GetHashCode());
        }

        [Fact]
        public void GetHashCode_NotEqualForDistinctObjects_1()
        {
            var value1 = new ValueObjectTestClass { Property1 = "string", Property2 = 4 };
            var value2 = new ValueObjectTestClass { Property1 = "string", Property2 = 5 };

            Assert.NotEqual(value1.GetHashCode(), value2.GetHashCode());
        }

        [Fact]
        public void GetHashCode_NotEqualForDistinctObjects_2()
        {
            var value1 = new ValueObjectTestClass { Property1 = "string", Property2 = 4 };
            var value2 = new ValueObjectTestClass { Property1 = "String", Property2 = 4 };

            Assert.NotEqual(value1.GetHashCode(), value2.GetHashCode());
        }

        [Fact]
        public void GetHashCode_HandlesNull()
        {
            var value1 = new ValueObjectTestClass { Property2 = 2 };
            var value2 = new ValueObjectTestClass { Property2 = 5 };

            Assert.NotEqual(value1.GetHashCode(), value2.GetHashCode());
        }

        [Fact]
        public void GetHashCode_ConsidersPublicFields()
        {
            var value1 = new ValueObjectTestClass { Property2 = 2 };
            var value2 = new ValueObjectTestClass { Property2 = 2, Field = 4 };

            Assert.NotEqual(value1.GetHashCode(), value2.GetHashCode());
        }

        [Fact]
        public void Nesting()
        {
            var value = new ValueObjectWithRecursiveValue();
            var value2 = new ValueObjectWithRecursiveValue();
            var nestedValue = new ValueObjectWithRecursiveValue() { Terminal = "test" };
            var nestedValue2 = new ValueObjectWithRecursiveValue() { Terminal = "test" };

            value.Recurse = nestedValue;
            value2.Recurse = nestedValue2;

            Assert.True(value.Equals(value2));
            Assert.Equal(value.GetHashCode(), value2.GetHashCode());
        }

        [Fact]
        public void IgnoreMember_Property_DoesNotConsider()
        {
            var value1 = new ValueObjectWithIgnoredMembers { Ignored = 2, Considered = 4 };
            var value2 = new ValueObjectWithIgnoredMembers { Ignored = 3, Considered = 4 };

            Assert.True(value1.Equals(value2));
        }

        [Fact]
        public void IgnoreMember_Field_DoesNotConsider()
        {
            var value1 = new ValueObjectWithIgnoredMembers { IgnoredField = 3, Considered = 4 };
            var value2 = new ValueObjectWithIgnoredMembers { IgnoredField = 2, Considered = 4 };

            Assert.True(value1.Equals(value2));
        }

        class MyValue : ValueObject
        {                        
        }

        class MyValue2 : MyValue
        {
        }

        [Fact]
        public void ObjectsOfDifferentTypeAreNotEqual_EvenIfSubclass()
        {
            var value1 = new MyValue();
            var value2 = new MyValue2();

            Assert.False(value1.Equals(value2));
        }

        private void AssertEqual(ValueObjectTestClass value1, ValueObjectTestClass value2)
        {
            Assert.Equal(value1, value2);
            Assert.True(value1 == value2);
            Assert.False(value1 != value2);
            Assert.True(value1.Equals(value2));
        }

        private void AssertNotEqual(ValueObjectTestClass value1, ValueObjectTestClass value2)
        {
            Assert.NotEqual(value1, value2);
            Assert.True(value1 != value2);
            Assert.False(value1 == value2);
            Assert.False(value1.Equals(value2));
        }
    }
}
