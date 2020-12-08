using System;
namespace BasicGameFrameworkLibrary.Attributes
{
    /// <summary>
    /// this is used in cases where after it replaces the game, it can autoreset other objects as well.  usually works without the same assembly but can later think about other exceptions.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class AutoResetAttribute : Attribute { }
}