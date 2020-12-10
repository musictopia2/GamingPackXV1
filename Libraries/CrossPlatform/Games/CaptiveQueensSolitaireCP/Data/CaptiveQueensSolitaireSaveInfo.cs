using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.SolitaireClasses.MainClasses;
using CommonBasicStandardLibraries.MVVMFramework.ViewModels;
namespace CaptiveQueensSolitaireCP.Data
{
    [SingletonGame]
    public class CaptiveQueensSolitaireSaveInfo : SolitaireSavedClass, IMappable
    {
        //anything else needed to save a game will be here.

    }
}