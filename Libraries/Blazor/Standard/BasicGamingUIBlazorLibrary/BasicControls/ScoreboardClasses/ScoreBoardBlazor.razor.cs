using BasicGameFrameworkLibrary.MultiplayerClasses.BasicPlayerClasses;
using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
using CommonBasicStandardLibraries.CollectionClasses;
using CommonBasicStandardLibraries.Exceptions;
using Microsoft.AspNetCore.Components;
using System.Reflection;
namespace BasicGamingUIBlazorLibrary.BasicControls.ScoreboardClasses
{
    public partial class ScoreBoardBlazor<P>
        where P : class, IPlayerItem, new()
    {
        [Parameter]
        public PlayerCollection<P>? Players { get; set; }
        [Parameter]
        public CustomBasicList<ScoreColumnModel> Columns { get; set; } = new CustomBasicList<ScoreColumnModel>();
        [Parameter]
        public string Height { get; set; } = "";

        [Parameter]
        public string Width { get; set; } = "";

        [Parameter]
        public bool UseAbbreviationForTrueFalse { get; set; }

        private bool IsVisible(P player, ScoreColumnModel column)
        {
            if (column.VisiblePath == "")
            {
                return true; //because its not even set.
            }
            PropertyInfo property = player.GetType().GetProperty(column.VisiblePath);
            object value = property.GetValue(player);
            bool output = bool.Parse(value.ToString());
            return output;
        }
        private string TextToDisplay(P player, ScoreColumnModel column)
        {

            if (IsVisible(player, column) == false)
            {
                return "";
            }

            PropertyInfo property = player.GetType().GetProperty(column.MainPath);
            object value = property.GetValue(player);
            string content = value.ToString();
            switch (column.SpecialCategory)
            {
                case EnumScoreSpecialCategory.None:
                    return content;
                case EnumScoreSpecialCategory.TrueFalse:
                    bool rets = bool.Parse(content);
                    if (rets == true)
                    {
                        if (UseAbbreviationForTrueFalse)
                        {
                            return "Y";
                        }
                        return "Yes";
                    }
                    if (UseAbbreviationForTrueFalse)
                    {
                        return "N";
                    }
                    return "No";
                case EnumScoreSpecialCategory.Currency:
                    decimal money = decimal.Parse(content);
                    return money.ToCurrency(0); //we want 0 places.

                default:
                    throw new BasicBlankException("Not supported for text to display");
            }


        }
    }
}