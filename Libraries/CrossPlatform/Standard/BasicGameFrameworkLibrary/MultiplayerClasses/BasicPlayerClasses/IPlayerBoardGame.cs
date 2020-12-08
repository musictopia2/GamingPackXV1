using System;
namespace BasicGameFrameworkLibrary.MultiplayerClasses.BasicPlayerClasses
{
    public interface IPlayerBoardGame<E> : IPlayerColors
        where E : Enum
    {
        E Color { get; set; }
    }
}