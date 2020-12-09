using CommonBasicStandardLibraries.CollectionClasses;
using FroggiesCP.Data;
using FroggiesCP.ViewModels;
using System.Drawing;
namespace FroggiesBlazor.Views
{
    public partial class GameBoardBlazor
    {
        private static string MethodName => nameof(FroggiesMainViewModel.MakeMoveAsync);
        private CustomBasicList<LilyPadModel>? Lilies { get; set; }
        public static Point GetPoint(LilyPadModel model)
        {
            return new Point(model.Column * 64, model.Row * 64);
        }
        protected override void OnParametersSet()
        {
            Lilies = DataContext!.MainGame.GetCompleteLilyList();
            base.OnParametersSet();
        }
    }
}