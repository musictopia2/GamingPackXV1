using System;
namespace BasicGameFrameworkLibrary.Attributes
{
    /// <summary>
    /// this is used for cases where you want to be able to do a list of properties and being able to hook the events for property notify change.
    /// still useful for blazor so blazor can use the view model and not the container and they are in sync.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    public class VMAttribute : Attribute { }
    
}