using BasicGameFrameworkLibrary.BasicEventModels;
using BasicGameFrameworkLibrary.StandardImplementations.Settings;
using BasicGamingUIBlazorLibrary.Extensions;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;
namespace GameLoaderBlazorLibrary
{
    public partial class BasicLoaderPage : IDisposable
    {
        [Inject]
        public ILoaderVM? DataContext { get; set; }

        [Inject]
        public IJSRuntime? JS { get; set; }

        private bool _loadedOnce;

        private bool _showSettings;


        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (GlobalClass.Multiplayer == false)
            {
                return;
            }
            if (firstRender)
            {
                _loadedOnce = true;
                await JS!.LoadGlobalDataAsync(); //needed to load data.
                if (GlobalDataModel.NickNameAcceptable() == false)
                {
                    _showSettings = true;
                }
                StateHasChanged();
            }
        }


        private bool CanShowGameList()
        {
            if (_showSettings)
            {
                return false; //because the settings are shown.
            }
            if (GlobalClass.Multiplayer == false)
            {
                return true;
            }
            if (GlobalDataModel.DataContext == null)
            {
                return false;
            }
            return string.IsNullOrWhiteSpace(GlobalDataModel.DataContext.NickName) == false;
        }


        private void BackToMain()
        {
            DataContext!.GameName = ""; //needs to be able to set the game name so the main page will load again.
        }
        private void ClosedSettings()
        {
            _showSettings = false;
        }

        private void OpenSettings()
        {
            _showSettings = true;
        }


        private bool _disposedValue;

        protected override void OnInitialized()
        {
            DataContext!.StateChanged = () => InvokeAsync(StateHasChanged);
            if (GlobalClass.Multiplayer == false)
            {
                _loadedOnce = true; //because not important since not using the settings.
            }
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