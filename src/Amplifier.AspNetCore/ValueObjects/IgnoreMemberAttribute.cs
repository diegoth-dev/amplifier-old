using System;

namespace Amplifier.AspNetCore.ValueObjects
{
    /// <summary>
    /// Annotation used to prevent a public property or field from being considered in memberwise comparisons.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class IgnoreMemberAttribute : Attribute
    {
    }
}
