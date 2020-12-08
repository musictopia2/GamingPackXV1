using System;
namespace BasicGameFrameworkLibrary.Dice
{
    public interface ICompleteSingleDice<T> : IRollSingleDice<T>, IBasicDice<T>, IGenerateDice<T> where T : IConvertible { }
}