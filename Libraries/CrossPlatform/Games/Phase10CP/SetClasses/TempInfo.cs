using BasicGameFrameworkLibrary.BasicDrawables.Dictionary;
using Phase10CP.Cards;
using Phase10CP.Data;
namespace Phase10CP.SetClasses
{
    public struct TempInfo
    {
        public EnumPhase10Sets WhatSet;
        public int FirstNumber;
        public int SecondNumber;
        public DeckRegularDict<Phase10CardInformation> CardList;
    }
}