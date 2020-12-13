using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.MultiplayerClasses.SavedGameClasses;
using SequenceDiceCP.Logic;
namespace SequenceDiceCP.Data
{
	[SingletonGame]
	public class SequenceDiceSaveInfo : BasicSavedBoardDiceGameClass<SequenceDicePlayerItem>
	{
		private string _instructions = "";
		public string Instructions
		{
			get { return _instructions; }
			set
			{
				if (SetProperty(ref _instructions, value))
				{
					if (_model != null)
					{
						_model.Instructions = value;
					}
				}
			}
		}
		private SequenceDiceVMData? _model;
		internal void LoadMod(SequenceDiceVMData model)
		{
			_model = model;
			_model.Instructions = Instructions;
		}
		public EnumGameStatusList GameStatus { get; set; }
		public SequenceBoardCollection GameBoard { get; set; } = new SequenceBoardCollection();
	}
}