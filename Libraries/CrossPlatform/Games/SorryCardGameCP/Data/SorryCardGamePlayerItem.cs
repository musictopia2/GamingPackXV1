using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.MultiplayerClasses.BasicPlayerClasses;
using CommonBasicStandardLibraries.Exceptions;
using SorryCardGameCP.Cards;
using SorryCardGameCP.Logic;
using System.Threading.Tasks;
namespace SorryCardGameCP.Data
{
    public class SorryCardGamePlayerItem : PlayerSingleHand<SorryCardGameCardInformation>, IPlayerBoardGame<EnumColorChoices>
    {
        private EnumColorChoices _color = EnumColorChoices.None;
        public EnumColorChoices Color
        {
            get { return _color; }
            set
            {
                if (SetProperty(ref _color, value))
                {
                    
                }
            }
        }
        public bool DidChooseColor => Color != EnumColorChoices.None;
        public void Clear()
        {
            Color = EnumColorChoices.None;
        }
        public bool OtherTurn { get; set; }
        private int _howManyAtHome;
        public int HowManyAtHome
        {
            get { return _howManyAtHome; }
            set
            {
                if (SetProperty(ref _howManyAtHome, value))
                {
                    if (value > 4)
                    {
                        throw new BasicBlankException("There can't ever be more than 4 at home.");
                    }
                    if (value < 0)
                    {
                        throw new BasicBlankException("There can't ever be less than 0 at home.");
                    }
                }
            }
        }
        SorryCardGameMainGameClass? _mainGame;
        [Command(EnumCommandCategory.Plain)]
        public async Task SorryPlayerAsync()
        {
            if (_mainGame!.BasicData!.MultiPlayer == true)
            {
                await _mainGame.Network!.SendAllAsync("sorryplayer", Id);
            }
            await _mainGame.SorryPlayerAsync(Id);
        }
        public bool CanSorryPlayer()
        {
            if (PlayerCategory == EnumPlayerCategory.Self)
            {
                return false; //you can't even click on your own pile.
            }
            if (_mainGame!.SaveRoot!.GameStatus != EnumGameStatus.ChoosePlayerToSorry)
            {
                return false;
            }
            return HowManyAtHome > 0;
        }
        public void Load(SorryCardGameMainGameClass mainGame)
        {
            _mainGame = mainGame;
        }
    }
}