using Microsoft.AspNetCore.Components;
using System.Reflection;
namespace LifeBoardGameBlazor
{
    public partial class SpinnerGraphicsBlazor
    {
        [Parameter]
        public string TargetHeight { get; set; } = "";
        [Parameter]
        public RenderFragment? ChildContent { get; set; }
        private Assembly GetAssembly => Assembly.GetAssembly(GetType());
    }
}