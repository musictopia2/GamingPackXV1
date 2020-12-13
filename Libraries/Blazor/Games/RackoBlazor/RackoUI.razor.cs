using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
using CommonBasicStandardLibraries.CollectionClasses;
using Microsoft.AspNetCore.Components;
using RackoCP.Cards;
using RackoCP.Data;
using RackoCP.ViewModels;
using System;
using System.Linq;
using System.Threading.Tasks;
namespace RackoBlazor
{
    public partial class RackoUI
    {
        [CascadingParameter]
        public RackoMainViewModel? DataContext { get; set; }
        [CascadingParameter]
        public RackoGameContainer? GameContainer { get; set; }
        [CascadingParameter]
        public int TargetHeight { get; set; } = 15;
        private string RealHeight => $"{TargetHeight}vh";
        private string GetKey => Guid.NewGuid().ToString();
        private bool IsEnabled
        {
            get
            {
                if (DataContext!.CommandContainer.IsExecuting)
                {
                    return false;
                }
                return DataContext.CanPlayOnPile;
            }
        }
        private CustomBasicList<RackoCardInformation>? _cardList;
        private CustomBasicList<int>? _otherList;
        protected override void OnParametersSet()
        {
            var selfPlayer = GameContainer!.PlayerList!.GetSelf();
            _cardList = selfPlayer.MainHandList.ToCustomBasicList();
            _cardList.Reverse();
            int x;
            int starts = GameContainer.PlayerList.Count() + 2;
            int diffs = starts;
            _otherList = new CustomBasicList<int>();
            for (x = 1; x <= 10; x++)
            {
                _otherList.Add(starts);
                starts += diffs;
            }
            _otherList.Reverse();
            base.OnParametersSet();
        }
        private string GetLabel(RackoCardInformation card)
        {
            return _otherList![_cardList!.IndexOf(card)].ToString();
        }
        private async Task ClickedRowAsync(RackoCardInformation row)
        {
            if (IsEnabled == false)
            {
                return;
            }
            await DataContext!.CommandContainer.ProcessCustomCommandAsync(DataContext.PlayOnPileAsync, row);
        }
    }
}