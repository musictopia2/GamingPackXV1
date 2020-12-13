using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.DIContainers;
using BasicGameFrameworkLibrary.MultiplayerClasses.MainViewModels;
using BasicGameFrameworkLibrary.TestUtilities;
using CommonBasicStandardLibraries.MVVMFramework.UIHelpers;
using RummyDiceCP.Logic;
using System.Threading.Tasks; //most of the time, i will be using asyncs.
namespace RummyDiceCP.ViewModels
{
	[InstanceGame]
	public class RummyDiceMainViewModel : BasicMultiplayerMainVM
	{
		public readonly RummyDiceMainGameClass MainGame; //if we don't need, delete.
        private readonly BasicData _basicData;

        public RummyDiceMainViewModel(CommandContainer commandContainer,
			RummyDiceMainGameClass mainGame,
			IViewModelData viewModel,
			BasicData basicData,
			TestOptions test,
			IGamePackageResolver resolver
			)
			: base(commandContainer, mainGame, viewModel, basicData, test, resolver)
		{
			MainGame = mainGame;
            _basicData = basicData;
        }
		private int _rollNumber;
		[VM]
		public int RollNumber
		{
			get { return _rollNumber; }
			set
			{
				if (SetProperty(ref _rollNumber, value))
				{
					
				}
			}
		}
		private string _currentPhase = "None";
		[VM]
		public string CurrentPhase
		{
			get { return _currentPhase; }
			set
			{
				if (SetProperty(ref _currentPhase, value))
				{
					
				}
			}
		}
		private int _score;
		[VM]
		public int Score
		{
			get { return _score; }
			set
			{
				if (SetProperty(ref _score, value))
				{
					
				}
			}
		}
		[Command(EnumCommandCategory.Game)]
		public async Task BoardAsync()
		{
			if (_basicData!.MultiPlayer == true)
            {
				await MainGame.Network!.SendAllAsync("boardclicked");
			}
			await MainGame.BoardProcessAsync();
		}
		public bool CanRoll => RollNumber < 4;

        [Command(EnumCommandCategory.Game)]
		public async Task RollAsync()
		{
			if (MainGame.MainBoard1.HasSelectedDice() == true)
			{
				await UIPlatform.ShowMessageAsync("Need to either unselect the dice or use them.");
				return;
			}
			await MainGame.RollDiceAsync();
		}
		[Command(EnumCommandCategory.Game)]
		public async Task CheckAsync()
		{
			if (_basicData!.MultiPlayer == true)
            {
				await MainGame.Network!.SendAllAsync("calculate");
			}
			await MainGame.DoCalculateAsync();
		}
	}
}