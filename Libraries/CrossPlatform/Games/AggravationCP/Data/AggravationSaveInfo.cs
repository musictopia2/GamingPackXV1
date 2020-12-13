using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.MultiplayerClasses.SavedGameClasses;
using CommonBasicStandardLibraries.CollectionClasses;
namespace AggravationCP.Data
{
	[SingletonGame]
	public class AggravationSaveInfo : BasicSavedBoardDiceGameClass<AggravationPlayerItem>
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
		private AggravationVMData? _model;
		internal void LoadMod(AggravationVMData model)
		{
			_model = model;
			_model.Instructions = Instructions;
		}
		public int PreviousSpace { get; set; }
		public CustomBasicList<MoveInfo> MoveList { get; set; } = new CustomBasicList<MoveInfo>();
		public EnumColorChoice OurColor { get; set; }
		public int DiceNumber { get; set; }
	}
}