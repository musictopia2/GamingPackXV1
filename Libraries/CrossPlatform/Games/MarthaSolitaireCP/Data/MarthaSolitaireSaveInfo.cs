using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.SolitaireClasses.MainClasses;
using CommonBasicStandardLibraries.MVVMFramework.ViewModels;
namespace MarthaSolitaireCP.Data
{
    [SingletonGame]
    public class MarthaSolitaireSaveInfo : SolitaireSavedClass, IMappable
    {
        //anything else needed to save a game will be here.

    }
}