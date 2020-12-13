using BasicGameFrameworkLibrary.ViewModels;
using Microsoft.AspNetCore.Components;
using PaydayCP.Data;
namespace PaydayBlazor.Views
{
    public partial class BasicPickerView<V>
        where V : BasicSubmitViewModel
    {
        [CascadingParameter]
        public V? DataContext { get; set; }
        [CascadingParameter]
        public PaydayVMData? VMData { get; set; }
        public string SubmitMethod => nameof(BasicSubmitViewModel.SubmitAsync);
    }
}