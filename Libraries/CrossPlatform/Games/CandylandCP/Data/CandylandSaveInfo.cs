using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.BasicDrawables.Dictionary;
using BasicGameFrameworkLibrary.MultiplayerClasses.SavedGameClasses;
namespace CandylandCP.Data
{
    [SingletonGame]
    public class CandylandSaveInfo : BasicSavedGameClass<CandylandPlayerItem>, ISavedCardList<CandylandCardData>
    {
        public bool DidDraw { get; set; }
        public DeckRegularDict<CandylandCardData>? CardList { get; set; }
        public CandylandCardData? CurrentCard { get; set; }
    }
}