using BasicGameFrameworkLibrary.BasicDrawables.Dictionary;
using BasicGameFrameworkLibrary.CombinationHelpers;
using BasicGameFrameworkLibrary.RegularDeckOfCards;
using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
using CommonBasicStandardLibraries.CollectionClasses;
using CommonBasicStandardLibraries.Exceptions;
using System.Linq;
namespace BladesOfSteelCP.Logic
{
    public static class Extensions
    {
        public static CustomBasicList<CustomBasicList<RegularSimpleCard>> PossibleCombinations(this IDeckDict<RegularSimpleCard> thisList, EnumRegularColorList whatColor)
        {
            return thisList.PossibleCombinations(whatColor, 3);
        }
        public static CustomBasicList<CustomBasicList<RegularSimpleCard>> PossibleCombinations(this IDeckDict<RegularSimpleCard> thisList, EnumRegularColorList whatColor, int maxs)
        {
            if (maxs < 3 && whatColor == EnumRegularColorList.Red)
            {
                throw new BasicBlankException("Attack must allow 3 cards for combinations");
            }
            int mins;
            if (whatColor == EnumRegularColorList.Red)
            {
                mins = 2;
            }
            else
            {
                mins = 1;
            }
            int x;
            CustomBasicList<int> firstList = new CustomBasicList<int>();
            var loopTo = maxs;
            for (x = mins; x <= loopTo; x++)
            {
                firstList.Add(x);
            }
            var newList = thisList.Where(items => items.Color == whatColor).ToCustomBasicList();
            CustomBasicList<CustomBasicList<RegularSimpleCard>> fullList = new CustomBasicList<CustomBasicList<RegularSimpleCard>>();
            firstList.ForEach(y =>
            {
                var thisCombo = newList.GetCombinations(y);
                fullList.AddRange(thisCombo);
            });
            return fullList;
        }
    }
}