using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.ViewModelInterfaces;
using CommonBasicStandardLibraries.MVVMFramework.Blazor.ViewModels;
using System.Threading.Tasks;
using XactikaCP.Data;
using XactikaCP.Logic;
namespace XactikaCP.ViewModels
{
    [InstanceGame]
    public class XactikaModeViewModel : BlazorScreenViewModel, IBlankGameVM
    {
        public readonly XactikaVMData Model;
        private readonly XactikaGameContainer _gameContainer;
        private readonly IModeProcesses _processes;
        public bool WasResumed { get; private set; }
        public XactikaModeViewModel(CommandContainer commandContainer,
            XactikaVMData model,
            XactikaGameContainer gameContainer,
            IModeProcesses processes
            )
        {
            CommandContainer = commandContainer;
            Model = model;
            WasResumed = Model.ModeChosen != EnumGameMode.None;
            _gameContainer = gameContainer;
            _processes = processes;
        }
        public CommandContainer CommandContainer { get; set; }
        public bool CanMode => Model.ModeChosen != EnumGameMode.None;
        [Command(EnumCommandCategory.Plain)]
        public async Task ModeAsync()
        {
            if (_gameContainer.BasicData.MultiPlayer)
            {
                await _gameContainer.Network!.SendAllAsync("gameoptionchosen", Model.ModeChosen);
            }
            await _processes.ProcessGameOptionChosenAsync(Model.ModeChosen, true);
        }
    }
}