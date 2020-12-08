using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.Dominos;
using BasicGameFrameworkLibrary.DrawableListsObservable;
using System;
using System.Threading.Tasks;
namespace BasicGameFrameworkLibrary.MultiplayerClasses.InterfaceModels
{
    public interface IDominoGamesData<D> : IViewModelData, IBasicEnableProcess, IEnableAlways
        where D : IDominoInfo, new()
    {
        Func<D, Task>? DrewDominoAsync { get; set; }
        HandObservable<D> PlayerHand1 { get; set; }
        DominosBoneYardClass<D> BoneYard { get; set; }
        Func<Task>? PlayerBoardClickedAsync { get; set; }
        Func<D, int, Task>? HandClickedAsync { get; set; }
    }
}