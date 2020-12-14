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

        public static CustomBasicList<Type> ReplaceBoardGameColorClasses<E, P, S>()
            where E : struct, Enum
            where P : class, IPlayerBoardGame<E>, new()
            where S : BasicSavedGameClass<P>, new()
        {
            CustomBasicList<Type> output = new CustomBasicList<Type>()
            {
                typeof(BeginningColorProcessorClass<E, P, S>),
                typeof(BeginningColorModel<E, P>),
                typeof(BeginningChooseColorViewModel<E, P>)
            };
            return output;
        }

    }
}