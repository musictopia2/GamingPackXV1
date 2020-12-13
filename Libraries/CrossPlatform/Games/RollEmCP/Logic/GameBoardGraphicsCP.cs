using BasicGameFrameworkLibrary.Attributes;
using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
using CommonBasicStandardLibraries.CollectionClasses;
using RollEmCP.Data;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
namespace RollEmCP.Logic
{
    [SingletonGame]
    [AutoReset]
    public class GameBoardGraphicsCP
    {
        private readonly RollEmGameContainer _gameContainer;
        public GameBoardGraphicsCP(RollEmGameContainer gameContainer)
        {
            _gameContainer = gameContainer;
            CreateSpaces();
        }
        public SizeF OriginalSize => new SizeF(230, 230);
        public SizeF SpaceSize => new SizeF(71, 51);
        public string FrameText => "Number List";
        public static void CreateNumberList(RollEmGameContainer gameContainer) //hopefully that is okay.
        {
            int x;
            int y;
            int z = 0;
            gameContainer.NumberList = new Dictionary<int, NumberInfo>();
            for (x = 1; x <= 4; x++)
            {
                for (y = 1; y <= 3; y++)
                {
                    z++;
                    var thisNumber = new NumberInfo();
                    thisNumber.Number = z;
                    gameContainer.NumberList.Add(z, thisNumber);
                }
            }
        }
        private void CreateSpaces()
        {
            if (_gameContainer.NumberList.Count == 0)
            {
                CreateNumberList(_gameContainer);
            }
            int x;
            int y;
            int z = 0;
            float tops = 13;
            float lefts;
            for (x = 1; x <= 4; x++)
            {
                lefts = 8;
                for (y = 1; y <= 3; y++)
                {
                    z += 1;
                    NumberInfo thisNumber = _gameContainer.NumberList![z];
                    thisNumber.Bounds = new RectangleF(lefts, tops, SpaceSize.Width, SpaceSize.Height);
                    thisNumber.Number = z;
                    lefts += SpaceSize.Width + 3;
                }
                tops += SpaceSize.Height + 3;
            }
        }
        public bool CanEnableMove
        {
            get
            {
                if (_gameContainer.SaveRoot.GameStatus == EnumStatusList.NeedRoll)
                {
                    return false;
                }
                return !_gameContainer.Command.IsExecuting;
            }
        }
        public async Task MakeMoveAsync(NumberInfo number)
        {
            await _gameContainer.ProcessCustomCommandAsync(_gameContainer.MakeMoveAsync!, _gameContainer.NumberList.GetKey(number));
        }
        public CustomBasicList<NumberInfo> GetNumberList => _gameContainer.NumberList.Values.ToCustomBasicList();
    }
}