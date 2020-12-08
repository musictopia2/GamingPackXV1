namespace BasicGameFrameworkLibrary.SolitaireClasses.DataClasses
{
    public interface ISolitaireData
    {
        EnumSolitaireMoveType MoveColumns { get; }
        int WastePiles { get; } //once i have autoimplemented interfaces, i can specify default amounts here too.
        int Rows { get; }
        int Columns { get; }
        bool IsKlondike { get; }
        int CardsNeededWasteBegin { get; }
        int CardsNeededMainBegin { get; }
        int Deals { get; }
        int CardsToDraw { get; }
        bool SuitsNeedToMatchForMainPile { get; }
        bool ShowNextNeededOnMain { get; }
        int WasteColumns { get; }
        int WasteRows { get; }
        bool MainRound { get; }
    }
}