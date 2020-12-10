using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.SolitaireClasses.MainClasses;
using CommonBasicStandardLibraries.MVVMFramework.ViewModels;
namespace KlondikeSolitaireCP.Data
{
    [SingletonGame]
    public class KlondikeSolitaireSaveInfo : SolitaireSavedClass, IMappable
    {
        //anything else needed to save a game will be here.

    }
}