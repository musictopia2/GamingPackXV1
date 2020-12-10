using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.SolitaireClasses.MainClasses;
using CommonBasicStandardLibraries.MVVMFramework.ViewModels;
namespace AgnesSolitaireCP.Data
{
    [SingletonGame]
    public class AgnesSolitaireSaveInfo : SolitaireSavedClass, IMappable
    {
        //anything else needed to save a game will be here.

    }
}