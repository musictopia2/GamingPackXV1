using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.SolitaireClasses.ClockClasses;
using BasicGameFrameworkLibrary.SolitaireClasses.DataClasses;
using CommonBasicStandardLibraries.Exceptions;
using CommonBasicStandardLibraries.Messenging;
using CommonBasicStandardLibraries.MVVMFramework.ViewModels;
using System.Threading.Tasks; //most of the time, i will be using asyncs.
namespace GrandfathersClockCP.Data
{
    [SingletonGame]
    public class ScoreModel : ObservableObject, IScoreData, IClockVM
    {
        private int _score;
        private readonly IEventAggregator _aggregator;

        public int Score
        {
            get { return _score; }
            set
            {
                if (SetProperty(ref _score, value))
                {
                    //can decide what to do when property changes
                    _aggregator.Publish(this);
                }
            }
        }
        public ScoreModel(IEventAggregator aggregator)
        {
            _aggregator = aggregator;
        }
        Task IClockVM.ClockClickedAsync(int index)
        {
            throw new BasicBlankException("Rethinking may be necessary");
        }
    }
}