using BasicGameFrameworkLibrary.ViewModelInterfaces;
using BasicGameFrameworkLibrary.ViewModels;
using Microsoft.AspNetCore.Components;
namespace BasicGamingUIBlazorLibrary.Views
{
    public abstract partial class BasicSubmitView<VM>
        where VM : class, ISubmitText
    {
        [CascadingParameter]
        public VM? DataContext { get; set; }
        private string GetText => DataContext!.Text;

        [Parameter]
        public string FontSize { get; set; } = "3vh";
        protected virtual string CommandText => nameof(BasicSubmitViewModel.SubmitAsync); //this part is okay.
    }
}