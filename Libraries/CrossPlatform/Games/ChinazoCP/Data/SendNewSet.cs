using BasicGameFrameworkLibrary.SpecializedGameTypes.RummyClasses;
namespace ChinazoCP.Data
{
    public class SendNewSet
    {
        public string CardListData { get; set; } = "";
        public bool UseSecond { get; set; }
        public EnumRummyType WhatSet { get; set; }
    }
}