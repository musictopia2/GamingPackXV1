using Microsoft.AspNetCore.Components;
using MillebournesCP.ViewModels;
namespace MillebournesBlazor.Views
{
    public partial class CoupeView //could have inherited and overrided but not worth redoing this one to do so.
    {
        [CascadingParameter]
        public CoupeViewModel? DataContext { get; set; }
        private string CoupeMethod => nameof(CoupeViewModel.CoupeAsync);
    }
}