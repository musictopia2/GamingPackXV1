using System;
namespace BasicGameFrameworkLibrary.Attributes
{
    /// <summary>
    /// This attribute if set, will be registered as instance.
    /// However, it must be used on the assembly that its scanning.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class InstanceGameAttribute : Attribute { }
}