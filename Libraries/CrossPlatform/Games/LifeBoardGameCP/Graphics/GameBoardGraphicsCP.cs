using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.GameboardPositionHelpers;
using BasicGameFrameworkLibrary.MultiplayerClasses.BasicPlayerClasses;
using BasicGameFrameworkLibrary.MultiplayerClasses.Extensions;
using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.FileFunctions;
using CommonBasicStandardLibraries.CollectionClasses;
using CommonBasicStandardLibraries.Exceptions;
using LifeBoardGameCP.Data;
using LifeBoardGameCP.Logic;
using Newtonsoft.Json;
using System.Drawing;
using System.Linq;
using System.Reflection;
using cc = CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.SColorString;
namespace LifeBoardGameCP.Graphics
{
    [SingletonGame]
    [AutoReset]
    public class GameBoardGraphicsCP
    {
        private readonly IBoardProcesses _options;
        private readonly LifeBoardGameGameContainer _gameContainer;
        private readonly ISpacePosition _spacePos;
        public static int GoingTo { get; set; } //was going to put into container but it fits here more as static one.
        //has to be public so blazor can use it.
        public SizeF OriginalSize => new SizeF(800, 800);
        public TempInfo TempData;
        public GameBoardGraphicsCP(IBoardProcesses options,
            LifeBoardGameGameContainer gameContainer,
            ISpacePosition spacePos)
        {
            _options = options;
            _gameContainer = gameContainer;
            _spacePos = spacePos;
            _gameContainer.ExtraSpace = new GameSpace();
            Assembly aa = Assembly.GetAssembly(GetType());
            var thisData = aa.ResourcesAllTextFromFile("lifeboardgame.json");
            TempData = JsonConvert.DeserializeObject<TempInfo>(thisData);
            CreateSpaces();
        }
        private RectangleF GetClickableRectangle(PositionInfo thisPos, bool isEnding)
        {
            var firstSize = new SizeF(10, 10);
            SizeF clickSize;
            if (isEnding == false)
            {
                clickSize = new SizeF(80, 80);
            }
            else
            {
                clickSize = new SizeF(140, 60);
            }
            var tempPoint = new PointF(thisPos.SpacePoint.X, thisPos.SpacePoint.Y);
            var thisPoint = new PointF(tempPoint.X - firstSize.Width, tempPoint.Y - firstSize.Height);
            return new RectangleF(thisPoint.X, thisPoint.Y, clickSize.Width, clickSize.Height);
        }
        private void CreateSpaces()
        {
            _gameContainer.CountrySpace = new GameSpace();
            _gameContainer.CountrySpace.Area = GetCountrySideRect();
            _gameContainer.MillionSpace = new GameSpace();
            _gameContainer.MillionSpace.Area = GetMillionRect();
        }
        private RectangleF GetCareerRectangle()
        {
            var thisPos = (from items in TempData.PositionList
                           where items.SpaceNumber == 201
                           select items).Single();
            return GetClickableRectangle(thisPos, false);
        }
        private RectangleF GetCollegeRectangle()
        {
            var thisPos = (from items in TempData.PositionList
                           where items.SpaceNumber == 202
                           select items).Single();
            return GetClickableRectangle(thisPos, false);
        }
        private RectangleF GetCountrySideRect()
        {
            var thisPos = (from items in TempData.PositionList
                           where items.SpaceNumber == 203
                           select items).Single();
            return GetClickableRectangle(thisPos, true);
        }
        private RectangleF GetMillionRect()
        {
            var thisPos = (from items in TempData.PositionList
                           where items.SpaceNumber == 204
                           select items).Single();
            return GetClickableRectangle(thisPos, true);
        }
        public CustomBasicList<EndPositionInfo> CountrysideAcresOptions()
        {
            if (_gameContainer.CurrentView != EnumViewCategory.EndGame)
            {
                return new CustomBasicList<EndPositionInfo>();
            }
            _gameContainer.Pos!.ClearArea(_gameContainer.CountrySpace!);
            _gameContainer.CountrySpace!.PieceList.Clear();
            RectangleF piece;
            SizeF size;
            PointF point;
            var tempList = (from items in _gameContainer.PlayerList
                            where items.LastMove == EnumFinal.CountrySideAcres
                            select items).ToCustomBasicList();
            CustomBasicList<EndPositionInfo> output = new CustomBasicList<EndPositionInfo>();
            foreach (var thisPlayer in tempList)
            {
                size = new SizeF(EndSize.Width, _gameContainer.CountrySpace.Area.Height);

                point = _gameContainer.Pos.GetPosition(_gameContainer.CountrySpace, size);
                piece = new RectangleF(point, size);
                EndPositionInfo end = new EndPositionInfo();
                end.Bounds = piece;
                end.Player = thisPlayer;
                output.Add(end);
                _gameContainer.CountrySpace.PieceList.Add(piece);
                _gameContainer.Pos.AddPieceToArea(_gameContainer.CountrySpace, piece);
            }
            return output;
        }
        public CustomBasicList<EndPositionInfo> MillionaireEstatesOptions()
        {
            if (_gameContainer.CurrentView != EnumViewCategory.EndGame)
            {
                return new CustomBasicList<EndPositionInfo>();
            }
            RectangleF piece;
            SizeF size;
            PointF point;
            CustomBasicList<EndPositionInfo> output = new CustomBasicList<EndPositionInfo>();
            _gameContainer.Pos.ClearArea(_gameContainer.MillionSpace!);
            _gameContainer.MillionSpace!.PieceList.Clear();
            var tempList = (from Items in _gameContainer.PlayerList
                            where Items.LastMove == EnumFinal.MillionaireEstates
                            select Items).ToCustomBasicList();
            foreach (var thisPlayer in tempList)
            {
                size = new SizeF(EndSize.Width, _gameContainer.MillionSpace.Area.Height);
                point = _gameContainer.Pos.GetPosition(_gameContainer.MillionSpace, size);
                piece = new RectangleF(point, size);
                _gameContainer.MillionSpace.PieceList.Add(piece);
                _gameContainer.Pos.AddPieceToArea(_gameContainer.MillionSpace, piece);
                EndPositionInfo end = new EndPositionInfo();
                end.Bounds = piece;
                end.Player = thisPlayer;
                output.Add(end);
            }
            return output;
        }
        private SizeF EndSize => new SizeF(34, 62);
        public void EndGameOptions()
        {
            if (_gameContainer.CurrentView != EnumViewCategory.EndGame)
            {
                throw new BasicBlankException("Needs to already be end game to use these options");
            }
            _gameContainer.Pos!.ClearArea(_gameContainer.CountrySpace!);
            _gameContainer.Pos.ClearArea(_gameContainer.MillionSpace!);
            _gameContainer.CountrySpace!.PieceList.Clear();
            _gameContainer.MillionSpace!.PieceList.Clear();
            RectangleF piece;
            SizeF size;
            PointF point;
            SizeF main = new SizeF(34, 62);
            var tempList = (from items in _gameContainer.PlayerList
                            where items.LastMove == EnumFinal.CountrySideAcres
                            select items).ToCustomBasicList();
            foreach (var thisPlayer in tempList)
            {
                size = new SizeF(main.Width, _gameContainer.CountrySpace.Area.Height);

                point = _gameContainer.Pos.GetPosition(_gameContainer.CountrySpace, size);
                piece = new RectangleF(point, size);
                _gameContainer.CountrySpace.PieceList.Add(piece);
                _gameContainer.Pos.AddPieceToArea(_gameContainer.CountrySpace, piece);

            }
            tempList = (from Items in _gameContainer.PlayerList
                        where Items.LastMove == EnumFinal.MillionaireEstates
                        select Items).ToCustomBasicList();
            foreach (var thisPlayer in tempList)
            {

                size = new SizeF(main.Width, _gameContainer.MillionSpace.Area.Height);
                point = _gameContainer.Pos.GetPosition(_gameContainer.MillionSpace, size);
                piece = new RectangleF(point, size);
                _gameContainer.MillionSpace.PieceList.Add(piece);
                _gameContainer.Pos.AddPieceToArea(_gameContainer.MillionSpace, piece);
            }
        }
        public CustomBasicList<ButtonInfo> GetMainActions()
        {
            if (_gameContainer.SingleInfo!.PlayerCategory != EnumPlayerCategory.Self)
            {
                return new CustomBasicList<ButtonInfo>();
            }
            RectangleF rect = GetExtraRectangle();
            _gameContainer.ExtraSpace!.Area = rect;
            _gameContainer.ExtraSpace.PieceList.Clear();
            ButtonInfo button;
            SizeF size = new SizeF(150, 50);
            PointF point;
            CustomBasicList<ButtonInfo> output = new CustomBasicList<ButtonInfo>();
            if (_options.CanPurchaseCarInsurance)
            {
                button = new ButtonInfo();
                button.Display = "Insure Car";
                button.Action = _options.PurchaseCarInsuranceAsync;
                button.Size = size;
                point = _gameContainer.Pos.GetPosition(_gameContainer.ExtraSpace!, size);
                button.Location = point;
                _gameContainer.ExtraSpace.PieceList.Add(new RectangleF(point, size));
                _gameContainer.Pos.AddPieceToArea(_gameContainer.ExtraSpace, new RectangleF(point, size));
                output.Add(button);
            }
            if (_options.CanPurchaseHouseInsurance)
            {
                button = new ButtonInfo();
                button.Display = "Insure House";
                button.Action = _options.PurchaseHouseInsuranceAsync;
                button.Size = size;
                point = _gameContainer.Pos.GetPosition(_gameContainer.ExtraSpace, size);
                button.Location = point;
                _gameContainer.ExtraSpace.PieceList.Add(new RectangleF(point, size));
                _gameContainer.Pos.AddPieceToArea(_gameContainer.ExtraSpace, new RectangleF(point, size));
                output.Add(button);
            }
            if (_options.CanPurchaseStock)
            {
                button = new ButtonInfo();
                button.Display = "Buy Stock";
                button.Action = _options.PurchaseStockAsync;
                button.Size = size;
                point = _gameContainer.Pos.GetPosition(_gameContainer.ExtraSpace, size);
                button.Location = point;
                _gameContainer.ExtraSpace.PieceList.Add(new RectangleF(point, size));
                _gameContainer.Pos.AddPieceToArea(_gameContainer.ExtraSpace, new RectangleF(point, size));
                output.Add(button);
            }
            if (_options.CanSellHouse)
            {
                button = new ButtonInfo();
                button.Display = "Sell House";
                button.Action = _options.SellHouseAsync;
                button.Size = size;
                point = _gameContainer.Pos.GetPosition(_gameContainer.ExtraSpace, size);
                button.Location = point;
                _gameContainer.ExtraSpace.PieceList.Add(new RectangleF(point, size));
                _gameContainer.Pos.AddPieceToArea(_gameContainer.ExtraSpace, new RectangleF(point, size));
                output.Add(button);
            }
            if (_options.CanAttendNightSchool)
            {
                button = new ButtonInfo();
                button.Display = "Night School";
                button.Action = _options.AttendNightSchoolAsync;
                button.Size = size;
                point = _gameContainer.Pos.GetPosition(_gameContainer.ExtraSpace, size);
                button.Location = point;
                _gameContainer.ExtraSpace.PieceList.Add(new RectangleF(point, size));
                _gameContainer.Pos.AddPieceToArea(_gameContainer.ExtraSpace, new RectangleF(point, size));
                output.Add(button);
            }
            if (_options.CanTrade4Tiles)
            {
                button = new ButtonInfo();
                button.Display = "Trade 4 Tiles";
                button.Action = _options.Trade4TilesAsync;
                button.Size = size;
                point = _gameContainer.Pos.GetPosition(_gameContainer.ExtraSpace, size);
                button.Location = point;
                _gameContainer.ExtraSpace.PieceList.Add(new RectangleF(point, size));
                _gameContainer.Pos.AddPieceToArea(_gameContainer.ExtraSpace, new RectangleF(point, size));
                output.Add(button);
            }
            if (_gameContainer.GameStatus == EnumWhatStatus.NeedToChooseSpace)
            {
                button = new ButtonInfo();
                button.Display = "Submit Space";
                button.Action = _options.HumanChoseSpaceAsync;
                button.Size = size;
                point = _gameContainer.Pos.GetPosition(_gameContainer.ExtraSpace, size);
                button.Location = point;
                _gameContainer.ExtraSpace.PieceList.Add(new RectangleF(point, size));
                _gameContainer.Pos.AddPieceToArea(_gameContainer.ExtraSpace, new RectangleF(point, size));
                output.Add(button);
            }
            if (_options.CanEndTurn)
            {
                button = new ButtonInfo();
                button.Display = "End Turn";
                button.Action = _gameContainer.UIEndTurnAsync;
                button.Size = size;
                point = _gameContainer.Pos.GetPosition(_gameContainer.ExtraSpace, size);
                button.Location = point;
                _gameContainer.ExtraSpace.PieceList.Add(new RectangleF(point, size));
                _gameContainer.Pos.AddPieceToArea(_gameContainer.ExtraSpace, new RectangleF(point, size));
                output.Add(button);
            }
            return output;
        }
        public CustomBasicList<ChooseSpaceInfo> GetSpaceChoices(PointF currentPoint)
        {
            if (_gameContainer.GameStatus != EnumWhatStatus.NeedToChooseSpace)
            {
                return new CustomBasicList<ChooseSpaceInfo>();
            }
            var firstPos = _spacePos.FirstPossiblePosition;
            var secondPos = _spacePos.SecondPossiblePosition;
            CustomBasicList<ChooseSpaceInfo> output = new CustomBasicList<ChooseSpaceInfo>();
            ChooseSpaceInfo space = new ChooseSpaceInfo();
            if (firstPos == _gameContainer.CurrentSelected)
            {
                space.Color = cc.Aqua;
            }
            else
            {
                space.Color = cc.Black;
            }
            var thisPos = (from Items in TempData.PositionList
                           where Items.PointView == currentPoint && Items.SpaceNumber == firstPos
                           select Items).Single();
            space.Bounds = GetClickableRectangle(thisPos, false);
            space.Space = firstPos;
            output.Add(space);
            space = new ChooseSpaceInfo();
            if (secondPos == _gameContainer.CurrentSelected)
            {
                space.Color = cc.Aqua;
            }
            else
            {
                space.Color = cc.Black;
            }
            thisPos = (from Items in TempData.PositionList
                       where Items.PointView == currentPoint && Items.SpaceNumber == secondPos
                       select Items).Single();
            space.Bounds = GetClickableRectangle(thisPos, false);
            space.Space = secondPos;
            output.Add(space);
            return output;
        }
        private RectangleF GetExtraRectangle()
        {
            int index;
            index = 300 + (int)_gameContainer.CurrentView;
            var thisPos = (from items in TempData.PositionList
                           where items.SpaceNumber == index
                           select items).Single();
            var firstPoint = new PointF(thisPos.SpacePoint.X, thisPos.SpacePoint.Y); // i think
            var thisSize = new SizeF(400, 300); // i think
            return new RectangleF(firstPoint.X, firstPoint.Y, thisSize.Width, thisSize.Height);
        }
        public CustomBasicList<RectangleF> GoingToProcesses(PointF currentPoint)
        {
            if (GoingTo == 0)
            {
                return new CustomBasicList<RectangleF>();
            }
            var thisPos = (from Items in TempData.PositionList
                           where Items.PointView == currentPoint && Items.SpaceNumber == GoingTo
                           select Items).SingleOrDefault();
            if (thisPos == null)
            {
                return new CustomBasicList<RectangleF>();
            }
            return new CustomBasicList<RectangleF>() { GetClickableRectangle(thisPos, false) };
        }
        public CustomBasicList<StartClickInfo> GetFirstOptions()
        {
            if (_gameContainer.GameStatus != EnumWhatStatus.NeedChooseFirstOption)
            {
                return new CustomBasicList<StartClickInfo>();
            }
            var firstRect = GetCareerRectangle();
            var secondRect = GetCollegeRectangle();
            return new CustomBasicList<StartClickInfo>() { new StartClickInfo(firstRect, EnumStart.Career),
                new StartClickInfo(secondRect, EnumStart.College)
                 };
        }
        public CustomBasicList<RetirementClickInfo> GetRetirementOptions()
        {
            if (_gameContainer.GameStatus != EnumWhatStatus.NeedChooseRetirement)
            {
                return new CustomBasicList<RetirementClickInfo>();
            }
            var firstRect = GetCountrySideRect();
            var secondRect = GetMillionRect();
            return new CustomBasicList<RetirementClickInfo>() { 
            new RetirementClickInfo(firstRect, EnumFinal.CountrySideAcres),
            new RetirementClickInfo(secondRect, EnumFinal.MillionaireEstates)
            };
        }
    }
}