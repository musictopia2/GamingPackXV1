using BasicGameFrameworkLibrary.BasicDrawables.Dictionary;
namespace MonasteryCardGameCP.Data
{
    public class SavedSet
    {
        public EnumMonasterySets WhatType { get; set; }
        public DeckRegularDict<MonasteryCardInfo> CardList { get; set; } = new DeckRegularDict<MonasteryCardInfo>();
    }
}