using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.ColorCards;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.ViewModelInterfaces;
using CommonBasicStandardLibraries.MVVMFramework.Blazor.ViewModels;
using System.Threading.Tasks;
using UnoCP.Data;
using UnoCP.Logic;
namespace UnoCP.ViewModels
{
    [InstanceGame]
    public class ChooseColorViewModel : BlazorScreenViewModel, IBlankGameVM
    {
        public readonly UnoVMData Model;
        private readonly IChooseColorProcesses _processes;
        public ChooseColorViewModel(CommandContainer commandContainer, UnoVMData model, IChooseColorProcesses processes)
        {
            CommandContainer = commandContainer;
            Model = model;
            _processes = processes;
            Model.ColorPicker.ItemClickedAsync += ColorPicker_ItemClickedAsync;
        }
        protected override Task TryCloseAsync()
        {
            Model.ColorPicker.ItemClickedAsync -= ColorPicker_ItemClickedAsync;
            return base.TryCloseAsync();
        }
        private async Task ColorPicker_ItemClickedAsync(EnumColorTypes piece)
        {
            await _processes!.ColorChosenAsync(piece);
        }
        public CommandContainer CommandContainer { get; set; }
    }
}