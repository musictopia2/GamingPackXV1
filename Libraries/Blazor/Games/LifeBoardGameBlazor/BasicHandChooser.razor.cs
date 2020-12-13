using BasicGameFrameworkLibrary.ViewModels;
using LifeBoardGameCP.Data;
using Microsoft.AspNetCore.Components;
namespace LifeBoardGameBlazor
{
    public partial class BasicHandChooser<B>
        where B : BasicSubmitViewModel
    {
        [CascadingParameter]
        public B? DataContext { get; set; }
        [CascadingParameter]
        public LifeBoardGameVMData? GameContainer { get; set; }
        private string SubmitMethod => nameof(BasicSubmitViewModel.SubmitAsync);
    }
}