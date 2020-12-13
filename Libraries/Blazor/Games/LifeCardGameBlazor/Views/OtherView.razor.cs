using LifeCardGameCP.Data;
using LifeCardGameCP.ViewModels;
using Microsoft.AspNetCore.Components;
namespace LifeCardGameBlazor.Views
{
    public partial class OtherView
    {
        [CascadingParameter]
        public OtherViewModel? DataContext { get; set; }
        [CascadingParameter]
        public LifeCardGameVMData? GameData { get; set; }
        private string SubmitMethod => nameof(OtherViewModel.ProcessOtherAsync);
    }
}