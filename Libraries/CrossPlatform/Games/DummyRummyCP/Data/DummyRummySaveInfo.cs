using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.MultiplayerClasses.SavedGameClasses;
using BasicGameFrameworkLibrary.RegularDeckOfCards;
using CommonBasicStandardLibraries.CollectionClasses;
namespace DummyRummyCP.Data
{
    [SingletonGame]
    public class DummyRummySaveInfo : BasicSavedCardClass<DummyRummyPlayerItem, RegularRummyCard>
    {
        private int _upTo;
        public int UpTo
        {
            get { return _upTo; }
            set
            {
                if (SetProperty(ref _upTo, value))
                {
                    if (_model == null)
                    {
                        return;
                    }
                    _model.UpTo = UpTo;
                }
            }
        }
        private DummyRummyVMData? _model;
        internal void LoadMod(DummyRummyVMData model)
        {
            _model = model;
            _model.UpTo = UpTo;
        }
        public int PlayerWentOut { get; set; }
        public bool SetsCreated { get; set; }
        public int PointsObtained { get; set; }
        public CustomBasicList<SavedSet> SetList { get; set; } = new CustomBasicList<SavedSet>();
    }
}