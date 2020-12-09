using BasicBlazorLibrary.Components.Basic;
using BasicBlazorLibrary.Helpers;
using BasicGameFrameworkLibrary.BasicEventModels;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.MultiplayerClasses.BasicPlayerClasses;
using BasicGamingUIBlazorLibrary.BasicControls.ScoreboardClasses;
using BuncoDiceGameCP.Data;
using BuncoDiceGameCP.ViewModels;
using CommonBasicStandardLibraries.CollectionClasses;
using CommonBasicStandardLibraries.Messenging;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;
using static CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.BasicDataFunctions;
namespace BuncoDiceGameBlazor.Views
{
    public partial class BuncoDiceGameMainView
    {
        private CustomBasicList<ScoreColumnModel> ScoreList { get; set; } = new CustomBasicList<ScoreColumnModel>();
        private CustomBasicList<LabelGridModel> Details { get; set; } = new CustomBasicList<LabelGridModel>();
        private CommandContainer? Command { get; set; }
        protected override void OnInitialized()
        {
            DataContext!.CommandContainer.AddAction(ShowChange);
            ScoreList.Clear();
            ScoreList.AddColumn("Points", false, nameof(PlayerItem.Points))
                .AddColumn("Table", false, nameof(PlayerItem.Table))
                .AddColumn("Team", false, nameof(PlayerItem.Team))
                .AddColumn("Buncos", false, nameof(PlayerItem.Buncos))
                .AddColumn("Wins", false, nameof(PlayerItem.Wins))
                .AddColumn("Losses", false, nameof(PlayerItem.Losses));



            Details.AddLabel("# To Get", nameof(StatisticsInfo.NumberToGet))
                .AddLabel("Set", nameof(StatisticsInfo.Set))
                .AddLabel("Your Team", nameof(StatisticsInfo.YourTeam))
                .AddLabel("Your Points", nameof(StatisticsInfo.YourPoints))
                .AddLabel("Opponent Score", nameof(StatisticsInfo.OpponentScore))
                .AddLabel("Buncos", nameof(StatisticsInfo.Buncos))
                .AddLabel("Wins", nameof(StatisticsInfo.Wins))
                .AddLabel("Losses", nameof(StatisticsInfo.Losses))
                .AddLabel("Your Table", nameof(StatisticsInfo.YourTable))
                .AddLabel("Team Mate", nameof(StatisticsInfo.TeamMate))
                .AddLabel("Opponent 1", nameof(StatisticsInfo.Opponent1))
                .AddLabel("Opponent 2", nameof(StatisticsInfo.Opponent2))
                .AddLabel("Status", nameof(StatisticsInfo.Status));


            Command = DataContext.CommandContainer;
            //if you have to add command change, do so as well.
            base.OnInitialized();
        }


        #region Command Names
        private string BuncoMethod => nameof(BuncoDiceGameMainViewModel.BuncoAsync);
        private string Has21Method => nameof(BuncoDiceGameMainViewModel.Human21Async);
        private string RollMethod => nameof(BuncoDiceGameMainViewModel.RollAsync);
        private string EndTurnMethod => nameof(BuncoDiceGameMainViewModel.EndTurnAsync);
        #endregion

        private PlayerCollection<PlayerItem> GetPlayers()
        {
            BuncoDiceGameSaveInfo saves = cons!.Resolve<BuncoDiceGameSaveInfo>();
            return saves.PlayerList;
        }
        private StatisticsInfo GetStats()
        {
            BuncoDiceGameSaveInfo saves = cons!.Resolve<BuncoDiceGameSaveInfo>();
            return saves.ThisStats;
        }
    }
}