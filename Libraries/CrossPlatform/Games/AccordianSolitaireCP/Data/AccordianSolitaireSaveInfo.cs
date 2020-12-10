using AccordianSolitaireCP.EventModels;
using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.BasicDrawables.Dictionary;
using CommonBasicStandardLibraries.CollectionClasses;
using CommonBasicStandardLibraries.Exceptions;
using CommonBasicStandardLibraries.Messenging;
using CommonBasicStandardLibraries.MVVMFramework.ViewModels;
namespace AccordianSolitaireCP.Data
{
    [SingletonGame]
    public class AccordianSolitaireSaveInfo : ObservableObject, IMappable
    {
        public CustomBasicList<int> DeckList { get; set; } = new CustomBasicList<int>();
        public DeckRegularDict<AccordianSolitaireCardInfo> HandList = new DeckRegularDict<AccordianSolitaireCardInfo>();
        public int DeckSelected { get; set; }
        public int NewestOne { get; set; }
        private int _score;

        public int Score
        {
            get { return _score; }
            set
            {
                if (SetProperty(ref _score, value))
                {
                    if (_aggregator != null)
                    {
                        PublishScore();
                    }
                }

            }
        }
        private void PublishScore()
        {
            if (_aggregator == null)
            {
                throw new BasicBlankException("No event aggrevator was sent to publish score.  Rethink");
            }
            _aggregator.Publish(new ScoreEventModel(Score));
        }
        private IEventAggregator? _aggregator;
        public void LoadMod(IEventAggregator aggregator)
        {
            _aggregator = aggregator;
            PublishScore();
        }
    }
}