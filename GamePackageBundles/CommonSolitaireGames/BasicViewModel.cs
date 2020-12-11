
using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGamingUIBlazorLibrary.Bootstrappers;
using CommonBasicStandardLibraries.CollectionClasses;
using CommonBasicStandardLibraries.Exceptions;
using GameLoaderBlazorLibrary;
using System;
namespace CommonSolitaireGames
{
    public class BasicViewModel : LoaderViewModel
    {
        public BasicViewModel(IStartUp starts) : base(starts)
        {
        }

        public override string Title => "CommonSolitaireGames";                

                
        protected override void GenerateGameList()
        {
            GameList = new CustomBasicList<string>() { "Carpet Solitaire", "Clock Solitaire", "Cribbage Patience", "Eagle Wings Solitaire", "Easy Go Solitaire", "Heap Solitaire", "Triangle Solitaire", "Vegas Solitaire"};
        }
        protected override Type GetGameType()
        {
            if (GameName == "Carpet Solitaire")
            {
                return typeof(CarpetSolitaireBlazor.Index);
            }
            if (GameName == "Clock Solitaire")
            {
                return typeof(ClockSolitaireBlazor.Index);
            }
            if (GameName == "Cribbage Patience")
            {
                return typeof(CribbagePatienceBlazor.Index);
            }
            if (GameName == "Eagle Wings Solitaire")
            {
                return typeof(EagleWingsSolitaireBlazor.Index);
            }
            if (GameName == "Easy Go Solitaire")
            {
                return typeof(EasyGoSolitaireBlazor.Index);
            }
            if (GameName == "Heap Solitaire")
            {
                return typeof(HeapSolitaireBlazor.Index);
            }
            if (GameName == "Triangle Solitaire")
            {
                return typeof(TriangleSolitaireBlazor.Index);
            }
            if (GameName == "Vegas Solitaire")
            {
                return typeof(VegasSolitaireBlazor.Index);
            }
            throw new BasicBlankException("Game Not Found");
        }
        protected override IGameBootstrapper ChooseGame()
        {
            if (GameName == "Carpet Solitaire")
            {
                return new CarpetSolitaireBlazor.Bootstrapper(Starts, Mode);
            }
            if (GameName == "Clock Solitaire")
            {
                return new ClockSolitaireBlazor.Bootstrapper(Starts, Mode);
            }
            if (GameName == "Cribbage Patience")
            {
                return new CribbagePatienceBlazor.Bootstrapper(Starts, Mode);
            }
            if (GameName == "Eagle Wings Solitaire")
            {
                return new EagleWingsSolitaireBlazor.Bootstrapper(Starts, Mode);
            }
            if (GameName == "Easy Go Solitaire")
            {
                return new EasyGoSolitaireBlazor.Bootstrapper(Starts, Mode);
            }
            if (GameName == "Heap Solitaire")
            {
                return new HeapSolitaireBlazor.Bootstrapper(Starts, Mode);
            }
            if (GameName == "Triangle Solitaire")
            {
                return new TriangleSolitaireBlazor.Bootstrapper(Starts, Mode);
            }
            if (GameName == "Vegas Solitaire")
            {
                return new VegasSolitaireBlazor.Bootstrapper(Starts, Mode);
            }
            throw new BasicBlankException("Game Not Found");
        }
    }
}