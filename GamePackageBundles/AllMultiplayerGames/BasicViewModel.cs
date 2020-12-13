
using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGamingUIBlazorLibrary.Bootstrappers;
using CommonBasicStandardLibraries.CollectionClasses;
using CommonBasicStandardLibraries.Exceptions;
using GameLoaderBlazorLibrary;
using System;
namespace AllMultiplayerGames
{
    public class BasicViewModel : LoaderViewModel
    {
        public BasicViewModel(IStartUp starts) : base(starts)
        {
        }

        public override string Title => "AllMultiplayerGames";                

                
        protected override void GenerateGameList()
        {
            GameList = new CustomBasicList<string>() { "21 Dice Game", "8 Round Rummy", "Aggravation", "Backgammon", "Battleship", "Bingo", "Blades Of Steel", "Bowling Dice Game", "California Jack", "Candyland", "Checkers", "Chess", "Chinazo", "Chinese Checkers", "Clue Board Game", "Concentration", "Connect Four", "Connect The Dots", "Countdown", "Cousin Rummy", "Crazy Eights", "Cribbage", "Dead Die 96", "Dice Dominos", "Dominos (Mexican Train)", "Dominos (Regular)", "Dummy Rummy", "Dutch Blitz", "Fill Or Bust", "Five Crowns", "Flinch", "Fluxx", "Four Suit Rummy", "Galaxy Card Game", "German Whist", "Go Fish", "Golf Card Game", "Hit The Deck", "Horseshoe", "Huse Hearts", "Italian Dominos", "Kismet", "Life Board Game", "Life Card Game", "Lotto Dominos", "Mancala", "Milk Run", "Millebournes", "Monastery Card Game", "Monopoly Card Game", "Old Maid", "Opetong", "Pass Out Dice Game", "Payday", "Phase 10", "Pickel Card Game", "Pinochle (2 Player)", "Racko", "Rage Card Game", "Roll Em", "Rook", "Rounds Card Game", "Rummy 500", "Rummy Dice", "Sequence Dice", "Ship Captain Crew", "Sinister Six", "Sixty Six (2 Player)", "SkipBo", "Skuck Card Game", "Snag Card Game", "Snakes And Ladders", "Sorry", "Sorry Card Game", "Spades (2 Player)", "Tee It Up", "Think Twice", "Three Letter Fun", "Tic Tac Toe", "Tile Rummy", "Trouble", "Uno", "Xactika", "Yacht Race", "Yahtzee", "Yahtzee Hands Down"};
        }
        protected override Type GetGameType()
        {
            if (GameName == "21 Dice Game")
            {
                return typeof(A21DiceGameBlazor.Index);
            }
            if (GameName == "8 Round Rummy")
            {
                return typeof(A8RoundRummyBlazor.Index);
            }
            if (GameName == "Aggravation")
            {
                return typeof(AggravationBlazor.Index);
            }
            if (GameName == "Backgammon")
            {
                return typeof(BackgammonBlazor.Index);
            }
            if (GameName == "Battleship")
            {
                return typeof(BattleshipBlazor.Index);
            }
            if (GameName == "Bingo")
            {
                return typeof(BingoBlazor.Index);
            }
            if (GameName == "Blades Of Steel")
            {
                return typeof(BladesOfSteelBlazor.Index);
            }
            if (GameName == "Bowling Dice Game")
            {
                return typeof(BowlingDiceGameBlazor.Index);
            }
            if (GameName == "California Jack")
            {
                return typeof(CaliforniaJackBlazor.Index);
            }
            if (GameName == "Candyland")
            {
                return typeof(CandylandBlazor.Index);
            }
            if (GameName == "Checkers")
            {
                return typeof(CheckersBlazor.Index);
            }
            if (GameName == "Chess")
            {
                return typeof(ChessBlazor.Index);
            }
            if (GameName == "Chinazo")
            {
                return typeof(ChinazoBlazor.Index);
            }
            if (GameName == "Chinese Checkers")
            {
                return typeof(ChineseCheckersBlazor.Index);
            }
            if (GameName == "Clue Board Game")
            {
                return typeof(ClueBoardGameBlazor.Index);
            }
            if (GameName == "Concentration")
            {
                return typeof(ConcentrationBlazor.Index);
            }
            if (GameName == "Connect Four")
            {
                return typeof(ConnectFourBlazor.Index);
            }
            if (GameName == "Connect The Dots")
            {
                return typeof(ConnectTheDotsBlazor.Index);
            }
            if (GameName == "Countdown")
            {
                return typeof(CountdownBlazor.Index);
            }
            if (GameName == "Cousin Rummy")
            {
                return typeof(CousinRummyBlazor.Index);
            }
            if (GameName == "Crazy Eights")
            {
                return typeof(CrazyEightsBlazor.Index);
            }
            if (GameName == "Cribbage")
            {
                return typeof(CribbageBlazor.Index);
            }
            if (GameName == "Dead Die 96")
            {
                return typeof(DeadDie96Blazor.Index);
            }
            if (GameName == "Dice Dominos")
            {
                return typeof(DiceDominosBlazor.Index);
            }
            if (GameName == "Dominos (Mexican Train)")
            {
                return typeof(DominosMexicanTrainBlazor.Index);
            }
            if (GameName == "Dominos (Regular)")
            {
                return typeof(DominosRegularBlazor.Index);
            }
            if (GameName == "Dummy Rummy")
            {
                return typeof(DummyRummyBlazor.Index);
            }
            if (GameName == "Dutch Blitz")
            {
                return typeof(DutchBlitzBlazor.Index);
            }
            if (GameName == "Fill Or Bust")
            {
                return typeof(FillOrBustBlazor.Index);
            }
            if (GameName == "Five Crowns")
            {
                return typeof(FiveCrownsBlazor.Index);
            }
            if (GameName == "Flinch")
            {
                return typeof(FlinchBlazor.Index);
            }
            if (GameName == "Fluxx")
            {
                return typeof(FluxxBlazor.Index);
            }
            if (GameName == "Four Suit Rummy")
            {
                return typeof(FourSuitRummyBlazor.Index);
            }
            if (GameName == "Galaxy Card Game")
            {
                return typeof(GalaxyCardGameBlazor.Index);
            }
            if (GameName == "German Whist")
            {
                return typeof(GermanWhistBlazor.Index);
            }
            if (GameName == "Go Fish")
            {
                return typeof(GoFishBlazor.Index);
            }
            if (GameName == "Golf Card Game")
            {
                return typeof(GolfCardGameBlazor.Index);
            }
            if (GameName == "Hit The Deck")
            {
                return typeof(HitTheDeckBlazor.Index);
            }
            if (GameName == "Horseshoe")
            {
                return typeof(HorseshoeCardGameBlazor.Index);
            }
            if (GameName == "Huse Hearts")
            {
                return typeof(HuseHeartsBlazor.Index);
            }
            if (GameName == "Italian Dominos")
            {
                return typeof(ItalianDominosBlazor.Index);
            }
            if (GameName == "Kismet")
            {
                return typeof(KismetBlazor.Index);
            }
            if (GameName == "Life Board Game")
            {
                return typeof(LifeBoardGameBlazor.Index);
            }
            if (GameName == "Life Card Game")
            {
                return typeof(LifeCardGameBlazor.Index);
            }
            if (GameName == "Lotto Dominos")
            {
                return typeof(LottoDominosBlazor.Index);
            }
            if (GameName == "Mancala")
            {
                return typeof(MancalaBlazor.Index);
            }
            if (GameName == "Milk Run")
            {
                return typeof(MilkRunBlazor.Index);
            }
            if (GameName == "Millebournes")
            {
                return typeof(MillebournesBlazor.Index);
            }
            if (GameName == "Monastery Card Game")
            {
                return typeof(MonasteryCardGameBlazor.Index);
            }
            if (GameName == "Monopoly Card Game")
            {
                return typeof(MonopolyCardGameBlazor.Index);
            }
            if (GameName == "Old Maid")
            {
                return typeof(OldMaidBlazor.Index);
            }
            if (GameName == "Opetong")
            {
                return typeof(OpetongBlazor.Index);
            }
            if (GameName == "Pass Out Dice Game")
            {
                return typeof(PassOutDiceGameBlazor.Index);
            }
            if (GameName == "Payday")
            {
                return typeof(PaydayBlazor.Index);
            }
            if (GameName == "Phase 10")
            {
                return typeof(Phase10Blazor.Index);
            }
            if (GameName == "Pickel Card Game")
            {
                return typeof(PickelCardGameBlazor.Index);
            }
            if (GameName == "Pinochle (2 Player)")
            {
                return typeof(Pinochle2PlayerBlazor.Index);
            }
            if (GameName == "Racko")
            {
                return typeof(RackoBlazor.Index);
            }
            if (GameName == "Rage Card Game")
            {
                return typeof(RageCardGameBlazor.Index);
            }
            if (GameName == "Roll Em")
            {
                return typeof(RollEmBlazor.Index);
            }
            if (GameName == "Rook")
            {
                return typeof(RookBlazor.Index);
            }
            if (GameName == "Rounds Card Game")
            {
                return typeof(RoundsCardGameBlazor.Index);
            }
            if (GameName == "Rummy 500")
            {
                return typeof(Rummy500Blazor.Index);
            }
            if (GameName == "Rummy Dice")
            {
                return typeof(RummyDiceBlazor.Index);
            }
            if (GameName == "Sequence Dice")
            {
                return typeof(SequenceDiceBlazor.Index);
            }
            if (GameName == "Ship Captain Crew")
            {
                return typeof(ShipCaptainCrewBlazor.Index);
            }
            if (GameName == "Sinister Six")
            {
                return typeof(SinisterSixBlazor.Index);
            }
            if (GameName == "Sixty Six (2 Player)")
            {
                return typeof(SixtySix2PlayerBlazor.Index);
            }
            if (GameName == "SkipBo")
            {
                return typeof(SkipboBlazor.Index);
            }
            if (GameName == "Skuck Card Game")
            {
                return typeof(SkuckCardGameBlazor.Index);
            }
            if (GameName == "Snag Card Game")
            {
                return typeof(SnagCardGameBlazor.Index);
            }
            if (GameName == "Snakes And Ladders")
            {
                return typeof(SnakesAndLaddersBlazor.Index);
            }
            if (GameName == "Sorry")
            {
                return typeof(SorryBlazor.Index);
            }
            if (GameName == "Sorry Card Game")
            {
                return typeof(SorryCardGameBlazor.Index);
            }
            if (GameName == "Spades (2 Player)")
            {
                return typeof(Spades2PlayerBlazor.Index);
            }
            if (GameName == "Tee It Up")
            {
                return typeof(TeeItUpBlazor.Index);
            }
            if (GameName == "Think Twice")
            {
                return typeof(ThinkTwiceBlazor.Index);
            }
            if (GameName == "Three Letter Fun")
            {
                return typeof(ThreeLetterFunBlazor.Index);
            }
            if (GameName == "Tic Tac Toe")
            {
                return typeof(TicTacToeBlazor.Index);
            }
            if (GameName == "Tile Rummy")
            {
                return typeof(TileRummyBlazor.Index);
            }
            if (GameName == "Trouble")
            {
                return typeof(TroubleBlazor.Index);
            }
            if (GameName == "Uno")
            {
                return typeof(UnoBlazor.Index);
            }
            if (GameName == "Xactika")
            {
                return typeof(XactikaBlazor.Index);
            }
            if (GameName == "Yacht Race")
            {
                return typeof(YachtRaceBlazor.Index);
            }
            if (GameName == "Yahtzee")
            {
                return typeof(YahtzeeBlazor.Index);
            }
            if (GameName == "Yahtzee Hands Down")
            {
                return typeof(YahtzeeHandsDownBlazor.Index);
            }
            throw new BasicBlankException("Game Not Found");
        }
        protected override IGameBootstrapper ChooseGame()
        {
            if (GameName == "21 Dice Game")
            {
                return new A21DiceGameBlazor.Bootstrapper(Starts, Mode);
            }
            if (GameName == "8 Round Rummy")
            {
                return new A8RoundRummyBlazor.Bootstrapper(Starts, Mode);
            }
            if (GameName == "Aggravation")
            {
                return new AggravationBlazor.Bootstrapper(Starts, Mode);
            }
            if (GameName == "Backgammon")
            {
                return new BackgammonBlazor.Bootstrapper(Starts, Mode);
            }
            if (GameName == "Battleship")
            {
                return new BattleshipBlazor.Bootstrapper(Starts, Mode);
            }
            if (GameName == "Bingo")
            {
                return new BingoBlazor.Bootstrapper(Starts, Mode);
            }
            if (GameName == "Blades Of Steel")
            {
                return new BladesOfSteelBlazor.Bootstrapper(Starts, Mode);
            }
            if (GameName == "Bowling Dice Game")
            {
                return new BowlingDiceGameBlazor.Bootstrapper(Starts, Mode);
            }
            if (GameName == "California Jack")
            {
                return new CaliforniaJackBlazor.Bootstrapper(Starts, Mode);
            }
            if (GameName == "Candyland")
            {
                return new CandylandBlazor.Bootstrapper(Starts, Mode);
            }
            if (GameName == "Checkers")
            {
                return new CheckersBlazor.Bootstrapper(Starts, Mode);
            }
            if (GameName == "Chess")
            {
                return new ChessBlazor.Bootstrapper(Starts, Mode);
            }
            if (GameName == "Chinazo")
            {
                return new ChinazoBlazor.Bootstrapper(Starts, Mode);
            }
            if (GameName == "Chinese Checkers")
            {
                return new ChineseCheckersBlazor.Bootstrapper(Starts, Mode);
            }
            if (GameName == "Clue Board Game")
            {
                return new ClueBoardGameBlazor.Bootstrapper(Starts, Mode);
            }
            if (GameName == "Concentration")
            {
                return new ConcentrationBlazor.Bootstrapper(Starts, Mode);
            }
            if (GameName == "Connect Four")
            {
                return new ConnectFourBlazor.Bootstrapper(Starts, Mode);
            }
            if (GameName == "Connect The Dots")
            {
                return new ConnectTheDotsBlazor.Bootstrapper(Starts, Mode);
            }
            if (GameName == "Countdown")
            {
                return new CountdownBlazor.Bootstrapper(Starts, Mode);
            }
            if (GameName == "Cousin Rummy")
            {
                return new CousinRummyBlazor.Bootstrapper(Starts, Mode);
            }
            if (GameName == "Crazy Eights")
            {
                return new CrazyEightsBlazor.Bootstrapper(Starts, Mode);
            }
            if (GameName == "Cribbage")
            {
                return new CribbageBlazor.Bootstrapper(Starts, Mode);
            }
            if (GameName == "Dead Die 96")
            {
                return new DeadDie96Blazor.Bootstrapper(Starts, Mode);
            }
            if (GameName == "Dice Dominos")
            {
                return new DiceDominosBlazor.Bootstrapper(Starts, Mode);
            }
            if (GameName == "Dominos (Mexican Train)")
            {
                return new DominosMexicanTrainBlazor.Bootstrapper(Starts, Mode);
            }
            if (GameName == "Dominos (Regular)")
            {
                return new DominosRegularBlazor.Bootstrapper(Starts, Mode);
            }
            if (GameName == "Dummy Rummy")
            {
                return new DummyRummyBlazor.Bootstrapper(Starts, Mode);
            }
            if (GameName == "Dutch Blitz")
            {
                return new DutchBlitzBlazor.Bootstrapper(Starts, Mode);
            }
            if (GameName == "Fill Or Bust")
            {
                return new FillOrBustBlazor.Bootstrapper(Starts, Mode);
            }
            if (GameName == "Five Crowns")
            {
                return new FiveCrownsBlazor.Bootstrapper(Starts, Mode);
            }
            if (GameName == "Flinch")
            {
                return new FlinchBlazor.Bootstrapper(Starts, Mode);
            }
            if (GameName == "Fluxx")
            {
                return new FluxxBlazor.Bootstrapper(Starts, Mode);
            }
            if (GameName == "Four Suit Rummy")
            {
                return new FourSuitRummyBlazor.Bootstrapper(Starts, Mode);
            }
            if (GameName == "Galaxy Card Game")
            {
                return new GalaxyCardGameBlazor.Bootstrapper(Starts, Mode);
            }
            if (GameName == "German Whist")
            {
                return new GermanWhistBlazor.Bootstrapper(Starts, Mode);
            }
            if (GameName == "Go Fish")
            {
                return new GoFishBlazor.Bootstrapper(Starts, Mode);
            }
            if (GameName == "Golf Card Game")
            {
                return new GolfCardGameBlazor.Bootstrapper(Starts, Mode);
            }
            if (GameName == "Hit The Deck")
            {
                return new HitTheDeckBlazor.Bootstrapper(Starts, Mode);
            }
            if (GameName == "Horseshoe")
            {
                return new HorseshoeCardGameBlazor.Bootstrapper(Starts, Mode);
            }
            if (GameName == "Huse Hearts")
            {
                return new HuseHeartsBlazor.Bootstrapper(Starts, Mode);
            }
            if (GameName == "Italian Dominos")
            {
                return new ItalianDominosBlazor.Bootstrapper(Starts, Mode);
            }
            if (GameName == "Kismet")
            {
                return new KismetBlazor.Bootstrapper(Starts, Mode);
            }
            if (GameName == "Life Board Game")
            {
                return new LifeBoardGameBlazor.Bootstrapper(Starts, Mode);
            }
            if (GameName == "Life Card Game")
            {
                return new LifeCardGameBlazor.Bootstrapper(Starts, Mode);
            }
            if (GameName == "Lotto Dominos")
            {
                return new LottoDominosBlazor.Bootstrapper(Starts, Mode);
            }
            if (GameName == "Mancala")
            {
                return new MancalaBlazor.Bootstrapper(Starts, Mode);
            }
            if (GameName == "Milk Run")
            {
                return new MilkRunBlazor.Bootstrapper(Starts, Mode);
            }
            if (GameName == "Millebournes")
            {
                return new MillebournesBlazor.Bootstrapper(Starts, Mode);
            }
            if (GameName == "Monastery Card Game")
            {
                return new MonasteryCardGameBlazor.Bootstrapper(Starts, Mode);
            }
            if (GameName == "Monopoly Card Game")
            {
                return new MonopolyCardGameBlazor.Bootstrapper(Starts, Mode);
            }
            if (GameName == "Old Maid")
            {
                return new OldMaidBlazor.Bootstrapper(Starts, Mode);
            }
            if (GameName == "Opetong")
            {
                return new OpetongBlazor.Bootstrapper(Starts, Mode);
            }
            if (GameName == "Pass Out Dice Game")
            {
                return new PassOutDiceGameBlazor.Bootstrapper(Starts, Mode);
            }
            if (GameName == "Payday")
            {
                return new PaydayBlazor.Bootstrapper(Starts, Mode);
            }
            if (GameName == "Phase 10")
            {
                return new Phase10Blazor.Bootstrapper(Starts, Mode);
            }
            if (GameName == "Pickel Card Game")
            {
                return new PickelCardGameBlazor.Bootstrapper(Starts, Mode);
            }
            if (GameName == "Pinochle (2 Player)")
            {
                return new Pinochle2PlayerBlazor.Bootstrapper(Starts, Mode);
            }
            if (GameName == "Racko")
            {
                return new RackoBlazor.Bootstrapper(Starts, Mode);
            }
            if (GameName == "Rage Card Game")
            {
                return new RageCardGameBlazor.Bootstrapper(Starts, Mode);
            }
            if (GameName == "Roll Em")
            {
                return new RollEmBlazor.Bootstrapper(Starts, Mode);
            }
            if (GameName == "Rook")
            {
                return new RookBlazor.Bootstrapper(Starts, Mode);
            }
            if (GameName == "Rounds Card Game")
            {
                return new RoundsCardGameBlazor.Bootstrapper(Starts, Mode);
            }
            if (GameName == "Rummy 500")
            {
                return new Rummy500Blazor.Bootstrapper(Starts, Mode);
            }
            if (GameName == "Rummy Dice")
            {
                return new RummyDiceBlazor.Bootstrapper(Starts, Mode);
            }
            if (GameName == "Sequence Dice")
            {
                return new SequenceDiceBlazor.Bootstrapper(Starts, Mode);
            }
            if (GameName == "Ship Captain Crew")
            {
                return new ShipCaptainCrewBlazor.Bootstrapper(Starts, Mode);
            }
            if (GameName == "Sinister Six")
            {
                return new SinisterSixBlazor.Bootstrapper(Starts, Mode);
            }
            if (GameName == "Sixty Six (2 Player)")
            {
                return new SixtySix2PlayerBlazor.Bootstrapper(Starts, Mode);
            }
            if (GameName == "SkipBo")
            {
                return new SkipboBlazor.Bootstrapper(Starts, Mode);
            }
            if (GameName == "Skuck Card Game")
            {
                return new SkuckCardGameBlazor.Bootstrapper(Starts, Mode);
            }
            if (GameName == "Snag Card Game")
            {
                return new SnagCardGameBlazor.Bootstrapper(Starts, Mode);
            }
            if (GameName == "Snakes And Ladders")
            {
                return new SnakesAndLaddersBlazor.Bootstrapper(Starts, Mode);
            }
            if (GameName == "Sorry")
            {
                return new SorryBlazor.Bootstrapper(Starts, Mode);
            }
            if (GameName == "Sorry Card Game")
            {
                return new SorryCardGameBlazor.Bootstrapper(Starts, Mode);
            }
            if (GameName == "Spades (2 Player)")
            {
                return new Spades2PlayerBlazor.Bootstrapper(Starts, Mode);
            }
            if (GameName == "Tee It Up")
            {
                return new TeeItUpBlazor.Bootstrapper(Starts, Mode);
            }
            if (GameName == "Think Twice")
            {
                return new ThinkTwiceBlazor.Bootstrapper(Starts, Mode);
            }
            if (GameName == "Three Letter Fun")
            {
                return new ThreeLetterFunBlazor.Bootstrapper(Starts, Mode);
            }
            if (GameName == "Tic Tac Toe")
            {
                return new TicTacToeBlazor.Bootstrapper(Starts, Mode);
            }
            if (GameName == "Tile Rummy")
            {
                return new TileRummyBlazor.Bootstrapper(Starts, Mode);
            }
            if (GameName == "Trouble")
            {
                return new TroubleBlazor.Bootstrapper(Starts, Mode);
            }
            if (GameName == "Uno")
            {
                return new UnoBlazor.Bootstrapper(Starts, Mode);
            }
            if (GameName == "Xactika")
            {
                return new XactikaBlazor.Bootstrapper(Starts, Mode);
            }
            if (GameName == "Yacht Race")
            {
                return new YachtRaceBlazor.Bootstrapper(Starts, Mode);
            }
            if (GameName == "Yahtzee")
            {
                return new YahtzeeBlazor.Bootstrapper(Starts, Mode);
            }
            if (GameName == "Yahtzee Hands Down")
            {
                return new YahtzeeHandsDownBlazor.Bootstrapper(Starts, Mode);
            }
            throw new BasicBlankException("Game Not Found");
        }
    }
}