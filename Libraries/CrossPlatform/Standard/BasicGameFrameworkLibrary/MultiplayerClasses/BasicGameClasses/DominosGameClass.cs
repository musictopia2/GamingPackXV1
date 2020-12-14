using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGameFrameworkLibrary.DIContainers;
using BasicGameFrameworkLibrary.Dominos;
using BasicGameFrameworkLibrary.MultiplayerClasses.BasicPlayerClasses;
using BasicGameFrameworkLibrary.MultiplayerClasses.Extensions;
using BasicGameFrameworkLibrary.MultiplayerClasses.GameContainers;
using BasicGameFrameworkLibrary.MultiplayerClasses.InterfaceMessages;
using BasicGameFrameworkLibrary.MultiplayerClasses.InterfaceModels;
using BasicGameFrameworkLibrary.MultiplayerClasses.InterfacesForHelpers;
using BasicGameFrameworkLibrary.MultiplayerClasses.SavedGameClasses;
using BasicGameFrameworkLibrary.NetworkingClasses.Extensions;
using BasicGameFrameworkLibrary.TestUtilities;
using CommonBasicStandardLibraries.Exceptions;
using CommonBasicStandardLibraries.Messenging;
using System.Linq;
using System.Threading.Tasks; //most of the time, i will be using asyncs.
namespace BasicGameFrameworkLibrary.MultiplayerClasses.BasicGameClasses
{
    public abstract class DominosGameClass<D, P, S> : BasicGameClass<P, S>
        , IDrewDominoNM, IPlayDominoNM, IDominoDrawProcesses<D>
        where D : IDominoInfo, new()
        where P : class, IPlayerSingleHand<D>, new()
        where S : BasicSavedDominosClass<D, P>, new()
    {
        public DominosGameClass(
            IGamePackageResolver mainContainer,
            IEventAggregator aggregator,
            BasicData basicData,
            TestOptions test,
            IDominoGamesData<D> currentMod,
            IMultiplayerSaveState state,
            IAsyncDelayer delay,
            CommandContainer command,
            BasicGameContainer<P, S> gameContainer
            ) : base(mainContainer, aggregator, basicData, test, currentMod, state, delay, command, gameContainer)
        {
            _model = currentMod;
            _command = command;
        }
        private readonly IDominoGamesData<D> _model;
        private readonly CommandContainer _command;
        public int DominosToPassOut { get; set; } //i think this is fine.
        protected void LoadUpDominos()
        {
            if (IsLoaded == true)
            {
                throw new BasicBlankException("Should not load the dominos if its already loaded.  Otherwise, rethink");
            }
            _model.PlayerHand1!.Text = "Your Dominos";
            _model.PlayerHand1.Visible = true;
        }
        protected void ClearBoneYard()
        {
            _model.BoneYard!.PopulateBoard();//i think.
        }
        protected void PassDominos()
        {
            if (_model.BoneYard!.RemainingList.Count() == 0)
            {
                throw new BasicBlankException("Cannot have 0 dominos after shuffling");
            }
            PlayerList!.ForEach(ThisPlayer =>
            {
                ThisPlayer.MainHandList.ReplaceRange(_model.BoneYard.FirstDraw(DominosToPassOut));
                if (ThisPlayer.MainHandList.Count() == 0)
                {
                    throw new BasicBlankException("Cannot have 0 dominos when passing out");
                }
            });
            AfterPassedDominos();
        }
        protected void AfterPassedDominos()
        {
            SingleInfo = PlayerList.GetSelf();
            _model.PlayerHand1!.HandList = SingleInfo.MainHandList; //forgot to hook up here.
            SingleInfo.MainHandList.Sort(); //i think.
            _model.BoneYard!.PopulateTotals();
        }
        public async Task DrawDominoAsync(D thisDomino)
        {
            if (SingleInfo!.CanSendMessage(BasicData!) == true)
            {
                await Network!.SendAllAsync("drawdomino", thisDomino.Deck);
            }
            _model.BoneYard!.RemoveDomino(thisDomino);
            _model.BoneYard.PopulateTotals();
            thisDomino.IsUnknown = false;
            thisDomino.Drew = true;
            SingleInfo!.MainHandList.Add(thisDomino);
            if (SingleInfo.PlayerCategory == EnumPlayerCategory.Self)
            {
                SingleInfo.MainHandList.Sort();
            }
            await AfterDrawingDomino();
        }
        protected virtual async Task AfterDrawingDomino()
        {
            await ContinueTurnAsync();
        }
        public async Task DrewDominoReceivedAsync(int deck)
        {
            D thisDomino = _model.BoneYard!.RemainingList.GetSpecificItem(deck);
            await DrawDominoAsync(thisDomino);
        }
        protected void ProtectedStartTurn()
        {
            _model.BoneYard!.NewTurn(); //i think
            this.ShowTurn();
        }
        protected void ProtectedSaveBone()
        {
            SaveRoot!.BoneYardData = _model.BoneYard!.SavedData();
        }
        protected void ProtectedLoadBone()
        {
            _model.BoneYard!.SavedGame(SaveRoot!.BoneYardData!);
        }
        protected async Task SendPlayDominoAsync(int deck)
        {
            if (SingleInfo!.CanSendMessage(BasicData!) == false)
                return;
            await Network!.SendPlayDominoAsync(deck);
        }
        public abstract Task PlayDominoAsync(int deck); //every game like this requires playing domino.  if i am wrong, rethink
        async Task IPlayDominoNM.PlayDominoMessageAsync(int deck)
        {
            await PlayDominoAsync(deck);
        }
        //looks like i have to attempt to not even have this since its not working anyways and nothing even used it.
        //protected virtual void ReportCanExecuteChanged()
        //{
        //    if (SingleInfo!.PlayerCategory != EnumPlayerCategory.Self)
        //    {
        //        return;
        //    }
        //    _command.ManualReport(); //try this way.
        //}
    }
}