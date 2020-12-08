using BasicGameFrameworkLibrary.BasicDrawables.Dictionary;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.DIContainers;
using BasicGameFrameworkLibrary.DrawableListsObservable;
using BasicGameFrameworkLibrary.MultiplayerClasses.InterfaceModels;
using CommonBasicStandardLibraries.Exceptions;
using System.Linq;
using System.Threading.Tasks; //most of the time, i will be using asyncs.
namespace BasicGameFrameworkLibrary.Dominos
{
    public class DominosBoneYardClass<D> : ScatteringPiecesObservable<D, DominosBasicShuffler<D>>
        where D : IDominoInfo, new()
    {
        private readonly IDominoGamesData<D> _model;

        public DominosBoneYardClass(IDominoGamesData<D> model,
            CommandContainer command,
            IGamePackageResolver resolver,
            DominosBasicShuffler<D> shuffles
            ) : base(command, resolver)
        {
            ProtectedText = "Bone Yard";
            ObjectList = shuffles;
            _model = model;
        }
        protected override async Task ClickedBoardAsync()
        {
            int deck = DrawPiece();
            if (_model.DrewDominoAsync == null)
            {
                throw new BasicBlankException("The drew domino was never populated.  Rethink");
            }
            await _model.DrewDominoAsync.Invoke(RemainingList.GetSpecificItem(deck));
        }
        public D FindDoubleDomino(int whichOne)
        {
            var output = RemainingList.Single(Items => Items.FirstNum == whichOne && Items.SecondNum == whichOne);
            RemoveDomino(output);
            output.IsUnknown = false;
            return output;
        }
        public void RemoveDomino(D domino)
        {
            RemoveSinglePiece(domino.Deck);
        }
        public DeckRegularDict<D> FirstDraw(int howMany)
        {
            GetFirstPieces(howMany, out DeckRegularDict<D> output);
            if (output.Count == 0)
            {
                throw new BasicBlankException("Cannot draw 0 dominos to begin with");
            }
            return output;
        }
        public void EmptyBones()
        {
            EmptyBoard();
        }
        public D DrawDomino()
        {
            int Decks = DrawPiece();
            return RemainingList.GetSpecificItem(Decks);
        }
        public bool HasBone()
        {
            return HasPieces();
        }
        protected override async Task ClickedPieceAsync(int deck)
        {
            if (_model.DrewDominoAsync == null)
            {
                throw new BasicBlankException("The drew domino was never populated.  Rethink");
            }
            await _model.DrewDominoAsync.Invoke(RemainingList.GetSpecificItem(deck)); //i think
        }
        protected override void PrivateEnableAlways() { }
    }
}