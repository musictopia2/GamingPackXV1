using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.BasicGameDataClasses;
using CommonBasicStandardLibraries.MVVMFramework.ViewModels;
namespace BattleshipCP.Data
{
	[SingletonGame]
	public class BattleshipVMData : ObservableObject, IViewModelData
	{
		private string _normalTurn = "";
		[VM]
		public string NormalTurn
		{
			get { return _normalTurn; }
			set
			{
				if (SetProperty(ref _normalTurn, value))
				{
					
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
					
				}
			}
		}
		private EnumShipList _shipSelected;
		[VM]
		public EnumShipList ShipSelected
		{
			get
			{
				return _shipSelected;
			}
			set
			{
				if (SetProperty(ref _shipSelected, value) == true)
				{
				}
			}
		}
		private bool _shipDirectionsVisible;
		[VM]
		public bool ShipDirectionsVisible
		{
			get { return _shipDirectionsVisible; }
			set
			{
				if (SetProperty(ref _shipDirectionsVisible, value))
				{
					
				}
			}
		}
		public BattleshipVMData()
		{

		}
	}
}