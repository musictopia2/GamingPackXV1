using BasicGameFrameworkLibrary.GameBoardCollections;
using BasicGamingUIBlazorLibrary.BasicControls.GameBoards;
using SolitaireBoardGameCP.Data;
namespace SolitaireBoardGameBlazor.Views
{
    public class SolitaireGameBoard : GridGameBoard<GameSpace>
    {
        protected override bool CanAddControl(IBoardCollection<GameSpace> itemsSource, int row, int column)
        {
            //return false; //for testing.
            if (column >= 3 && column <= 5)
                return true;
            if (row >= 3 && row <= 5)
                return true;
            return false;
        }
    }
}