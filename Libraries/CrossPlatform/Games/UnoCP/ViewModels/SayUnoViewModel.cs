using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.ViewModelInterfaces;
using CommonBasicStandardLibraries.MVVMFramework.Blazor.ViewModels;
using System.Threading.Tasks;
using UnoCP.Data;
using UnoCP.Logic;
namespace UnoCP.ViewModels
{
    [InstanceGame]
    public class SayUnoViewModel : BlazorScreenViewModel, IBlankGameVM
    {
        private readonly ISayUnoProcesses _processes;
        private readonly UnoVMData _model;
        public SayUnoViewModel(CommandContainer commandContainer, ISayUnoProcesses processes, UnoVMData model)
        {
            CommandContainer = commandContainer;
            _processes = processes;
            _model = model;
            _model.Stops.TimeUp += Stops_TimeUp;
            CommandContainer.ExecutingChanged += CommandContainer_ExecutingChanged;
        }
        private void CommandContainer_ExecutingChanged()
        {
            if (CommandContainer.IsExecuting)
            {
                return;
            }
            NotifyOfCanExecuteChange(nameof(CanUno));
            _model.Stops.StartTimer();
        }

        private async void Stops_TimeUp()
        {
            CommandContainer!.ManuelFinish = true;
            CommandContainer.IsExecuting = true;
            await _processes.ProcessUnoAsync(false);
        }
        protected override Task TryCloseAsync()
        {
            _model.Stops.TimeUp -= Stops_TimeUp;
            CommandContainer.ExecutingChanged -= CommandContainer_ExecutingChanged;
            return base.TryCloseAsync();
        }
        public CommandContainer CommandContainer { get; set; }
        public bool CanUno => true;
        [Command(EnumCommandCategory.Plain)]
        public async Task SayUnoAsync()
        {
            _model.Stops!.PauseTimer();
            await _processes!.ProcessUnoAsync(true);
        }
    }
}