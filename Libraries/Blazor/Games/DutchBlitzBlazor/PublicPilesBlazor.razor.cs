using BasicGamingUIBlazorLibrary.Extensions;
using DutchBlitzCP.Data;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
namespace DutchBlitzBlazor
{
    public partial class PublicPilesBlazor
    {
        [CascadingParameter]
        public DutchBlitzVMData? GameData { get; set; }
        [CascadingParameter]
        public int TargetHeight { get; set; }
        private string RealHeight => TargetHeight.HeightString();
        private enum EnumLocation
        {
            None, Top, Bottom
        }
        private async Task NewClickedAsync()
        {
            await GameData!.PublicPiles1.NewCommand.ExecuteAsync(null);
        }
    }
}