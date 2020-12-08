using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.Dice;
using BasicGameFrameworkLibrary.MultiplayerClasses.InterfacesForHelpers;
using BasicGameFrameworkLibrary.MultiplayerClasses.SavedGameClasses;
namespace BasicGameFrameworkLibrary.MultiplayerClasses.InterfaceModels
{
    public interface IBasicDiceGamesData<D> : IViewModelData, ICup<D>
        where D : IStandardDice, new()
    {
        int RollNumber { get; set; }
        void LoadCup(ISavedDiceList<D> saveRoot, bool autoResume);
    }
}