using System.Collections.Generic;

namespace Amplifier.AspNetCore.Entities
{
    /// <summary>
    /// Entity base class. Every entity on the application should inherit from this class.
    /// </summary>
    /// <typeparam name="TKey">Entity primary key type.</typeparam>
    public class Entity<TKey> : IEntity<TKey>
    {
        /// <summary>
        /// Unique entity identifier.
        /// </summary>
        public TKey Id { get; set; }

        /// <summary>
        /// Override Equals method to implement identifier equality.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns>true if the specified object identity is equal to the current object identity; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            var other = obj as Entity<TKey>;

            if (ReferenceEquals(other, null))
                return false;

            if (ReferenceEquals(this, other))
                return true;

            if (GetType() != other.GetType())
                return false;

            return EqualityComparer<TKey>.Default.Equals(Id, other.Id);            
        }

        /// <summary>
        /// Override == operator to implement identifier equality.
        /// </summary>
        /// <param name="a">Current object.</param>
        /// <param name="b">The object to compare with.</param>
        /// <returns>true if the specified object identity is equal to the current object identity; otherwise, false.</returns>
        public static bool operator ==(Entity<TKey> a, Entity<TKey> b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
                return true;

            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
                return false;

            return a.Equals(b);
        }

        /// <summary>
        /// Override != operator to implement identifier inequality.
        /// </summary>
        /// <param name="a">Current object.</param>
        /// <param name="b">The object to compare with.</param>
        /// <returns>true if the specified object identity is different to the current object identity; otherwise, false.</returns>
        public static bool operator !=(Entity<TKey> a, Entity<TKey> b)
        {
            return !(a == b);
        }

        /// <summary>
        /// Override GetHashCode method to implement identifier equality.
        /// </summary>
        /// <returns>Returns the hash code of this string plus id.</returns>
        public override int GetHashCode()
        {
            return (GetType().ToString() + Id).GetHashCode();
        }
    }
}
