using Microsoft.AspNetCore.Components;
using RummyDiceCP.Data;
using RummyDiceCP.ViewModels;
using System.Threading.Tasks;
namespace RummyDiceBlazor
{
    public partial class RummyHandBlazor
    {
        [Parameter]
        public RummyDiceHandVM? DataContext { get; set; }
        //looks like there is a case with the button where we already have a command.  needs to account for that as well.
        [Parameter]
        public string TargetHeight { get; set; } = ""; //for now, do here.
        private string TempSetName => $"Temp Set {DataContext!.Index}";

        private async Task OnClickAsync(RummyDiceInfo dice)
        {
            if (DataContext!.DiceCommand.CanExecute(dice) == false)
            {
                return;
            }
            await DataContext.DiceCommand.ExecuteAsync(dice);
        }
    }
}