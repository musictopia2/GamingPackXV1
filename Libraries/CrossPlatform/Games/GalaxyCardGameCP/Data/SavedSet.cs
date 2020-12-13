using BasicGameFrameworkLibrary.BasicDrawables.Dictionary;
using GalaxyCardGameCP.Cards;
namespace GalaxyCardGameCP.Data
{
    public class SavedSet
    {
        public DeckRegularDict<GalaxyCardGameCardInformation> CardList { get; set; } = new DeckRegularDict<GalaxyCardGameCardInformation>();
        public EnumWhatSets WhatSet;
    }
}