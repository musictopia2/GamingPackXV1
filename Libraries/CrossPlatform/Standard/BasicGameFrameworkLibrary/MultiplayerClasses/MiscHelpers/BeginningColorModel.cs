using BasicGameFrameworkLibrary.ChooserClasses;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGameFrameworkLibrary.MultiplayerClasses.BasicPlayerClasses;
using System;
namespace BasicGameFrameworkLibrary.MultiplayerClasses.MiscHelpers
{
    public class BeginningColorModel<E, P> : IBeginningColorModel<E>
        where E : struct, Enum
        where P : class, IPlayerBoardGame<E>, new()
    {
        public BoardGamesColorPicker<E, P> ColorChooser { get; set; }
        SimpleEnumPickerVM<E> IBeginningColorModel<E>.ColorChooser => ColorChooser;
        public BeginningColorModel(CommandContainer command)
        {
            ColorChooser = new BoardGamesColorPicker<E, P>(command, new ColorListChooser<E>());
        }
    }
}