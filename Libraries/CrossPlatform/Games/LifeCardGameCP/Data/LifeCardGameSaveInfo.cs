using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.MultiplayerClasses.SavedGameClasses;
using CommonBasicStandardLibraries.CollectionClasses;
using LifeCardGameCP.Cards;
namespace LifeCardGameCP.Data
{
    [SingletonGame]
    public class LifeCardGameSaveInfo : BasicSavedCardClass<LifeCardGamePlayerItem, LifeCardGameCardInformation>
    {
        public CustomBasicList<int> YearList { get; set; } = new CustomBasicList<int>();
        public int YearsPassed() => YearList.Count * 10;
    }
}