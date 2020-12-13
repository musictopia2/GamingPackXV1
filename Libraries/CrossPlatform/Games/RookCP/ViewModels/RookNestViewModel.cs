using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.ViewModelInterfaces;
using CommonBasicStandardLibraries.MVVMFramework.Blazor.ViewModels;
using CommonBasicStandardLibraries.MVVMFramework.UIHelpers;
using RookCP.Data;
using RookCP.Logic;
using System.Threading.Tasks;
namespace RookCP.ViewModels
{
    [InstanceGame]
    public class RookNestViewModel : BlazorScreenViewModel, IBlankGameVM, ISubmitText
    {
        private readonly RookVMData _model;
        private readonly INestProcesses _processes;
        public string Text => "Choose Nest Cards";
        public RookNestViewModel(CommandContainer commandContainer,
            RookVMData model,
            INestProcesses processes
            )
        {
            CommandContainer = commandContainer;
            _model = model;
            _processes = processes;
        }
        public CommandContainer CommandContainer { get; set; }
        [Command(EnumCommandCategory.Plain)]
        public async Task NestAsync()
        {
            var thisList = _model.PlayerHand1!.ListSelectedObjects();
            if (thisList.Count != 5)
            {
                UIPlatform.ShowError("Sorry, you must choose 5 cards to throw away");
                return;
            }
            await _processes!.ProcessNestAsync(thisList);
        }
    }
}