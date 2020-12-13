using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.FileFunctions;
using CommonBasicStandardLibraries.CollectionClasses;
using LifeBoardGameCP.ViewModels;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using System.Reflection;
namespace LifeBoardGameBlazor
{
    public partial class SpinnerAnimationBlazor
    {
        [CascadingParameter]
        public SpinnerViewModel? DataContext { get; set; }
        private Assembly GetAssembly => Assembly.GetAssembly(GetType());

        private CustomBasicList<string> _matrixs = new CustomBasicList<string>();
        protected override void OnInitialized()
        {
            Assembly assembly = GetAssembly;
            var thisData = assembly!.ResourcesAllTextFromFile("arrowmatrix.json");
            _matrixs = JsonConvert.DeserializeObject<CustomBasicList<string>>(thisData);
            DataContext!.GameContainer.RefreshSpinner = ShowChange;
            base.OnInitialized();
        }
        private void ShowChange()
        {
            InvokeAsync(StateHasChanged);
        }
    }
}