using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.MultiplayerClasses.Extensions;
using CommonBasicStandardLibraries.Exceptions;
using RageCardGameCP.Data;
using System.Threading.Tasks;
namespace RageCardGameCP.Logic
{
    [SingletonGame]
    [AutoReset]
    public class BidProcesses : IBidProcesses
    {
        public readonly RageCardGameGameContainer GameContainer;
        private readonly RageCardGameVMData _model;
        private readonly RageDelgates _delgates;
        public BidProcesses(RageCardGameGameContainer gameContainer, RageCardGameVMData model, RageDelgates delgates)
        {
            GameContainer = gameContainer;
            _model = model;
            _delgates = delgates;
        }
        public async Task LoadBiddingScreenAsync()
        {
            _model.BidAmount = -1;
            _model.Bid1!.LoadNormalNumberRangeValues(0, GameContainer!.SaveRoot!.CardsToPassOut);
            _model.Bid1.UnselectAll();
            if (_delgates.LoadBidScreenAsync == null)
            {
                throw new BasicBlankException("Nobody is loading the bidding screen.  Rethink");
            }
            await _delgates.LoadBidScreenAsync.Invoke();
        }
        public async Task ProcessBidAsync()
        {
            GameContainer.SingleInfo = GameContainer.PlayerList!.GetWhoPlayer();
            if (GameContainer.SingleInfo.CanSendMessage(GameContainer.BasicData!) == true)
            {
                await GameContainer.Network!.SendAllAsync("bid", _model!.BidAmount);
            }
            GameContainer.SingleInfo.BidAmount = _model!.BidAmount;
            if (_delgates.CloseBidScreenAsync == null)
            {
                throw new BasicBlankException("Nobody is closing bidding screen.  Rethink");
            }
            await _delgates.CloseBidScreenAsync.Invoke();
            await GameContainer.EndTurnAsync!.Invoke();
        }
    }
}