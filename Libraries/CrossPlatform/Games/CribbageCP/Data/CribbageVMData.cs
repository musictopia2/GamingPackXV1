using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.DrawableListsObservable;
using BasicGameFrameworkLibrary.MultiplayerClasses.InterfaceModels;
using CommonBasicStandardLibraries.MVVMFramework.ViewModels;
using CribbageCP.Logic;
namespace CribbageCP.Data
{

    [SingletonGame]
    [AutoReset]
    public class CribbageVMData : ObservableObject, IBasicCardGamesData<CribbageCard>, IBasicEnableProcess
    {
        public CribbageVMData(CommandContainer command, HiddenBoard board)
        {
            Deck1 = new DeckObservablePile<CribbageCard>(command);
            Pile1 = new SingleObservablePile<CribbageCard>(command);
            PlayerHand1 = new HandObservable<CribbageCard>(command);
            MainFrame = new HandObservable<CribbageCard>(command);
            CribFrame = new HandObservable<CribbageCard>(command);
            CribFrame.Visible = false;
            MainFrame.Text = "Card List";
            CribFrame.Text = "Crib";
            MainFrame.SendEnableProcesses(this, () => false);
            CribFrame.SendEnableProcesses(this, () => false);
            GameBoard1 = board;
            ScoreBoard1 = new ScoreBoardCP();

        }
        public ScoreBoardCP ScoreBoard1;
        public HandObservable<CribbageCard> CribFrame;
        public HandObservable<CribbageCard> MainFrame;
        public HiddenBoard GameBoard1;
        public DeckObservablePile<CribbageCard> Deck1 { get; set; }
        public SingleObservablePile<CribbageCard> Pile1 { get; set; }
        public HandObservable<CribbageCard> PlayerHand1 { get; set; }
        public SingleObservablePile<CribbageCard>? OtherPile { get; set; }

        private string _normalTurn = "";
        [VM]
        public string NormalTurn
        {
            get { return _normalTurn; }
            set
            {
                if (SetProperty(ref _normalTurn, value))
                {
                    
                }
            }
        }

        private string _status = "";
        [VM] //use this tag to transfer to the actual view model.  this is being done to avoid overflow errors.
        public string Status
        {
            get { return _status; }
            set
            {
                if (SetProperty(ref _status, value))
                {
                    
                }
            }
        }
        private int _totalScore;
        [VM]
        public int TotalScore
        {
            get { return _totalScore; }
            set
            {
                if (SetProperty(ref _totalScore, value))
                {
                    
                }
            }
        }
        private int _totalCount;
        [VM]
        public int TotalCount
        {
            get { return _totalCount; }
            set
            {
                if (SetProperty(ref _totalCount, value))
                {
                    
                }
            }
        }
        private string _dealer = "";
        [VM]
        public string Dealer
        {
            get { return _dealer; }
            set
            {
                if (SetProperty(ref _dealer, value))
                {
                    
                }
            }
        }
        bool IBasicEnableProcess.CanEnableBasics()
        {
            return true;
        }
    }
}