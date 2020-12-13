using LifeBoardGameCP.Data;
using static CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.BasicDataFunctions;
namespace LifeBoardGameBlazor.Views
{
    public partial class ShowCardView
    {
        private LifeBoardGameVMData? DataContext { get; set; }
        protected override void OnInitialized()
        {
            DataContext = cons!.Resolve<LifeBoardGameVMData>();
            base.OnInitialized();
        }
    }
}