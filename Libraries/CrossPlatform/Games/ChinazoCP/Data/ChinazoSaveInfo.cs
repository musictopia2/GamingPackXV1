using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.MultiplayerClasses.SavedGameClasses;
using CommonBasicStandardLibraries.CollectionClasses;
namespace ChinazoCP.Data
{
    [SingletonGame]
    public class ChinazoSaveInfo : BasicSavedCardClass<ChinazoPlayerItem, ChinazoCard>
    {
        public CustomBasicList<SavedSet> SetList { get; set; } = new CustomBasicList<SavedSet>();
        public int Round { get; set; }
        public bool HadChinazo { get; set; }
    }
}