using BasicGameFrameworkLibrary.BasicDrawables.Dictionary;
using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
using CommonBasicStandardLibraries.CollectionClasses;
namespace BasicGameFrameworkLibrary.RegularDeckOfCards
{
    public static class Helpers
    {
        public static CustomBasicList<ExcludeRCard> AppendExclude(this CustomBasicList<ExcludeRCard> thisList,
            EnumSuitList suit, int number)
        {
            thisList.Add(new ExcludeRCard(suit, number));
            return thisList;
        }
        public static CustomBasicList<EnumRegularCardValueList> LoadValuesFromCards<R>(this IDeckDict<R> thisDict) where R : IRegularCard
        {
            return thisDict.DistinctItems(Items => Items.Value); //this simple now.
        }
        public static CustomBasicList<EnumSuitList> GetCompleteSuitList => new CustomBasicList<EnumSuitList> { EnumSuitList.Clubs, EnumSuitList.Diamonds, EnumSuitList.Hearts, EnumSuitList.Spades };
    }
}