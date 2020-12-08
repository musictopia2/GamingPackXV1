using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.CommonInterfaces;
using Microsoft.AspNetCore.Components;
using aa = CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.BasicDataFunctions;
namespace BasicGamingUIBlazorLibrary.Shells
{
    public partial class SinglePlayerParentShell
    {


        //first major change is here.
        //because the new ui worked great.

        [Parameter]
        public RenderFragment? ChildContent { get; set; }
        public BasicData BasicData { get; set; } //maybe this should be cascaded.

        private IGameInfo _game;

        public SinglePlayerParentShell() //hopefully this works.
        {
            BasicData = aa.Resolve<BasicData>();
            _game = aa.Resolve<IGameInfo>();
        }



        protected override void OnInitialized()
        {
            IStartUp starts = aa.Resolve<IStartUp>();
            starts.StartVariables(BasicData);
            BasicData.ChangeState = ShowChange; //hopefully this simple (?)
            //try to ignore this for now.
            base.OnInitialized();
        }

        private async void ShowChange()
        {
            await InvokeAsync(StateHasChanged);
        }


    }
}