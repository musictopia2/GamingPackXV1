
using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGamingUIBlazorLibrary.Bootstrappers;
using CommonBasicStandardLibraries.CollectionClasses;
using CommonBasicStandardLibraries.Exceptions;
using GameLoaderBlazorLibrary;
using System;
namespace CristinaFavoriteSoloGames
{
    public class BasicViewModel : LoaderViewModel
    {
        public BasicViewModel(IStartUp starts) : base(starts)
        {
        }

        public override string Title => "CristinaFavoriteSoloGames";                

                
        protected override void GenerateGameList()
        {
            GameList = new CustomBasicList<string>() { "Blackjack", "Clock Solitaire", "Carpet Solitaire", "Easy Go Solitaire", "Froggies", "Klondike Solitaire", "MahJong Solitaire", "Mastermind", "Spider Solitaire"};
        }
        protected override Type GetGameType()
        {
            if (GameName == "Blackjack")
            {
                return typeof(BlackjackBlazor.Index);
            }
            if (GameName == "Clock Solitaire")
            {
                return typeof(ClockSolitaireBlazor.Index);
            }
            if (GameName == "Carpet Solitaire")
            {
                return typeof(CarpetSolitaireBlazor.Index);
            }
            if (GameName == "Easy Go Solitaire")
            {
                return typeof(EasyGoSolitaireBlazor.Index);
            }
            if (GameName == "Froggies")
            {
                return typeof(FroggiesBlazor.Index);
            }
            if (GameName == "Klondike Solitaire")
            {
                return typeof(KlondikeSolitaireBlazor.Index);
            }
            if (GameName == "MahJong Solitaire")
            {
                return typeof(MahJongSolitaireBlazor.Index);
            }
            if (GameName == "Mastermind")
            {
                return typeof(MastermindBlazor.Index);
            }
            if (GameName == "Spider Solitaire")
            {
                return typeof(SpiderSolitaireBlazor.Index);
            }
            throw new BasicBlankException("Game Not Found");
        }
        protected override IGameBootstrapper ChooseGame()
        {
            if (GameName == "Blackjack")
            {
                return new BlackjackBlazor.Bootstrapper(Starts, Mode);
            }
            if (GameName == "Clock Solitaire")
            {
                return new ClockSolitaireBlazor.Bootstrapper(Starts, Mode);
            }
            if (GameName == "Carpet Solitaire")
            {
                return new CarpetSolitaireBlazor.Bootstrapper(Starts, Mode);
            }
            if (GameName == "Easy Go Solitaire")
            {
                return new EasyGoSolitaireBlazor.Bootstrapper(Starts, Mode);
            }
            if (GameName == "Froggies")
            {
                return new FroggiesBlazor.Bootstrapper(Starts, Mode);
            }
            if (GameName == "Klondike Solitaire")
            {
                return new KlondikeSolitaireBlazor.Bootstrapper(Starts, Mode);
            }
            if (GameName == "MahJong Solitaire")
            {
                return new MahJongSolitaireBlazor.Bootstrapper(Starts, Mode);
            }
            if (GameName == "Mastermind")
            {
                return new MastermindBlazor.Bootstrapper(Starts, Mode);
            }
            if (GameName == "Spider Solitaire")
            {
                return new SpiderSolitaireBlazor.Bootstrapper(Starts, Mode);
            }
            throw new BasicBlankException("Game Not Found");
        }
    }
}