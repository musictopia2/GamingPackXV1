using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.MultiplayerClasses.SavedGameClasses;
using BasicGameFrameworkLibrary.RegularDeckOfCards;
namespace CrazyEightsCP.Data
{
	[SingletonGame]
	public class CrazyEightsSaveInfo : BasicSavedCardClass<CrazyEightsPlayerItem, RegularSimpleCard>
	{

		private bool _chooseSuit;
		public bool ChooseSuit
		{
			get { return _chooseSuit; }
			set
			{
				if (SetProperty(ref _chooseSuit, value))
				{
					if (_model != null)
					{
						_model.ChooseSuit = value;
					}
				}
			}
		}
		public EnumSuitList CurrentSuit { get; set; }
		public EnumRegularCardValueList CurrentNumber { get; set; }
		private CrazyEightsVMData? _model;
		public void LoadMod(CrazyEightsVMData model)
		{
			_model = model;
		}
	}
}