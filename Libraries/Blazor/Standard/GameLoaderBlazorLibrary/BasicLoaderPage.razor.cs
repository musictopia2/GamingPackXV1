using System;
using cc = CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.BasicDataFunctions;
namespace GameLoaderBlazorLibrary
{
    public partial class BasicLoaderPage : IDisposable
    {
        //use custom here too.


        private ILoaderVM? DataContext { get; set; } //attempt to not make it static (?)
        private bool _disposedValue;

        protected override void OnInitialized()
        {
            DataContext = cc.cons!.Resolve<ILoaderVM>();
            DataContext.StateChanged = () => InvokeAsync(StateHasChanged);
            base.OnInitialized();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    DataContext!.StateChanged = null;
                }

                _disposedValue = true;
            }
        }
        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}