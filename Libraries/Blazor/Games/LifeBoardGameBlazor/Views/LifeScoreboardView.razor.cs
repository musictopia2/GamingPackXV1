using BasicGamingUIBlazorLibrary.BasicControls.ScoreboardClasses;
using CommonBasicStandardLibraries.CollectionClasses;
using LifeBoardGameCP.Data;
using Microsoft.AspNetCore.Components;
namespace LifeBoardGameBlazor.Views
{
    public partial class LifeScoreboardView
    {
        private readonly CustomBasicList<ScoreColumnModel> _scores = new CustomBasicList<ScoreColumnModel>();

        [CascadingParameter]
        public LifeBoardGameGameContainer? GameContainer { get; set; } 
        protected override void OnInitialized()
        {
            _scores.Clear();
            _scores.AddColumn("Money", true, nameof(LifeBoardGamePlayerItem.MoneyEarned), category: EnumScoreSpecialCategory.Currency)
                .AddColumn("Loans", true, nameof(LifeBoardGamePlayerItem.Loans), category: EnumScoreSpecialCategory.Currency)
                .AddColumn("Stock 1", false, nameof(LifeBoardGamePlayerItem.FirstStock))
                .AddColumn("Stock 2", false, nameof(LifeBoardGamePlayerItem.SecondStock))
                .AddColumn("Career", true, nameof(LifeBoardGamePlayerItem.Career1))
                .AddColumn("Salary", true, nameof(LifeBoardGamePlayerItem.Salary), category: EnumScoreSpecialCategory.Currency)
                .AddColumn("Tiles", false, nameof(LifeBoardGamePlayerItem.TilesCollected))
                .AddColumn("Car I.", false, nameof(LifeBoardGamePlayerItem.CarIsInsured), category: EnumScoreSpecialCategory.TrueFalse)
                .AddColumn("House I.", false, nameof(LifeBoardGamePlayerItem.HouseIsInsured), category: EnumScoreSpecialCategory.TrueFalse)
                .AddColumn("S Career", true, nameof(LifeBoardGamePlayerItem.Career2));
            base.OnInitialized();
        }
    }
}