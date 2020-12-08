using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
using CommonBasicStandardLibraries.CollectionClasses;
using System;
namespace BasicGameFrameworkLibrary.Dice
{
    public static class SharedDiceRoutines
    {
        public static D GetDiceInfo<D>(int chosen, int upTo) where D : IBasicDice<int>, new()
        {
            D output = new D();
            output.Index = upTo;
            output.Populate(chosen); //not always that cut and dry.
            return output;
        }
        public static CustomBasicList<CustomBasicList<T>> GetMultipleRolledDice<T>(int howManySections, IGenerateDice<T> dice) where T : IConvertible
        {
            CustomBasicList<CustomBasicList<T>> output = new CustomBasicList<CustomBasicList<T>>();
            CustomBasicList<T> thisList = new CustomBasicList<T>();
            CustomBasicList<T> otherList = dice.GetPossibleList;
            howManySections.Times(items =>
            {
                thisList.Add(otherList.GetRandomItem());
            });
            output.Add(thisList);
            return output;
        }
        public static CustomBasicList<T> GetSingleRolledDice<T>(int howManySections, IGenerateDice<T> dice) where T : IConvertible
        {
            CustomBasicList<T> thisList = new CustomBasicList<T>();
            CustomBasicList<T> otherList = dice.GetPossibleList;
            howManySections.Times(items =>
            {
                thisList.Add(otherList.GetRandomItem());
            });
            return thisList;
        }
    }
}