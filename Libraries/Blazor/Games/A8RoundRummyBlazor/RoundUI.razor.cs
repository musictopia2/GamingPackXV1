using A8RoundRummyCP.Data;
using CommonBasicStandardLibraries.CollectionClasses;
using Microsoft.AspNetCore.Components;
namespace A8RoundRummyBlazor
{
    public partial class RoundUI
    {
        [Parameter]
        public CustomBasicList<RoundClass>? Rounds { get; set; }
    }
}