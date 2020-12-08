using BasicGameFrameworkLibrary.BasicEventModels;
using CommonBasicStandardLibraries.Messenging;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using static CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.BasicDataFunctions;
namespace BasicGamingUIBlazorLibrary.Shells
{
    public partial class YahtzeeShellView : IHandleAsync<WarningEventModel>
    {
        [Parameter]
        public RenderFragment? ChildContent { get; set; }
        protected bool Opened;
        private WarningEventModel? _warning;
        private IEventAggregator? _aggregator;
        protected override void OnInitialized()
        {
            _aggregator = Resolve<IEventAggregator>();
            _aggregator.Subscribe(this);
            base.OnInitialized();
        }
        Task IHandleAsync<WarningEventModel>.HandleAsync(WarningEventModel message)
        {
            _warning = message;
            Opened = true;
            InvokeAsync(StateHasChanged);
            return Task.CompletedTask;
        }
        private async Task ConfirmAsync(bool rets)
        {
            SelectionChosenEventModel results = new SelectionChosenEventModel();
            if (rets == false)
            {
                results.OptionChosen = EnumOptionChosen.No;
            }
            else
            {
                results.OptionChosen = EnumOptionChosen.Yes;
            }
            Opened = false; //no longer opened.  try this too.
            await _aggregator!.PublishAsync(results);
        }
    }
}