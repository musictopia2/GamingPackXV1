namespace BasicGamingUIBlazorLibrary.BasicControls.ScoreboardClasses
{
    public class ScoreColumnModel //decided to not risk doing records with scorecolumnmodels though since i already have extension to help anyways.
    {
        public string MainPath { get; set; } = "";
        public string Header { get; set; } = "";
        public string VisiblePath { get; set; } = "";
        public bool IsHorizontal { get; set; } = true; //defaults to true.
        public EnumScoreSpecialCategory SpecialCategory { get; set; } = EnumScoreSpecialCategory.None;
    }
}