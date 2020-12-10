using Microsoft.AspNetCore.Components;
using System;
namespace GameLoaderBlazorLibrary
{
    public partial class BasicLoaderPage : IDisposable
    {
        [Inject]
        public ILoaderVM? DataContext { get; set; }

        private bool _disposedValue;

        protected override void OnInitialized()
        {
            DataContext!.StateChanged = () => InvokeAsync(StateHasChanged);
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