using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.Extensions;
using BasicGameFrameworkLibrary.NetworkingClasses.Extensions;
using BasicGameFrameworkLibrary.NetworkingClasses.Interfaces;
using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
using CommonBasicStandardLibraries.CollectionClasses;
using MancalaCP.Data;
using MancalaCP.Logic;
using System.Linq;
using System.Threading.Tasks;
namespace MancalaCP.ViewModels
{
    [SingletonGame]
    [AutoReset]
    public class GameBoardVM
    {
        public GameBoardVM(MancalaMainGameClass mainGame,
            CommandContainer command,
            BasicData basicData,
            GameBoardProcesses gameBoard1,
            MancalaVMData vMData
            )
        {
            _mainGame = mainGame;
            _command = command;
            _basicData = basicData;
            GameBoard1 = gameBoard1;
            _network = _basicData.GetNetwork();
            GameData = vMData;
        }
        private readonly INetworkMessages? _network;
        private readonly MancalaMainGameClass _mainGame;
        private readonly CommandContainer _command;
        private readonly BasicData _basicData;
        public GameBoardProcesses GameBoard1 { get; }
        public CustomBasicList<SpaceInfo> SpaceList => GameData.SpaceList.Values.ToCustomBasicList();
        public MancalaVMData GameData { get; }
        public int GetIndex(SpaceInfo space) => GameData.SpaceList.GetKey(space);
        public async Task MakeMoveAsync(SpaceInfo space)
        {
            int index = GameData.SpaceList.GetKey(space);
            await MakeMoveAsync(index);
        }
        private async Task MakeMoveAsync(int space)
        {
            if (_command.IsExecuting)
            {
                return; //has to do manually this time because of how it works.
            }
            _command.IsExecuting = true;
            if (_mainGame!.SingleInfo!.ObjectList.Any(x => x.Index == space) == false)
            {
                return;
            }
            _mainGame!.OpenMove();
            if (_basicData!.MultiPlayer == true)
            {
                await _network!.SendMoveAsync(space + 7); //because reversed.
            }
            await GameBoard1!.AnimateMoveAsync(space);
        }
    }
}