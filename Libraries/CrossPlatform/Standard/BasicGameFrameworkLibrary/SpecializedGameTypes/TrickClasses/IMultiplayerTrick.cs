using BasicGameFrameworkLibrary.BasicDrawables.Interfaces;
using BasicGameFrameworkLibrary.MultiplayerClasses.BasicPlayerClasses;
using CommonBasicStandardLibraries.CollectionClasses;
using System;
namespace BasicGameFrameworkLibrary.SpecializedGameTypes.TrickClasses
{
    public interface IMultiplayerTrick<S, T, P>
        where S : Enum
        where T : ITrickCard<S>, new()
        where P : IPlayerSingleHand<T>, new()
    {
        CustomBasicList<TrickCoordinate>? ViewList { get; set; }
        P GetSpecificPlayer(int id);
    }
}