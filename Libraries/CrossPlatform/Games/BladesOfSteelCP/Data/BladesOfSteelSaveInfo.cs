using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.MultiplayerClasses.SavedGameClasses;
using BasicGameFrameworkLibrary.RegularDeckOfCards;
namespace BladesOfSteelCP.Data
{
    [SingletonGame]
    public class BladesOfSteelSaveInfo : BasicSavedCardClass<BladesOfSteelPlayerItem, RegularSimpleCard>
    {
        private string _instructions = "";
        public string Instructions
        {
            get { return _instructions; }
            set
            {
                if (SetProperty(ref _instructions, value))
                {
                    if (_model == null)
                    {
                        return;
                    }
                    _model.Instructions = value;
                }
            }
        }
        private string _otherPlayer = "";
        public string OtherPlayer
        {
            get { return _otherPlayer; }
            set
            {
                if (SetProperty(ref _otherPlayer, value))
                {
                    if (_model == null)
                    {
                        return;
                    }
                    _model.OtherPlayer = value;
                }
            }
        }
        public bool IsFaceOff { get; set; }
        public bool WasTie { get; set; }
        private BladesOfSteelVMData? _model;
        internal void LoadMod(BladesOfSteelVMData model)
        {
            _model = model;
            _model.Instructions = Instructions;
            _model.OtherPlayer = OtherPlayer;
        }
    }
}