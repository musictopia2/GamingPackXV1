
using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGamingUIBlazorLibrary.Bootstrappers;
using CommonBasicStandardLibraries.CollectionClasses;
using CommonBasicStandardLibraries.Exceptions;
using GameLoaderBlazorLibrary;
using System;
namespace NonCardOrSolitaireGames
{
    public class BasicViewModel : LoaderViewModel
    {
        public BasicViewModel(IStartUp starts) : base(starts)
        {
        }

        public override string Title => "NonCardOrSolitaireGames";                

                
        protected override void GenerateGameList()
        {
            GameList = new CustomBasicList<string>() { "Blackjack", "Bunco Dice Game", "Froggies", "Mastermind", "Minesweeper", "Poker", "Solitaire Board Game", "XPuzzle"};
        }
        protected override Type GetGameType()
        {
            if (GameName == "Blackjack")
            {
                return typeof(BlackjackBlazor.Index);
            }
            if (GameName == "Bunco Dice Game")
            {
                return typeof(BuncoDiceGameBlazor.Index);
            }
            if (GameName == "Froggies")
            {
                return typeof(FroggiesBlazor.Index);
            }
            if (GameName == "Mastermind")
            {
                return typeof(MastermindBlazor.Index);
            }
            if (GameName == "Minesweeper")
            {
                return typeof(MinesweeperBlazor.Index);
            }
            if (GameName == "Poker")
            {
                return typeof(PokerBlazor.Index);
            }
            if (GameName == "Solitaire Board Game")
            {
                return typeof(SolitaireBoardGameBlazor.Index);
            }
            if (GameName == "XPuzzle")
            {
                return typeof(XPuzzleBlazor.Index);
            }
            throw new BasicBlankException("Game Not Found");
        }
        protected override IGameBootstrapper ChooseGame()
        {
            if (GameName == "Blackjack")
            {
                return new BlackjackBlazor.Bootstrapper(Starts, Mode);
            }
            if (GameName == "Bunco Dice Game")
            {
                return new BuncoDiceGameBlazor.Bootstrapper(Starts, Mode);
            }
            if (GameName == "Froggies")
            {
                return new FroggiesBlazor.Bootstrapper(Starts, Mode);
            }
            if (GameName == "Mastermind")
            {
                return new MastermindBlazor.Bootstrapper(Starts, Mode);
            }
            if (GameName == "Minesweeper")
            {
                return new MinesweeperBlazor.Bootstrapper(Starts, Mode);
            }
            if (GameName == "Poker")
            {
                return new PokerBlazor.Bootstrapper(Starts, Mode);
            }
            if (GameName == "Solitaire Board Game")
            {
                return new SolitaireBoardGameBlazor.Bootstrapper(Starts, Mode);
            }
            if (GameName == "XPuzzle")
            {
                return new XPuzzleBlazor.Bootstrapper(Starts, Mode);
            }
            throw new BasicBlankException("Game Not Found");
        }
    }
}