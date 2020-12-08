using BasicGameFrameworkLibrary.BasicGameDataClasses;
namespace BasicGameFrameworkLibrary.MultiplayerClasses.InterfaceModels
{
    public interface ISimpleBoardGamesData : IViewModelData
    {
        string Instructions { get; set; }
    }
}