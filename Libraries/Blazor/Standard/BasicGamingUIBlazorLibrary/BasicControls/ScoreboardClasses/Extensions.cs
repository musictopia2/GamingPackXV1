using BasicGameFrameworkLibrary.MultiplayerClasses.BasicPlayerClasses;
using CommonBasicStandardLibraries.CollectionClasses;
namespace BasicGamingUIBlazorLibrary.BasicControls.ScoreboardClasses
{
    public static class ScoreExtensions
    {
        public static CustomBasicList<ScoreColumnModel> AddColumn(this CustomBasicList<ScoreColumnModel> scores, string header, bool isHorizontal, string normalPath, string visiblePath = "", EnumScoreSpecialCategory category = EnumScoreSpecialCategory.None)
        {
            if (scores.Count == 0 && header != "Nick Name")
                scores = scores.AddColumn("Nick Name", true, nameof(IPlayerItem.NickName));
            ScoreColumnModel info = new ScoreColumnModel()
            {
                Header = header,
                MainPath = normalPath,
                IsHorizontal = isHorizontal,
                SpecialCategory = category,
                VisiblePath = visiblePath,
            };
            scores.Add(info);
            return scores;
        }
    }
}