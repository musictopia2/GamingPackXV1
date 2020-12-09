using CommonBasicStandardLibraries.Exceptions;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
namespace BasicGamingUIBlazorLibrary.StartupClasses
{
    public partial class BeginningBlazor
    {
        [Parameter]
        public RenderFragment? ChildContent { get; set; }
        [Inject]
        public IJSRuntime? JS { get; set; }

        protected override void OnInitialized()
        {
            GlobalStartUp.JsRuntime = JS;
            if (GlobalStartUp.StartBootStrap == null)
            {
                throw new BasicBlankException("There was nothing to start up the bootstrap");
            }
            GlobalStartUp.StartBootStrap.Invoke();
            base.OnInitialized();
        }
    }
}