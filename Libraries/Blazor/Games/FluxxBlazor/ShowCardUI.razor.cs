using FluxxCP.UICP;
using Microsoft.AspNetCore.Components;
namespace FluxxBlazor
{
    public partial class ShowCardUI
    {
        [CascadingParameter]
        public CompleteContainerClass? CompleteContainer { get; set; }
        [CascadingParameter]
        public int TargetHeight { get; set; } = 15;
        private string RealHeight => $"{TargetHeight}vh";
        [Parameter]
        public EnumShowCategory ShowCategory { get; set; }
        private DetailCardObservable? _detail;
        private string _text = "";
        protected override void OnParametersSet()
        {
            if (CompleteContainer == null)
            {
                return;
            }
            switch (ShowCategory)
            {
                case EnumShowCategory.MainScreen:
                    _text = "Card Information";
                    _detail = CompleteContainer.GameData.CardDetail;
                    break;
                case EnumShowCategory.CurrentAction:
                    _text = "Current Card Information";
                    _detail = CompleteContainer.ActionContainer.CurrentDetail;
                    break;
                case EnumShowCategory.MainAction:
                    _text = CompleteContainer.ActionContainer.ActionFrameText; //i think.
                    _detail = CompleteContainer.ActionContainer.ActionDetail;
                    break;
                case EnumShowCategory.KeeperScreen:
                    _text = "Current Card Information";
                    _detail = CompleteContainer.KeeperContainer.CardDetail;
                    break;
                default:
                    break;
            }
            base.OnParametersSet();
        }
    }
}