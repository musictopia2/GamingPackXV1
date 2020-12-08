using System;
namespace BasicGameFrameworkLibrary.Attributes
{
    [AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class CommandAttribute : Attribute
    {
        public CommandAttribute(EnumCommandCategory category)
        {
            Category = category;
        }
        public EnumCommandCategory Category { get; }
    }
}