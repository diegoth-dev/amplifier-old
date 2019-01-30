using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Amplifier.AspNetCore.ValueObjects
{
    /// <summary>
    /// Value object base class.
    /// </summary>
    public class ValueObject : IEquatable<ValueObject>
    {
        private List<PropertyInfo> _properties;
        private List<FieldInfo> _fields;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj1"></param>
        /// <param name="obj2"></param>
        /// <returns></returns>
        public static bool operator ==(ValueObject obj1, ValueObject obj2)
        {
            return !Equals(obj1, null) ? obj1.Equals(obj2) : Equals(obj2, null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj1"></param>
        /// <param name="obj2"></param>
        /// <returns></returns>
        public static bool operator !=(ValueObject obj1, ValueObject obj2)
        {
            return !(obj1 == obj2);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool Equals(ValueObject obj)
        {
            return Equals(obj as object);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            var hash = GetProperties().Select(prop => prop.GetValue(this, null)).Aggregate(17, HashValue);

            return GetFields().Select(field => field.GetValue(this)).Aggregate(hash, HashValue);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType()) return false;

            return GetProperties().All(p => PropertiesAreEqual(obj, p))
                   && GetFields().All(f => FieldsAreEqual(obj, f));
        }

        private bool PropertiesAreEqual(object obj, PropertyInfo p)
        {
            return Equals(p.GetValue(this, null), p.GetValue(obj, null));
        }

        private bool FieldsAreEqual(object obj, FieldInfo f)
        {
            return Equals(f.GetValue(this), f.GetValue(obj));
        }

        private IEnumerable<PropertyInfo> GetProperties()
        {
            return this._properties ?? (this._properties = GetType()
                       .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                       .Where(p => !Attribute.IsDefined(p, typeof(IgnoreMemberAttribute))).ToList());
        }

        private IEnumerable<FieldInfo> GetFields()
        {
            return this._fields ?? (_fields = GetType().GetFields(BindingFlags.Instance | BindingFlags.Public)
                       .Where(f => !Attribute.IsDefined(f, typeof(IgnoreMemberAttribute))).ToList());
        }        

        private int HashValue(int seed, object value)
        {
            var currentHash = value?.GetHashCode() ?? 0;

            return (seed * 23) + currentHash;
        }
    }
}
