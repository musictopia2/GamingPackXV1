using BasicGameFrameworkLibrary.Attributes;
using CommonBasicStandardLibraries.MVVMFramework.ViewModels;

namespace MinesweeperCP.Data
{
    [SingletonGame]
    public class MinesweeperSaveInfo : ObservableObject, IMappable { }
}