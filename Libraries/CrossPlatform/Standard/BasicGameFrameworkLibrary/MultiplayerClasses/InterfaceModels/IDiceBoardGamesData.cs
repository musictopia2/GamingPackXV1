using BasicGameFrameworkLibrary.Dice;
using BasicGameFrameworkLibrary.MultiplayerClasses.InterfacesForHelpers;
using BasicGameFrameworkLibrary.MultiplayerClasses.SavedGameClasses;
namespace BasicGameFrameworkLibrary.MultiplayerClasses.InterfaceModels
{
    public interface IDiceBoardGamesData : ISimpleBoardGamesData, ICup<SimpleDice>
    {
        void LoadCup(ISavedDiceList<SimpleDice> saveRoot, bool autoResume);
    }
}