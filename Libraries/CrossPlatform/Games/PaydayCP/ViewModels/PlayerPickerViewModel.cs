using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.ViewModels;
using PaydayCP.Data;
using PaydayCP.Logic;
using System.Threading.Tasks;
namespace PaydayCP.ViewModels
{
    public class PlayerPickerViewModel : BasicSubmitViewModel
    {
        private readonly PaydayVMData _model;
        private readonly IChoosePlayerProcesses _processes;
        public PlayerPickerViewModel(CommandContainer commandContainer, PaydayVMData model, IChoosePlayerProcesses processes) : base(commandContainer)
        {
            _model = model;
            _processes = processes;
        }
        public override bool CanSubmit => _model.PopUpChosen != "";
        public override Task SubmitAsync()
        {
            return _processes.ProcessChosenPlayerAsync();
        }
    }
}