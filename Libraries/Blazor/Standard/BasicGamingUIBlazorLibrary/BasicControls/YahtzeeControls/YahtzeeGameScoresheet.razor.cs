using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.Dice;
using BasicGameFrameworkLibrary.SpecializedGameTypes.YahtzeeStyleHelpers.Containers;
using BasicGameFrameworkLibrary.SpecializedGameTypes.YahtzeeStyleHelpers.Data;
using BasicGameFrameworkLibrary.SpecializedGameTypes.YahtzeeStyleHelpers.ViewModels;
using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
using CommonBasicStandardLibraries.CollectionClasses;
using Microsoft.AspNetCore.Components;
using System.Linq;
using System.Threading.Tasks;
namespace BasicGamingUIBlazorLibrary.BasicControls.YahtzeeControls
{
    public partial class YahtzeeGameScoresheet<D>
        where D : SimpleDice, new()
    {
        [Parameter]
        public ScoreContainer? ScoreContainer { get; set; }
        [CascadingParameter]
        public YahtzeeScoresheetViewModel<D>? DataContext { get; set; }
        [Parameter]
        public CommandContainer? CommandContainer { get; set; }
        [Parameter]
        public int BottomDescriptionWidth { get; set; }
        private async Task ProcessRowClickedAsync(RowInfo row)
        {
            if (DataContext!.CanRow(row) == false)
            {
                return;
            }
            await DataContext.RowAsync(row); //i think.
        }
        private RowInfo GetBonus()
        {
            RowInfo output;
            output = ScoreContainer!.RowList.Where(x => x.IsTop && x.RowSection == EnumRow.Bonus).Single();
            return output;
        }
        private RowInfo GetTopScore()
        {
            RowInfo output;
            output = ScoreContainer!.RowList.Where(x => x.IsTop && x.RowSection == EnumRow.Totals).Single();
            return output;
        }
        private CustomBasicList<RowInfo> GetTopList()
        {
            CustomBasicList<RowInfo> output;
            output = ScoreContainer!.RowList.Where(x => x.IsTop && x.RowSection == EnumRow.Regular).ToCustomBasicList();
            return output;
        }
        private RowInfo GetBottomScore()
        {
            RowInfo output;
            output = ScoreContainer!.RowList.Where(x => x.IsTop == false && x.RowSection == EnumRow.Totals).Single();
            return output;
        }
        private CustomBasicList<RowInfo> GetBottomList()
        {
            CustomBasicList<RowInfo> output;
            output = ScoreContainer!.RowList.Where(x => x.IsTop == false && x.RowSection == EnumRow.Regular).ToCustomBasicList();
            return output;
        }
    }
}