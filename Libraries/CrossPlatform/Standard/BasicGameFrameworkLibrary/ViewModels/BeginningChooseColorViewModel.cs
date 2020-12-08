using BasicGameFrameworkLibrary.ChooserClasses;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.MultiplayerClasses.BasicPlayerClasses;
using BasicGameFrameworkLibrary.MultiplayerClasses.InterfacesForHelpers;
using BasicGameFrameworkLibrary.MultiplayerClasses.MiscHelpers;
using BasicGameFrameworkLibrary.ViewModelInterfaces;
using CommonBasicStandardLibraries.MVVMFramework.Blazor.ViewModels;
using System;
using System.Threading.Tasks; //most of the time, i will be using asyncs.
namespace BasicGameFrameworkLibrary.ViewModels
{
    public class BeginningChooseColorViewModel<E, P> : BlazorScreenViewModel, IBlankGameVM, IBeginningColorViewModel, IDisposable
        where E : struct, Enum
        where P : class, IPlayerBoardGame<E>, new()
    {
        private readonly BeginningColorModel<E, P> _model;
        private readonly IBeginningColorProcesses<E> _processes;


        private string _turn = "";

        public string Turn
        {
            get { return _turn; }
            set
            {
                if (SetProperty(ref _turn, value))
                {
                    //can decide what to do when property changes
                }

            }
        }

        private string _instructions = "";

        public string Instructions
        {
            get { return _instructions; }
            set
            {
                if (SetProperty(ref _instructions, value))
                {
                    //can decide what to do when property changes
                }

            }
        }
        public BeginningChooseColorViewModel(CommandContainer commandContainer, BeginningColorModel<E, P> model, IBeginningColorProcesses<E> processes)
        {
            CommandContainer = commandContainer;
            _model = model;
            _processes = processes;
            _processes.SetInstructions = (x => Instructions = x);
            _processes.SetTurn = (x => Turn = x); //has to set delegates before init obviously.
            _model.ColorChooser.AutoSelectCategory = EnumAutoSelectCategory.AutoEvent;
            _model.ColorChooser.ItemClickedAsync += ColorChooser_ItemClickedAsync;
        }
        public BoardGamesColorPicker<E, P> GetColorPicker => _model.ColorChooser;
        protected override Task ActivateAsync()
        {
            return _processes.InitAsync();
        }
        private Task ColorChooser_ItemClickedAsync(E piece)
        {
            return _processes.ChoseColorAsync(piece);
        }

        public void Dispose() //hopefully this simple (?)
        {
            _model.ColorChooser.ItemClickedAsync -= ColorChooser_ItemClickedAsync;
        }

        public CommandContainer CommandContainer { get; set; }
    }
}