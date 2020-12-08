using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.Dice;
using Microsoft.AspNetCore.Components;
using System;
using aa = CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.BasicDataFunctions;
namespace BasicGamingUIBlazorLibrary.BasicControls.Misc
{
    public partial class DiceListConrolBlazor<D> : IDisposable
        where D : IStandardDice, new()
    {
        [Parameter]
        public DiceCup<D>? Cup { get; set; }
        [Parameter]
        public Action? OnChange { get; set; }
        [Parameter]
        public string TargetHeight { get; set; } = "15vh"; //decide what the defaults should be.  can always adjust so some games can be larger and some games not as large.

        //looks like there is a serious issue.  because here, we need to update just a certain one.  i think commandcontainer is still proper.
        //but this time, will attempt to have a dictionary.
        //probably can't override this time though.
        private CommandContainer _command;
        public DiceListConrolBlazor()
        {
            _command = aa.Resolve<CommandContainer>();
            _command.AddAction(ShowChange, "dicecup");
        }
        private async void ShowChange()
        {
            await InvokeAsync(StateHasChanged);
        }
        void IDisposable.Dispose()
        {
            _command.RemoveAction("dicecup"); //no need for the action since this uses a dictionary.
        }
    }
}