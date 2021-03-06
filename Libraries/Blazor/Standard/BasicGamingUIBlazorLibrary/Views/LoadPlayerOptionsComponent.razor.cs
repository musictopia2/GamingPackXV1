using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.MiscProcesses;
using BasicGameFrameworkLibrary.MultiplayerClasses.BasicPlayerClasses;
using BasicGameFrameworkLibrary.ViewModelInterfaces;
using BasicGameFrameworkLibrary.ViewModels;
using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
using CommonBasicStandardLibraries.CollectionClasses;
using Microsoft.AspNetCore.Components;
using System.Linq;

namespace BasicGamingUIBlazorLibrary.Views
{
    public partial class LoadPlayerOptionsComponent<P>
        where P : class, IPlayerItem, new()
    {
        [Parameter]
        public EnumPlayOptions PlayOption { get; set; }
        //good news is with computerextra, can use that as a hint to see what should be displayed.


        [CascadingParameter]
        public IGameInfo? GameData { get; set; }
        [CascadingParameter]
        public BasicData? BasicInfo { get; set; }
        [CascadingParameter]
        public MultiplayerOpeningViewModel<P>? DataContext { get; set; }
        private (string Method, int Parameter, string Display) CommandData(int amount)
        {
            //hint, the amount being sent in here was wrong.

            int increment;
            if (PlayOption == EnumPlayOptions.HumanLocal)
            {
                increment = amount + 1;
            }
            else
            {
                increment = amount;
            }
            string path;
            string header;
            switch (PlayOption)
            {
                case EnumPlayOptions.ComputerExtra:
                    path = nameof(IMultiplayerOpeningViewModel.StartAsync);
                    header = $"{increment} Extra Computer Players";
                    break;
                case EnumPlayOptions.ComputerLocal:
                    path = nameof(IMultiplayerOpeningViewModel.StartComputerSinglePlayerGameAsync);
                    header = $"{increment} Local Computer Players";
                    break;
                case EnumPlayOptions.HumanLocal:
                    path = nameof(IMultiplayerOpeningViewModel.StartPassAndPlayGameAsync);
                    header = $"{increment} Pass And Play Human Players";
                    break;
                default:
                    return ("", 0, "");
            }
            return (path, increment, header);
        }

        private CustomBasicList<int> _completeList = new CustomBasicList<int>();
        private (string Method, string Display) SolitaireData()
        {
            return (nameof(IMultiplayerOpeningViewModel.SolitaireAsync), "New Single Player Game");
        }
        protected override void OnParametersSet()
        {
            if (GameData == null)
            {
                return;
            }
            _completeList = OpenPlayersHelper.GetPossiblePlayers(GameData);
            if (PlayOption == EnumPlayOptions.ComputerExtra)
            {
                
                for (int i = 0; i < _completeList.Count; i++)
                {
                    _completeList[i] = _completeList[i] - DataContext!.ClientsConnected;
                }
                if (_completeList.First() == 0)
                {
                    _completeList.RemoveFirstItem(); //because there is already an option to have no extra computer players.
                }

            }
            base.OnParametersSet();
        }
    }
}