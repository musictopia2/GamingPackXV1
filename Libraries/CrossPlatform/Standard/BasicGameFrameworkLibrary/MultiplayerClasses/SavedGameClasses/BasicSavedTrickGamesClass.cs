using BasicGameFrameworkLibrary.BasicDrawables.Dictionary;
using BasicGameFrameworkLibrary.BasicDrawables.Interfaces;
using BasicGameFrameworkLibrary.MultiplayerClasses.BasicPlayerClasses;
using BasicGameFrameworkLibrary.MultiplayerClasses.InterfaceModels;
using Newtonsoft.Json;
using System;
namespace BasicGameFrameworkLibrary.MultiplayerClasses.SavedGameClasses
{
    public class BasicSavedTrickGamesClass<S, T, P> : BasicSavedCardClass<P, T>
        where S : struct, Enum
        where T : class, ITrickCard<S>, new()
        where P : class, IPlayerTrick<S, T>, new()
    {
        public DeckRegularDict<T> TrickList = new DeckRegularDict<T>();
        private S _trumpSuit;
        public S TrumpSuit
        {
            get { return _trumpSuit; }
            set
            {
                if (SetProperty(ref _trumpSuit, value))
                {
                    if (TrickMod != null)
                        TrickMod.TrumpSuit = value;
                }
            }
        }
        [JsonIgnore]
        public ITrickCardGamesData<T, S>? TrickMod;
        public void LoadTrickVM(ITrickCardGamesData<T, S> TrickMod)
        {
            this.TrickMod = TrickMod;
            TrickMod.TrumpSuit = TrumpSuit;
        }
    }
}