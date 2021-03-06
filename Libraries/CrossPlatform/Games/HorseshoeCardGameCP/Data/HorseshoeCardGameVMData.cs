using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.BasicDrawables.Dictionary;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.DrawableListsObservable;
using BasicGameFrameworkLibrary.Extensions;
using BasicGameFrameworkLibrary.MultiplayerClasses.BasicPlayerClasses;
using BasicGameFrameworkLibrary.MultiplayerClasses.InterfaceModels;
using BasicGameFrameworkLibrary.RegularDeckOfCards;
using BasicGameFrameworkLibrary.SpecializedGameTypes.TrickClasses;
using CommonBasicStandardLibraries.Exceptions;
using CommonBasicStandardLibraries.MVVMFramework.ViewModels;
using HorseshoeCardGameCP.Cards;
using HorseshoeCardGameCP.Logic;
namespace HorseshoeCardGameCP.Data
{
	[SingletonGame]
	[AutoReset]
	public class HorseshoeCardGameVMData : ObservableObject, ITrickCardGamesData<HorseshoeCardGameCardInformation, EnumSuitList>
		, ITrickDummyHand<EnumSuitList, HorseshoeCardGameCardInformation>
	{
		private readonly HorseshoeCardGameGameContainer _gameContainer;
		public HorseshoeCardGameVMData(CommandContainer command,
			HorseshoeTrickAreaCP trickArea1,
			HorseshoeCardGameGameContainer gameContainer
			)
		{
			Deck1 = new DeckObservablePile<HorseshoeCardGameCardInformation>(command);
			Pile1 = new SingleObservablePile<HorseshoeCardGameCardInformation>(command);
			PlayerHand1 = new HandObservable<HorseshoeCardGameCardInformation>(command);
			TrickArea1 = trickArea1;
			_gameContainer = gameContainer;
			PlayerHand1.Maximum = 6;
			gameContainer.GetCurrentHandList = GetCurrentHandList;
		}
		public HorseshoeTrickAreaCP TrickArea1 { get; set; }
		BasicTrickAreaObservable<EnumSuitList, HorseshoeCardGameCardInformation> ITrickCardGamesData<HorseshoeCardGameCardInformation, EnumSuitList>.TrickArea1
		{
			get => TrickArea1;
			set => TrickArea1 = (HorseshoeTrickAreaCP)value;
		}
		public DeckObservablePile<HorseshoeCardGameCardInformation> Deck1 { get; set; }
		public SingleObservablePile<HorseshoeCardGameCardInformation> Pile1 { get; set; }
		public HandObservable<HorseshoeCardGameCardInformation> PlayerHand1 { get; set; }
		public SingleObservablePile<HorseshoeCardGameCardInformation>? OtherPile { get; set; }
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
		private EnumSuitList _trumpSuit;
		[VM]
		public EnumSuitList TrumpSuit
		{
			get { return _trumpSuit; }
			set
			{
				if (SetProperty(ref _trumpSuit, value)) { }
			}
		}
		public DeckRegularDict<HorseshoeCardGameCardInformation> GetCurrentHandList()
		{
			DeckRegularDict<HorseshoeCardGameCardInformation> output = _gameContainer!.SingleInfo!.MainHandList.ToRegularDeckDict();
			output.AddRange(_gameContainer.SingleInfo.TempHand!.ValidCardList);
			return output;
		}

		public int CardSelected()
		{
			if (_gameContainer!.SingleInfo!.PlayerCategory != EnumPlayerCategory.Self)
            {
				throw new BasicBlankException("Only self can get card selected.  If I am wrong, rethink");
			}
			int selects = PlayerHand1!.ObjectSelected();
			int others = _gameContainer.SingleInfo.TempHand!.CardSelected;
			if (selects != 0 && others != 0)
            {
				throw new BasicBlankException("You cannot choose from both hand and temps.  Rethink");
			}
			if (selects != 0)
            {
				return selects;
			}
			return others;
		}
		public void RemoveCard(int deck)
		{
			bool rets = _gameContainer!.SingleInfo!.MainHandList.ObjectExist(deck);
			if (rets == true)
			{
				_gameContainer.SingleInfo.MainHandList.RemoveObjectByDeck(deck);
				return;
			}
			var thisCard = _gameContainer.SingleInfo.TempHand!.CardList.GetSpecificItem(deck);
			if (thisCard.IsEnabled == false)
			{
				throw new BasicBlankException("Card was supposed to be disabled");
			}
			_gameContainer.SingleInfo.TempHand.HideCard(thisCard);
		}
	}
}