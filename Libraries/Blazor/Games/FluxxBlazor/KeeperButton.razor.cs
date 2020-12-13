using FluxxCP.ViewModels;
using Microsoft.AspNetCore.Components;
namespace FluxxBlazor
{
    public partial class KeeperButton<K>
        where K : class
    {
        [CascadingParameter]
        public K? DataContext { get; set; }
        [Parameter]
        public bool StartsOnNewLine { get; set; } = false;
        private string ShowMethod => nameof(BasicActionScreen.ShowKeepersAsync);
    }
}