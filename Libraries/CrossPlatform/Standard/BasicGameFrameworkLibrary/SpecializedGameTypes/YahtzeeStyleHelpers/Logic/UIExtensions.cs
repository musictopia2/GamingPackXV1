using BasicGameFrameworkLibrary.SpecializedGameTypes.YahtzeeStyleHelpers.Data;
namespace BasicGameFrameworkLibrary.SpecializedGameTypes.YahtzeeStyleHelpers.Logic
{
    public static class UIExtensions
    {
        public static string RenderPointsObtained(this RowInfo row)
        {
            if (row.PointsObtained == null)
            {
                return "";
            }
            return row.PointsObtained.Value.ToString();
        }
        public static string RenderPointsPossible(this RowInfo row)
        {
            if (row.Possible == null)
            {
                return "";
            }
            return row.Possible.Value.ToString();
        }
    }
}