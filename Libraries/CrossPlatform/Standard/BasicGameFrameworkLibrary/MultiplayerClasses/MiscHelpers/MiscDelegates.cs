using BasicGameFrameworkLibrary.MultiplayerClasses.BasicGameClasses;
using BasicGameFrameworkLibrary.MultiplayerClasses.BasicPlayerClasses;
using BasicGameFrameworkLibrary.MultiplayerClasses.SavedGameClasses;
using BasicGameFrameworkLibrary.ViewModels;
using CommonBasicStandardLibraries.CollectionClasses;
using System;
using System.Threading.Tasks;
namespace BasicGameFrameworkLibrary.MultiplayerClasses.MiscHelpers
{
    public static class MiscDelegates
    {
        public static Func<Task>? ColorsFinishedAsync { get; set; }
        public static Func<CustomBasicList<Type>>? GetMiscObjectsToReplace { get; set; }
        public static Func<Task>? ComputerChooseColorsAsync { get; set; }
        public static Func<Task>? ContinueColorsAsync { get; set; }
        public static Action? FillRestColors { get; set; }
        public static Action? ManuelSetColors { get; set; }

        

    }
}