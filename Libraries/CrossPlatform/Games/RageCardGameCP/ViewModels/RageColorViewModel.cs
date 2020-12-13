using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.ViewModelInterfaces;
using CommonBasicStandardLibraries.MVVMFramework.Blazor.ViewModels;
using RageCardGameCP.Data;
using System.ComponentModel;
using System.Threading.Tasks;
namespace RageCardGameCP.ViewModels
{
    [InstanceGame]
    public class RageColorViewModel : BlazorScreenViewModel, IBlankGameVM
    {
        public readonly RageCardGameVMData Model;
        public RageColorViewModel(CommandContainer commandContainer, RageCardGameVMData model, RageCardGameGameContainer gameContainer)
        {
            CommandContainer = commandContainer;
            Model = model;
            GameContainer = gameContainer;
            Lead = Model.Lead;
            TrumpSuit = Model.TrumpSuit;
            Model.PropertyChanged += Model_PropertyChanged;
        }
        private void Model_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Lead))
            {
                Lead = Model.Lead;
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
        private string _lead = "";
        public string Lead
        {
            get { return _lead; }
            set
            {
                if (SetProperty(ref _lead, value))
                {
                    
                }
            }
        }
    }
}