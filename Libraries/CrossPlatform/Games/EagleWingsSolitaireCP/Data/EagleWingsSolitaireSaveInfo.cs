using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.SolitaireClasses.MainClasses;
using CommonBasicStandardLibraries.CollectionClasses;
using CommonBasicStandardLibraries.MVVMFramework.ViewModels;
namespace EagleWingsSolitaireCP.Data
{
    [SingletonGame]
    public class EagleWingsSolitaireSaveInfo : SolitaireSavedClass, IMappable
    {
        public CustomBasicList<int> HeelList { get; set; } = new CustomBasicList<int>();
    }
}