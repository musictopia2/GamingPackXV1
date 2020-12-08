using BasicGameFrameworkLibrary.SpecializedGameTypes.YahtzeeStyleHelpers.Data;
using CommonBasicStandardLibraries.CollectionClasses;
namespace BasicGameFrameworkLibrary.SpecializedGameTypes.YahtzeeStyleHelpers.Logic
{
    public interface IScoreLogic
    {
        void LoadBoard();
        void PopulatePossibleScores();
        void ClearRecent();
        void StartTurn();
        void MarkScore(RowInfo currentRow);
        int TotalScore { get; }
        CustomBasicList<RowInfo> GetAvailableScores { get; }
    }
}