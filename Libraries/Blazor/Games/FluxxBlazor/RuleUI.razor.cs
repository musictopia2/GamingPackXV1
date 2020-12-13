using Microsoft.AspNetCore.Components;
namespace FluxxBlazor
{
    public partial class RuleUI : SimpleActionView
    {
        [CascadingParameter]
        public int TargetHeight { get; set; } = 15;
        private string RealHeight => $"{TargetHeight}vh";
    }
}