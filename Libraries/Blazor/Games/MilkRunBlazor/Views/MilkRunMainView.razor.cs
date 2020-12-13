using BasicBlazorLibrary.Helpers;
using BasicBlazorLibrary.Components.Basic;
using BasicGameFrameworkLibrary.TestUtilities;
using CommonBasicStandardLibraries.CollectionClasses;
using Microsoft.AspNetCore.Components;
using MilkRunCP.Data;
using MilkRunCP.ViewModels;
using static CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.BasicDataFunctions;
namespace MilkRunBlazor.Views
{
    public partial class MilkRunMainView
    {
        [CascadingParameter]
        public TestOptions? TestData { get; set; }
        private readonly CustomBasicList<LabelGridModel> _labels = new CustomBasicList<LabelGridModel>();
        private MilkRunVMData? _vmData;
        private MilkRunGameContainer? _gameContainer;
        private CustomBasicList<MilkRunPlayerItem> _players = new CustomBasicList<MilkRunPlayerItem>();
        protected override void OnInitialized()
        {
            _vmData = cons!.Resolve<MilkRunVMData>();
            _gameContainer = cons.Resolve<MilkRunGameContainer>();
            _labels.Clear();
            _labels.AddLabel("Turn", nameof(MilkRunMainViewModel.NormalTurn));
            _players = _gameContainer.PlayerList!.GetAllPlayersStartingWithSelf();
            _players.Reverse();
            base.OnInitialized();
        }
        private string AnimationPileName(MilkRunPlayerItem player, EnumMilkType category)
        {
            return $"{category}{player.NickName}";
        }
    }
}