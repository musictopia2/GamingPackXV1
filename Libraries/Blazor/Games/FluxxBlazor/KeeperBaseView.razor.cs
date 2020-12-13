using CommonBasicStandardLibraries.CollectionClasses;
using FluxxCP.Data;
using FluxxCP.ViewModels;
using Microsoft.AspNetCore.Components;
using System;
using System.Linq;
using System.Threading.Tasks;
using aa = BasicBlazorLibrary.Components.CssGrids.Helpers;
namespace FluxxBlazor
{
    public abstract partial class KeeperBaseView<K> : IDisposable
        where K: class
    {
        protected override Task OnInitializedAsync()
        {
            CompleteContainer!.GameContainer.Command.AddAction(ShowChange);
            return base.OnInitializedAsync();
        }
        private void ShowChange()
        {
            InvokeAsync(() =>
            {
                StateHasChanged();
            });
        }

        [CascadingParameter]
        public CompleteContainerClass? CompleteContainer { get; set; }
        [CascadingParameter]
        public K? DataContext { get; set; }
        private string GetRowsText
        {
            get
            {
                return aa.RepeatAuto(BottomRow + 1);
                
            }
        }
        private int BottomRow
        {
            get
            {
                if (CompleteContainer!.GameContainer.PlayerList!.Count() <= 3)
                {
                    return 3;
                }
                else
                {
                    return 4;
                }
            }
        }
        private string GetColumnsText
        {
            get
            {
                if (CompleteContainer!.GameContainer.PlayerList!.Count() == 2 || CompleteContainer.GameContainer.PlayerList!.Count == 4)
                {
                    return "50vw 50vw";
                }
                else
                {
                    return "33vw 33vw 33vw";
                }
            }
        }
        private CustomBasicList<FluxxPlayerItem> _players = new CustomBasicList<FluxxPlayerItem>();
        protected override void OnInitialized()
        {
            _players = CompleteContainer!.GameContainer.PlayerList!.GetAllPlayersStartingWithSelf();
            base.OnInitialized();
        }
        void IDisposable.Dispose()
        {
            CompleteContainer!.GameContainer.Command.RemoveAction(ShowChange);
        }
        private string CloseMethod => nameof(KeeperShowViewModel.CloseKeeperAsync);
        protected virtual string CommandText => "";
        protected abstract EnumKeeperCategory KeeperCategory { get; }
    }
}