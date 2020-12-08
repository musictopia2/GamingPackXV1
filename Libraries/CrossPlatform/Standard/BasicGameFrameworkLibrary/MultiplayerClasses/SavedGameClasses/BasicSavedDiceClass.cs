using BasicGameFrameworkLibrary.Dice;
using BasicGameFrameworkLibrary.MultiplayerClasses.BasicPlayerClasses;
using BasicGameFrameworkLibrary.MultiplayerClasses.InterfaceModels;
namespace BasicGameFrameworkLibrary.MultiplayerClasses.SavedGameClasses
{
    public class BasicSavedDiceClass<D, P> : BasicSavedGameClass<P>, ISavedDiceList<D>
        where D : IStandardDice, new()
        where P : class, IPlayerItem, new()
    {
        public DiceList<D> DiceList { get; set; } = new DiceList<D>();
        private int _rollNumber;
        public int RollNumber
        {
            get { return _rollNumber; }
            set
            {
                if (SetProperty(ref _rollNumber, value))
                {
                    if (_model == null)
                    {
                        return;
                    }
                    _model.RollNumber = value;
                }
            }
        }

        private IBasicDiceGamesData<D>? _model;
        internal void LoadMod(IBasicDiceGamesData<D> model)
        {
            _model = model;
            _model.RollNumber = RollNumber;
        }
    }
}