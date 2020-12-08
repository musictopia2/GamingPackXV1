using System;
namespace BasicGameFrameworkLibrary.BasicEventModels
{
    public class RepaintEventModel
    {
        public static Action? UpdatePartOfBoard { get; set; }
        public static bool NeedsExtraSpeed { get; set; } //hopefully this helps as well (?)
    }
}