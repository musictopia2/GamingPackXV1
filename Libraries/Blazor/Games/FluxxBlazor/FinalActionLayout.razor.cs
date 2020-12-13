using Microsoft.AspNetCore.Components;
namespace FluxxBlazor
{
    public partial class FinalActionLayout
    {
        [CascadingParameter]
        public CompleteContainerClass? CompleteContainer { get; set; }
        [Parameter]
        public RenderFragment? ChildContent { get; set; }
        private string FirstGridColumns => "33vw 33vw auto";
    }
}