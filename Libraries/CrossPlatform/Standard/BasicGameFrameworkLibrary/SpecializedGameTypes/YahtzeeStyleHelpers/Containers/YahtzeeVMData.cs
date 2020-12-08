using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.Dice;
using BasicGameFrameworkLibrary.DIContainers;
using BasicGameFrameworkLibrary.MultiplayerClasses.InterfaceModels;
using BasicGameFrameworkLibrary.MultiplayerClasses.SavedGameClasses;
using CommonBasicStandardLibraries.MVVMFramework.ViewModels;
namespace BasicGameFrameworkLibrary.SpecializedGameTypes.YahtzeeStyleHelpers.Containers
{
	public sealed class YahtzeeVMData<D> : ObservableObject, IBasicDiceGamesData<D>
		where D : SimpleDice, new()
	{
		private readonly CommandContainer _command;
		private readonly IGamePackageResolver _resolver;

		public YahtzeeVMData(CommandContainer command, IGamePackageResolver resolver)
		{
			_command = command;
			_resolver = resolver;
		}

		private string _normalTurn = "";
		[VM]
		public string NormalTurn
		{
			get { return _normalTurn; }
			set
			{
				if (SetProperty(ref _normalTurn, value))
				{
					//can decide what to do when property changes
				}

			}
		}

		private string _status = "";
		[VM] //use this tag to transfer to the actual view model.  this is being done to avoid overflow errors.
		public string Status
		{
			get { return _status; }
			set
			{
				if (SetProperty(ref _status, value))
				{
					//can decide what to do when property changes
				}

			}
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
					//can decide what to do when property changes
				}

			}
		}

		private int _round;
		[VM]
		public int Round
		{
			get { return _round; }
			set
			{
				if (SetProperty(ref _round, value)) { }
			}
		}
		public DiceCup<D>? Cup { get; set; }
		public void LoadCup(ISavedDiceList<D> saveRoot, bool autoResume)
		{
			if (Cup != null && autoResume)
				return;
			Cup = new DiceCup<D>(saveRoot.DiceList, _resolver, _command);
			if (autoResume == true)
				Cup.CanShowDice = true;
			Cup.HowManyDice = 5; //you specify how many dice here.
			Cup.Visible = true; //i think.
			Cup.ShowHold = true;
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
					//can decide what to do when property changes
				}

			}
		}
		private string _categoryChosen = "None";
		[VM]
		public string CategoryChosen
		{
			get { return _categoryChosen; }
			set
			{
				if (SetProperty(ref _categoryChosen, value))
				{
					//can decide what to do when property changes
				}

			}
		}
	}
}