using BasicGameFrameworkLibrary.BasicDrawables.Interfaces;
using BasicGameFrameworkLibrary.SpecializedGameTypes.TrickClasses;
using System;
namespace BasicGameFrameworkLibrary.MultiplayerClasses.InterfaceModels
{
    public interface ITrickCardGamesData<D, TS> : IBasicCardGamesData<D>
        where TS : struct, Enum
        where D : class, ITrickCard<TS>, new()
    {
        TS TrumpSuit { get; set; }
        BasicTrickAreaObservable<TS, D> TrickArea1 { get; set; }
    }
}