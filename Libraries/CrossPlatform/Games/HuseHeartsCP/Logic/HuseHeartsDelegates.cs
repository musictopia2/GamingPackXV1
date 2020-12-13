using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.BasicDrawables.Dictionary;
using HuseHeartsCP.Cards;
using System;
namespace HuseHeartsCP.Logic
{
    [SingletonGame]
    public class HuseHeartsDelegates
    {
        public Action<DeckRegularDict<HuseHeartsCardInformation>>? SetDummyList { get; set; }
        public Func<DeckRegularDict<HuseHeartsCardInformation>>? GetDummyList { get; set; }
    }
}