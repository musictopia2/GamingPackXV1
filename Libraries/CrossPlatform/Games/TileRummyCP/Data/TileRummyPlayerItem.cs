using BasicGameFrameworkLibrary.BasicDrawables.Dictionary;
using BasicGameFrameworkLibrary.BasicEventModels;
using BasicGameFrameworkLibrary.MultiplayerClasses.BasicPlayerClasses;
using CommonBasicStandardLibraries.Messenging;
namespace TileRummyCP.Data
{
    public class TileRummyPlayerItem : PlayerSingleHand<TileInfo>, IHandle<UpdateCountEventModel>
    {
        private DeckRegularDict<TileInfo> _additionalTileList = new DeckRegularDict<TileInfo>();
        public DeckRegularDict<TileInfo> AdditionalTileList
        {
            get { return _additionalTileList; }
            set
            {
                if (SetProperty(ref _additionalTileList, value))
                {
                    
                }
            }
        }
        private bool _initCompleted;
        public bool InitCompleted
        {
            get { return _initCompleted; }
            set
            {
                if (SetProperty(ref _initCompleted, value))
                {
                    
                }
            }
        }
        private int _score;
        public int Score
        {
            get { return _score; }
            set
            {
                if (SetProperty(ref _score, value))
                {
                    
                }
            }
        }
        public override int ObjectCount => MainHandList.Count + _tempCards;
        private int _tempCards;
        public void Handle(UpdateCountEventModel message)
        {
            _tempCards = message.ObjectCount;
        }
    }
}