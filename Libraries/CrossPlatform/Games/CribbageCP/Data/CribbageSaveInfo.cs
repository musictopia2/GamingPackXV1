using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.BasicDrawables.Dictionary;
using BasicGameFrameworkLibrary.DrawableListsObservable;
using BasicGameFrameworkLibrary.MultiplayerClasses.SavedGameClasses;
using CommonBasicStandardLibraries.Exceptions;
namespace CribbageCP.Data
{
    [SingletonGame]
    public class CribbageSaveInfo : BasicSavedCardClass<CribbagePlayerItem, CribbageCard>
    {
        public bool IsStart { get; set; }
        public bool StartOver { get; set; }
        public int NewTurn { get; set; }
        public int Dealer { get; set; }
        public bool IsCorrect { get; set; }
        public DeckRegularDict<CribbageCard> CribList { get; set; } = new DeckRegularDict<CribbageCard>();
        public DeckRegularDict<CribbageCard> MainList { get; set; } = new DeckRegularDict<CribbageCard>();
        public DeckRegularDict<CribbageCard> MainFrameList { get; set; } = new DeckRegularDict<CribbageCard>();
        private CribbageVMData? _model;
        private EnumGameStatus _whatStatus;
        public EnumGameStatus WhatStatus
        {
            get { return _whatStatus; }
            set
            {
                if (SetProperty(ref _whatStatus, value))
                {
                    //can decide what to do when property changes
                    ProcessModStatus();
                }
            }
        }
        private void ProcessModStatus()
        {
            if (_model == null)
            {
                return;
            }
            if (_model.CribFrame == null)
            {
                throw new BasicBlankException("I think the crib frame should have been created first");
            }
            _model.CribFrame.Visible = WhatStatus == EnumGameStatus.GetResultsCrib;
            if (WhatStatus == EnumGameStatus.CardsForCrib && _gameContainer!.PlayerList!.Count == 2)
            {
                _model.PlayerHand1!.AutoSelect = EnumHandAutoType.SelectAsMany;
            }
            else
            {
                _model.PlayerHand1!.AutoSelect = EnumHandAutoType.SelectOneOnly;
            }
        }
        private CribbageGameContainer? _gameContainer;
        public void LoadMod(CribbageVMData model, CribbageGameContainer gameContainer)
        {
            _gameContainer = gameContainer;
            _model = model;
            ProcessModStatus();
        }
    }
}