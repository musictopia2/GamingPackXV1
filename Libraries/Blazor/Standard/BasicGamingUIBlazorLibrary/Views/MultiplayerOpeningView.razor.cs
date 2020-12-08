using System;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using BasicBlazorLibrary.Helpers;
using BasicBlazorLibrary.Components.Basic;
using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.MiscProcesses;
using BasicGameFrameworkLibrary.MultiplayerClasses.BasicPlayerClasses;
using BasicGameFrameworkLibrary.ViewModelInterfaces;
using BasicGameFrameworkLibrary.ViewModels;
using CommonBasicStandardLibraries.CollectionClasses;
using Microsoft.AspNetCore.Components;

namespace BasicGamingUIBlazorLibrary.Views
{
    public partial class MultiplayerOpeningView<P> : IDisposable
         where P : class, IPlayerItem, new()
    {
        [CascadingParameter]
        public MultiplayerOpeningViewModel<P>? DataContext { get; set; }

        [CascadingParameter]
        public IGameInfo? GameData { get; set; }


        private string StartMethod => nameof(IMultiplayerOpeningViewModel.StartAsync);

        private string AutoResumeNetworkMethod => nameof(IMultiplayerOpeningViewModel.ResumeMultiplayerGameAsync);
        private string AutoResumeLocalMethod => nameof(IMultiplayerOpeningViewModel.ResumeSinglePlayerAsync);

        private string HostMethod => nameof(IMultiplayerOpeningViewModel.HostAsync);
        private string ConnectMethod => nameof(IMultiplayerOpeningViewModel.ConnectAsync);
        private string CancelMethod => nameof(IMultiplayerOpeningViewModel.CancelConnectionAsync);

        


        private bool _canHuman;
        private bool _canComputer;

        private CustomBasicList<LabelGridModel> _labels = new CustomBasicList<LabelGridModel>();

        protected override void OnInitialized()
        {
            DataContext!.CommandContainer.AddAction(ShowChange); //means this is necessary.
            _labels.Clear();
            _labels.AddLabel("Players Connected", nameof(IMultiplayerOpeningViewModel.ClientsConnected))
                .AddLabel("Previous Players", nameof(IMultiplayerOpeningViewModel.PreviousNonComputerNetworkedPlayers));
            base.OnInitialized();
        }
        private void ShowChange()
        {
            InvokeAsync(() => StateHasChanged());
        }
        protected override void OnParametersSet()
        {
            if (DataContext == null || GameData == null)
            {
                return;
            }
            _canHuman = OpenPlayersHelper.CanHuman(GameData);
            _canComputer = OpenPlayersHelper.CanComputer(GameData);
            

            base.OnParametersSet();
        }

        public void Dispose()
        {
            DataContext!.CommandContainer.RemoveAction(ShowChange);
        }
    }
}