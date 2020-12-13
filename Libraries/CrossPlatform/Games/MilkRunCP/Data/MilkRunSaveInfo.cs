using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.MultiplayerClasses.SavedGameClasses;
using MilkRunCP.Cards;
namespace MilkRunCP.Data
{
    [SingletonGame]
    public class MilkRunSaveInfo : BasicSavedCardClass<MilkRunPlayerItem, MilkRunCardInformation>
    { 
        public int CardsDrawn { get; set; }
        public bool DrawnFromDiscard { get; set; }
    }
}