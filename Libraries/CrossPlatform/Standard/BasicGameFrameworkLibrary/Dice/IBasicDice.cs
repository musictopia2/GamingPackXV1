using BasicGameFrameworkLibrary.CommonInterfaces;
using System;
namespace BasicGameFrameworkLibrary.Dice
{
    public interface IBasicDice<T> : IVisibleObject, IPopulateObject<T> where T : IConvertible //this is everything a dice must do in order to be a dice.
    {
        int HeightWidth { get; }
        T Value { get; set; } //decided that value now will be generics.
        int Index { get; set; }
    }
}