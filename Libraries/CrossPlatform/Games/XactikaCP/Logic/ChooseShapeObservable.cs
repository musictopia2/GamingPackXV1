using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.MiscProcesses;
using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
using CommonBasicStandardLibraries.CollectionClasses;
using System.Linq;
using System.Reflection;
using XactikaCP.Cards;
using XactikaCP.Data;
namespace XactikaCP.Logic
{
    public class ChooseShapeObservable : SimpleControlObservable
    {
        private readonly XactikaGameContainer _gameContainer;
        public ChooseShapeObservable(XactikaGameContainer gameContainer) : base(gameContainer.Command)
        {
            MethodInfo method = this.GetPrivateMethod(nameof(ProcessPieceSelected));
            ShapeSelectedCommand = new ControlCommand(this, method, gameContainer.Command);
            _gameContainer = gameContainer;
        }

        private EnumShapes _shapeChosen = EnumShapes.None;
        public EnumShapes ShapeChosen
        {
            get
            {
                return _shapeChosen;
            }
            set
            {
                if (SetProperty(ref _shapeChosen, value) == true)
                {
                    _gameContainer.SaveRoot!.ShapeChosen = value;
                }
            }
        }
        private int _howMany = 0;
        public int HowMany
        {
            get
            {
                return _howMany;
            }
            set
            {
                if (SetProperty(ref _howMany, value) == true)
                {
                }
            }
        }
        public ControlCommand ShapeSelectedCommand { get; set; }
        public CustomBasicList<PieceModel> PieceList { get; set; } = new CustomBasicList<PieceModel>();
        private void ProcessPieceSelected(PieceModel piece)
        {
            PieceList.ForEach(tempPiece =>
            {
                if (tempPiece.Equals(piece))
                {
                    tempPiece.IsSelected = true;
                }
                else
                {
                    tempPiece.IsSelected = false;
                }
            });
            HowMany = piece.HowMany;
            ShapeChosen = piece.ShapeUsed;
        }
        protected override void EnableChange()
        {
            ShapeSelectedCommand.ReportCanExecuteChange();
        }
        protected override void PrivateEnableAlways() { }
        private bool _visible;
        public bool Visible
        {
            get { return _visible; }
            set
            {
                if (SetProperty(ref _visible, value))
                {
                    
                }
            }
        }
        public void ChoosePiece(EnumShapes shape)
        {
            if (PieceList.Count > 0)
            {
                var piece = (from x in PieceList
                             where (int)x.ShapeUsed == (int)shape
                             select x).Single();
                piece.IsSelected = false;
                PieceList.ReplaceAllWithGivenItem(piece);
            }
            else
            {
                PieceModel newPiece = new PieceModel();
                newPiece.ShapeUsed = shape;
                newPiece.HowMany = _gameContainer.SaveRoot!.Value;
                PieceList.ReplaceAllWithGivenItem(newPiece);
            }
            Visible = true;
        }
        public void Reset()
        {
            ShapeChosen = EnumShapes.None;
            HowMany = 0;
            _gameContainer.SaveRoot!.ShapeChosen = EnumShapes.None;
            Visible = false;
        }
        public void LoadPieceList(XactikaCardInformation card)
        {
            CustomBasicCollection<PieceModel> tempList = new CustomBasicCollection<PieceModel>();
            PieceModel thisPiece = new PieceModel();
            thisPiece.HowMany = card.HowManyBalls;
            thisPiece.ShapeUsed = EnumShapes.Balls;
            tempList.Add(thisPiece);
            thisPiece = new PieceModel();
            thisPiece.HowMany = card.HowManyCones;
            thisPiece.ShapeUsed = EnumShapes.Cones;
            tempList.Add(thisPiece);
            thisPiece = new PieceModel();
            thisPiece.HowMany = card.HowManyCubes;
            thisPiece.ShapeUsed = EnumShapes.Cubes;
            tempList.Add(thisPiece);
            thisPiece = new PieceModel();
            thisPiece.HowMany = card.HowManyStars;
            thisPiece.ShapeUsed = EnumShapes.Stars;
            tempList.Add(thisPiece);
            PieceList.ReplaceRange(tempList);
            Visible = true;
        }
    }
}