using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.MahjongTileClasses;
using CommonBasicStandardLibraries.CollectionClasses;
using MahJongSolitaireCP.Data;
using MahJongSolitaireCP.ViewModels;
using Microsoft.AspNetCore.Components;
namespace MahJongSolitaireBlazor.Views
{

    internal struct TileGame
    {
        public int Deck { get; set; }
        public int GameNumber { get; set; }
    }

    public partial class GameBoardBlazor
    {
        //private MahjongSolitaireTileInfo? CurrentTile { get; set; }


        [Parameter]
        public CustomBasicList<BoardInfo> BoardList { get; set; } = new CustomBasicList<BoardInfo>();
        private string MethodName => nameof(MahJongSolitaireMainViewModel.SelectTileAsync);

        private TileGame GetTileKey(MahjongSolitaireTileInfo tile)
        {
            return new TileGame()
            {
                Deck = tile.Deck,
                GameNumber = MahJongSolitaireMainViewModel.GameDrawing
            };
        }

        private string GetGameKey => $"MahjongGame{MahJongSolitaireMainViewModel.GameDrawing}";

    }
}