using BasicBlazorLibrary.Helpers;
using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGameFrameworkLibrary.TestUtilities;
using CommonBasicStandardLibraries.MVVMFramework.UIHelpers;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Threading.Tasks;
using aa = CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.BasicDataFunctions;
namespace BasicGamingUIBlazorLibrary.Shells
{
    public partial class MultiplayerBasicParentShell
    {
        [Parameter]
        public RenderFragment? ChildContent { get; set; }
        public BasicData? BasicData { get; set; } //maybe this should be cascaded.
        public IGameInfo? GameData { get; set; } //i don't think this one needs to
        public TestOptions? TestData { get; set; }



        [Inject]
        private IJSRuntime? JS { get; set; } //now needs this because will attempt to do into local storage even on desktop mode (not sure if this will work or not).
        private bool _loading = true;
        private bool _hadNickName;
        protected override void OnInitialized()
        {
            BasicData = aa.Resolve<BasicData>();
            TestData = aa.Resolve<TestOptions>();
            GameData = aa.Resolve<IGameInfo>();
            IStartUp starts = aa.Resolve<IStartUp>();
            starts.StartVariables(BasicData); //eventually would do something else to figure out who it is.
            if (BasicData.NickName != "")
            {
                _hadNickName = true; //because it got it somehow even if from native or other external process.
            }
            base.OnInitialized();
        }
        private static Task ProcessNickNameAsync()
        {
            return Task.CompletedTask;
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (BasicData == null || JS == null)
            {
                return;
            }
            if (firstRender && BasicData.NickName == "")
            {
                string item = await JS.StorageGetItemAsync<string>("nickname");
                if (string.IsNullOrWhiteSpace(item) == false)
                {
                    BasicData.NickName = item;
                    _hadNickName = true;
                }
                _loading = false;
                StateHasChanged(); //try this too.
            }
        }
        protected override void OnParametersSet()
        {
            if (BasicData == null)
            {
                return;
            }
            if (_hadNickName == false && BasicData.NickName != "")
            {
                _loading = true;

            }
            base.OnParametersSet();
        }
        private async void SetNickNameAsync()
        {
            if (BasicData == null || JS == null)
            {
                return;
            }
            if (_hadNickName == false && BasicData.NickName != "")
            {
                await JS.StorageSetItemAsync("nickname", BasicData.NickName);
            }
        }
    }
}