using BattleshipCP.Data;
using CommonBasicStandardLibraries.CollectionClasses;
using Microsoft.AspNetCore.Components;
namespace BattleshipBlazor
{
    public partial class ShipControlBlazor
    {
        [Parameter]
        public CustomBasicList<ShipInfoCP>? Ships { get; set; }
        [Parameter]
        public string TargetHeight { get; set; } = "";
        public ShipControlBlazor()
        {
            System.Console.WriteLine("Creating Ship Control");
        }
    }
}