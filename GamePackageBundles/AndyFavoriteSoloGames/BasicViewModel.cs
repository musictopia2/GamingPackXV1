
using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGamingUIBlazorLibrary.Bootstrappers;
using CommonBasicStandardLibraries.CollectionClasses;
using CommonBasicStandardLibraries.Exceptions;
using GameLoaderBlazorLibrary;
using System;
namespace AndyFavoriteSoloGames
{
    public class BasicViewModel : LoaderViewModel
    {
        public BasicViewModel(IStartUp starts) : base(starts)
        {
        }

        public override string Title => "AndyFavoriteSoloGames";                

                
        protected override void GenerateGameList()
        {
            GameList = new CustomBasicList<string>() { "MahJong Solitaire", "Klondike Solitaire", "Bisley Solitaire", "Florentine Solitaire", "Free Cell Solitaire", "Bakers Dozen Solitaire", "Beleagured Castle", "Eight Off Solitaire", "Spider Solitaire", "Martha Solitaire", "Persian Solitaire", "Grandfather's Clock", "Pyramid Solitaire"};
        }
        protected override Type GetGameType()
        {
            if (GameName == "MahJong Solitaire")
            {
                return typeof(MahJongSolitaireBlazor.Index);
            }
            if (GameName == "Klondike Solitaire")
            {
                return typeof(KlondikeSolitaireBlazor.Index);
            }
            if (GameName == "Bisley Solitaire")
            {
                return typeof(BisleySolitaireBlazor.Index);
            }
            if (GameName == "Florentine Solitaire")
            {
                return typeof(FlorentineSolitaireBlazor.Index);
            }
            if (GameName == "Free Cell Solitaire")
            {
                return typeof(FreeCellSolitaireBlazor.Index);
            }
            if (GameName == "Bakers Dozen Solitaire")
            {
                return typeof(BakersDozenSolitaireBlazor.Index);
            }
            if (GameName == "Beleagured Castle")
            {
                return typeof(BeleaguredCastleBlazor.Index);
            }
            if (GameName == "Eight Off Solitaire")
            {
                return typeof(EightOffSolitaireBlazor.Index);
            }
            if (GameName == "Spider Solitaire")
            {
                return typeof(SpiderSolitaireBlazor.Index);
            }
            if (GameName == "Martha Solitaire")
            {
                return typeof(MarthaSolitaireBlazor.Index);
            }
            if (GameName == "Persian Solitaire")
            {
                return typeof(PersianSolitaireBlazor.Index);
            }
            if (GameName == "Grandfather's Clock")
            {
                return typeof(GrandfathersClockBlazor.Index);
            }
            if (GameName == "Pyramid Solitaire")
            {
                return typeof(PyramidSolitaireBlazor.Index);
            }
            throw new BasicBlankException("Game Not Found");
        }
        protected override IGameBootstrapper ChooseGame()
        {
            if (GameName == "MahJong Solitaire")
            {
                return new MahJongSolitaireBlazor.Bootstrapper(Starts, Mode);
            }
            if (GameName == "Klondike Solitaire")
            {
                return new KlondikeSolitaireBlazor.Bootstrapper(Starts, Mode);
            }
            if (GameName == "Bisley Solitaire")
            {
                return new BisleySolitaireBlazor.Bootstrapper(Starts, Mode);
            }
            if (GameName == "Florentine Solitaire")
            {
                return new FlorentineSolitaireBlazor.Bootstrapper(Starts, Mode);
            }
            if (GameName == "Free Cell Solitaire")
            {
                return new FreeCellSolitaireBlazor.Bootstrapper(Starts, Mode);
            }
            if (GameName == "Bakers Dozen Solitaire")
            {
                return new BakersDozenSolitaireBlazor.Bootstrapper(Starts, Mode);
            }
            if (GameName == "Beleagured Castle")
            {
                return new BeleaguredCastleBlazor.Bootstrapper(Starts, Mode);
            }
            if (GameName == "Eight Off Solitaire")
            {
                return new EightOffSolitaireBlazor.Bootstrapper(Starts, Mode);
            }
            if (GameName == "Spider Solitaire")
            {
                return new SpiderSolitaireBlazor.Bootstrapper(Starts, Mode);
            }
            if (GameName == "Martha Solitaire")
            {
                return new MarthaSolitaireBlazor.Bootstrapper(Starts, Mode);
            }
            if (GameName == "Persian Solitaire")
            {
                return new PersianSolitaireBlazor.Bootstrapper(Starts, Mode);
            }
            if (GameName == "Grandfather's Clock")
            {
                return new GrandfathersClockBlazor.Bootstrapper(Starts, Mode);
            }
            if (GameName == "Pyramid Solitaire")
            {
                return new PyramidSolitaireBlazor.Bootstrapper(Starts, Mode);
            }
            throw new BasicBlankException("Game Not Found");
        }
    }
}