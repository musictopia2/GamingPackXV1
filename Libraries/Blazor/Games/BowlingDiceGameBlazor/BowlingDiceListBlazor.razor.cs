using BowlingDiceGameCP.Data;
using CommonBasicStandardLibraries.CollectionClasses;
using Microsoft.AspNetCore.Components;
namespace BowlingDiceGameBlazor
{
    public partial class BowlingDiceListBlazor
    {
        [Parameter]
        public CustomBasicList<SingleDiceInfo>? DiceList { get; set; }
    }
}