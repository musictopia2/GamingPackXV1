using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.MultiplayerClasses.SavedGameClasses;
using BasicGameFrameworkLibrary.RegularDeckOfCards;
namespace OldMaidCP.Data
{
    [SingletonGame]
    public class OldMaidSaveInfo : BasicSavedCardClass<OldMaidPlayerItem, RegularSimpleCard>
    {
        public bool RemovePairs { get; set; }
        public bool AlreadyChoseOne { get; set; }
    }
}