using CommonBasicStandardLibraries.CollectionClasses;
using MahJongSolitaireCP.Data;
using MahJongSolitaireCP.ViewModels;
namespace MahJongSolitaireBlazor.Views
{
    public partial class MahJongSolitaireMainView
    {
        private CustomBasicList<BoardInfo>? BoardList { get; set; }
        protected override void OnParametersSet()
        {
            BoardList = DataContext!.MainGame.GameBoard1.GetPriorityBoards();
            base.OnParametersSet();
        }
        private string UndoMethod => nameof(MahJongSolitaireMainViewModel.UndoMoveAsync);
        private string HintMethod => nameof(MahJongSolitaireMainViewModel.GetHint);
    }
}