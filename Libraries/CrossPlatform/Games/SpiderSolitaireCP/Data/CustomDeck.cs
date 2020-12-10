using BasicGameFrameworkLibrary.BasicDrawables.Interfaces;
using BasicGameFrameworkLibrary.RegularDeckOfCards;
using CommonBasicStandardLibraries.CollectionClasses;
using CommonBasicStandardLibraries.Exceptions;
namespace SpiderSolitaireCP.Data
{
    public class CustomDeck : IRegularDeckInfo
    {
        private readonly LevelClass _level;
        public CustomDeck(LevelClass level)
        {
            _level = level;
        }
        int IRegularDeckInfo.HowManyDecks
        {
            get
            {
                if (_level.LevelChosen == 3)
                {
                    return 2;
                }
                if (_level.LevelChosen == 2)
                {
                    return 4;
                }
                if (_level.LevelChosen == 1)
                {
                    return 8;
                }
                throw new BasicBlankException("Needs levels 1, 2, or 3 for figuring out how many decks");
            }
        }

        bool IRegularDeckInfo.UseJokers => false;

        int IRegularDeckInfo.GetExtraJokers => 0;

        int IRegularDeckInfo.LowestNumber => 1;

        int IRegularDeckInfo.HighestNumber => 13; //aces will usually be low.
        CustomBasicList<ExcludeRCard> IRegularDeckInfo.ExcludeList => new CustomBasicList<ExcludeRCard>();

        CustomBasicList<EnumSuitList> IRegularDeckInfo.SuitList
        {
            get
            {
                if (_level.LevelChosen == 1)
                {
                    return new CustomBasicList<EnumSuitList> { EnumSuitList.Spades };
                }
                if (_level.LevelChosen == 2)
                {
                    return new CustomBasicList<EnumSuitList> { EnumSuitList.Spades, EnumSuitList.Hearts };
                }
                if (_level.LevelChosen != 3)
                {
                    throw new BasicBlankException("Only 3 levels are supposed for the suit list");
                }
                var tempList = Helpers.GetCompleteSuitList;
                return tempList;
            }
        }
        int IDeckCount.GetDeckCount()
        {
            return 104;
        }
    }
}