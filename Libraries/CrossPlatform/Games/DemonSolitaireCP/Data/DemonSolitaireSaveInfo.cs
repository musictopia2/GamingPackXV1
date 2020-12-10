using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.SolitaireClasses.MainClasses;
using CommonBasicStandardLibraries.CollectionClasses;
using CommonBasicStandardLibraries.MVVMFramework.ViewModels;
namespace DemonSolitaireCP.Data
{
    [SingletonGame]
    public class DemonSolitaireSaveInfo : SolitaireSavedClass, IMappable
    {
        public CustomBasicList<int> HeelList { get; set; } = new CustomBasicList<int>();
    }
}