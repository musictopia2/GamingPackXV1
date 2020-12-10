using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.MultiplePilesObservable;
using CommonBasicStandardLibraries.CollectionClasses;
using CommonBasicStandardLibraries.Messenging;
using CommonBasicStandardLibraries.MVVMFramework.ViewModels;
using HeapSolitaireCP.EventModels;
namespace HeapSolitaireCP.Data
{
    [SingletonGame]
    public class HeapSolitaireSaveInfo : ObservableObject, IMappable
    {
        public CustomBasicList<int> DeckList { get; set; } = new CustomBasicList<int>();
        public int PreviousSelected { get; set; } = -1; //has to show at first that none is selected.
        private int _score;
        public int Score
        {
            get { return _score; }
            set
            {
                if (SetProperty(ref _score, value))
                {
                    Publish();
                }

            }
        }
        public CustomBasicList<BasicPileInfo<HeapSolitaireCardInfo>>? WasteData { get; set; }
        public CustomBasicList<BasicPileInfo<HeapSolitaireCardInfo>>? MainPiles { get; set; }
        private IEventAggregator? _aggregator;
        private void Publish()
        {
            if (_aggregator == null)
            {
                return;
            }
            _aggregator.Publish(new ScoreEventModel(Score));
        }
        public void Load(IEventAggregator aggregator)
        {
            _aggregator = aggregator;
            Publish();
        }
    }
}