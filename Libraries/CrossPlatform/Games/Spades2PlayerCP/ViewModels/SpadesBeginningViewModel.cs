using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.ViewModelInterfaces;
using CommonBasicStandardLibraries.MVVMFramework.Blazor.ViewModels;
using Spades2PlayerCP.Data;
using Spades2PlayerCP.Logic;
using System.Threading.Tasks;
namespace Spades2PlayerCP.ViewModels
{
    [InstanceGame]
    public class SpadesBeginningViewModel : BlazorScreenViewModel, IBlankGameVM
    {
        private readonly Spades2PlayerMainGameClass _mainGame;
        public SpadesBeginningViewModel(CommandContainer commandContainer, Spades2PlayerMainGameClass mainGame, Spades2PlayerVMData model)
        {
            CommandContainer = commandContainer;
            _mainGame = mainGame;
            Model = model;
        }
        public CommandContainer CommandContainer { get; set; }
        public Spades2PlayerVMData Model { get; }
        [Command(EnumCommandCategory.Plain)]
        public async Task TakeCardAsync()
        {
            if (_mainGame.BasicData!.MultiPlayer == true)
            {
                await _mainGame.Network!.SendAllAsync("acceptcard");
            }
            await _mainGame.AcceptCardAsync();
        }
    }
}