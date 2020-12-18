using BasicGameFrameworkLibrary.StandardImplementations.Settings;
using BasicGamingUIBlazorLibrary.Extensions;
using CommonBasicStandardLibraries.MVVMFramework.UIHelpers;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;
using aa = BasicBlazorLibrary.Components.CssGrids.Helpers;
namespace GameLoaderBlazorLibrary
{
    public partial class GameSettingsComponent
    {
        [Parameter]
        public EventCallback CloseSettings { get; set; }
        [Inject]
        IJSRuntime? JS { get; set; } //this should have its own js.
        private static string GetRows => aa.RepeatAuto(7);
        private static string GetColumns => aa.RepeatMaximum(2); //may eventually do a special grid control that specialize in adding in data and it would just work.
        private bool _beginaccept;
        protected override void OnInitialized()
        {
            _beginaccept = GlobalDataModel.NickNameAcceptable();
            base.OnInitialized();
        }
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender && GlobalDataModel.NickNameAcceptable() == false)
            {
                await _nickElement.FocusAsync();
                return;
            }
            if (_useCustom)
            {
                await _customElement.FocusAsync();
                _useCustom = false;
            }
        }
        private bool _useCustom;
        private void UpdateServerOptions(EnumAzureMode mode)
        {
            GlobalDataModel.DataContext!.ServerMode = mode;
            if (mode == EnumAzureMode.Custom)
            {
                _useCustom = true;
            }
        }
        private void ChangeFastAnimation()
        {
            GlobalDataModel.DataContext!.FastAnimation = !GlobalDataModel.DataContext.FastAnimation;
        }
        private string GetAnimationText
        {
            get
            {
                if (GlobalDataModel.DataContext!.FastAnimation)
                {
                    return "Use Regular Animation";
                }
                return "Use Fast Animation";
            }
        }
        private async Task SaveChangesAsync()
        {
            if (GlobalDataModel.DataContext == null || JS == null)
            {
                return;
            }
            if (GlobalDataModel.NickNameAcceptable() == false)
            {
                ToastPlatform.ShowError("Needs to enter nick name at least");
                await _nickElement.FocusAsync();
                return;
            }
            await JS.SaveGlobalDataAsync();
            ToastPlatform.ShowSuccess($"Saved Changes On {DateTime.Now}");
            await CloseSettings.InvokeAsync();
        }
        private async Task CancelAsync()
        {
            await CloseSettings.InvokeAsync();
        }
    }
}