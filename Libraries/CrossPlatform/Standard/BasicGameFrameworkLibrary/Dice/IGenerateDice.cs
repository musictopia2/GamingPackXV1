using CommonBasicStandardLibraries.CollectionClasses;
using System;
namespace BasicGameFrameworkLibrary.Dice
{
    public interface IGenerateDice<T> where T : IConvertible
    {
        CustomBasicList<T> GetPossibleList { get; } //i like the idea of it being a property (read only)
    }
}