using System;
using System.Threading.Tasks;
namespace BasicGameFrameworkLibrary.SpecializedGameTypes.CheckersChessHelpers
{
    public static class CheckersChessDelegates
    {
        public static Func<bool>? CanMove { get; set; }
        public static Func<int, Task>? MakeMoveAsync { get; set; } //may still be needed (?)
    }
}