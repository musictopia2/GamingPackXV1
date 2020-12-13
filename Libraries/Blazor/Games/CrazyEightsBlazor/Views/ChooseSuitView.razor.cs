using CrazyEightsCP.Data;
using static CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.BasicDataFunctions;
namespace CrazyEightsBlazor.Views
{
    public partial class ChooseSuitView
    {
        private CrazyEightsVMData? _data;
        protected override void OnInitialized()
        {
            _data = cons!.Resolve<CrazyEightsVMData>();
            base.OnInitialized();
        }
    }
}