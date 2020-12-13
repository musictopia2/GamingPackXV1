using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.ViewModels;
using CommonBasicStandardLibraries.MVVMFramework.UIHelpers;
using FluxxCP.Containers;
using FluxxCP.Logic;
using System.Threading.Tasks;
namespace FluxxCP.ViewModels
{
    public class PlayViewModel : BasicSubmitViewModel
    {
        private readonly IPlayProcesses _playProcesses;
        private readonly FluxxMainGameClass _mainGame;
        private readonly FluxxGameContainer _gameContainer;
        private readonly FluxxVMData _model;
        public override string Text => "Play";
        public PlayViewModel(CommandContainer commandContainer, IPlayProcesses playProcesses, FluxxMainGameClass mainGame, FluxxGameContainer gameContainer, FluxxVMData model) : base(commandContainer)
        {
            _playProcesses = playProcesses;
            _mainGame = mainGame;
            _gameContainer = gameContainer;
            _model = model;
        }
        private bool IsOtherTurn => _mainGame!.OtherTurn > 0;
        public override bool CanSubmit => !IsOtherTurn;
        public override async Task SubmitAsync()
        {
            if (_model.Goal1!.ObjectSelected() > 0)
            {
                ToastPlatform.ShowError("Cannot select any goal cards to play");
                _model.UnselectAllCards();
                return;
            }
            if (_model.Keeper1!.ObjectSelected() > 0)
            {
                ToastPlatform.ShowError("Cannot select any keeper cards to play");
                _model.UnselectAllCards();
                return;
            }
            if (_gameContainer!.NeedsToRemoveGoal())
            {
                ToastPlatform.ShowError("Cannot choose any cards to play until you discard a goal");
                _model.UnselectAllCards();
                return;
            }
            int howMany = _model.PlayerHand1!.HowManySelectedObjects;
            if (howMany != 1)
            {
                ToastPlatform.ShowError("Must choose only one card to play");
                _model.UnselectAllCards();
                return;
            }
            if (_mainGame.SaveRoot!.PlaysLeft <= 0)
            {
                ToastPlatform.ShowError("Sorry; you don't have any plays left");
                _model.UnselectAllCards();
                return;
            }
            int deck = _model.PlayerHand1.ObjectSelected();
            await _playProcesses.SendPlayAsync(deck);
            await _playProcesses.PlayCardAsync(deck);
        }
    }
}