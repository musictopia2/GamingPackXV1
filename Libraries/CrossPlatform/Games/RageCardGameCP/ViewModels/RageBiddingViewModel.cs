using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.ViewModelInterfaces;
using CommonBasicStandardLibraries.MVVMFramework.Blazor.ViewModels;
using RageCardGameCP.Data;
using RageCardGameCP.Logic;
using System.ComponentModel;
using System.Threading.Tasks;
namespace RageCardGameCP.ViewModels
{
    [InstanceGame]
    public class RageBiddingViewModel : BlazorScreenViewModel, IBlankGameVM
    {
        public readonly RageCardGameVMData Model;
        private readonly IBidProcesses _processes;
        public RageBiddingViewModel(CommandContainer commandContainer, RageCardGameVMData model, IBidProcesses processes, RageCardGameGameContainer gameContainer)
        {
            CommandContainer = commandContainer;
            Model = model;
            _processes = processes;
            GameContainer = gameContainer;
            NormalTurn = Model.NormalTurn;
            TrumpSuit = Model.TrumpSuit;
            Model.PropertyChanged += Model_PropertyChanged;
        }
        private void Model_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(NormalTurn))
            {
                NormalTurn = Model.NormalTurn;
            }
            if (e.PropertyName == nameof(TrumpSuit))
            {
                TrumpSuit = Model.TrumpSuit;
            }
        }
        protected override Task TryCloseAsync()
        {
            Model.PropertyChanged -= Model_PropertyChanged;
            return base.TryCloseAsync();
        }
        public CommandContainer CommandContainer { get; set; }
        public RageCardGameGameContainer GameContainer { get; }
        public bool CanBid => Model.BidAmount > -1;
        [Command(EnumCommandCategory.Plain)]
        public async Task BidAsync()
        {
            await _processes.ProcessBidAsync();
        }
        private string _normalTurn = "";
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
        private EnumColor _trumpSuit;
        public EnumColor TrumpSuit
        {
            get { return _trumpSuit; }
            set
            {
                if (SetProperty(ref _trumpSuit, value))
                {
                    
                }
            }
        }
    }
}