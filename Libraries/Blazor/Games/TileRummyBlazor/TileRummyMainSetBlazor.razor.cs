using Microsoft.AspNetCore.Components;
using TileRummyCP.Logic;
namespace TileRummyBlazor
{
    public partial class TileRummyMainSetBlazor
    {
        //this is a wrapper for the mainsets.
        //even mainsets may require a temporary class since tuples don't seem to work well when the arguments are object like and not something like integer or even string.
        [Parameter]
        public MainSets? DataContext { get; set; }
        [Parameter]
        public string ContainerWidth { get; set; } = "";
        [Parameter]
        public string ContainerHeight { get; set; } = "";

    }
}