
using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGamingUIBlazorLibrary.Bootstrappers;
using CommonBasicStandardLibraries.CollectionClasses;
using CommonBasicStandardLibraries.Exceptions;
using GameLoaderBlazorLibrary;
using System;
namespace RareSoloGames
{
    public class BasicViewModel : LoaderViewModel
    {
        public BasicViewModel(IStartUp starts) : base(starts)
        {
        }

        public override string Title => "RareSoloGames";                

                
        protected override void GenerateGameList()
        {
            GameList = new CustomBasicList<string>() { "Accordian Solitaire", "Agnes Solitaire", "Alternation Solitaire", "Block Eleven Solitaire", "Calculation Solitaire", "Captive Queens Solitaire", "Demon Solitaire", "Little Spider Solitaire", "Raglan Solitaire"};
        }
        protected override Type GetGameType()
        {
            if (GameName == "Accordian Solitaire")
            {
                return typeof(AccordianSolitaireBlazor.Index);
            }
            if (GameName == "Agnes Solitaire")
            {
                return typeof(AgnesSolitaireBlazor.Index);
            }
            if (GameName == "Alternation Solitaire")
            {
                return typeof(AlternationSolitaireBlazor.Index);
            }
            if (GameName == "Block Eleven Solitaire")
            {
                return typeof(BlockElevenSolitaireBlazor.Index);
            }
            if (GameName == "Calculation Solitaire")
            {
                return typeof(CalculationSolitaireBlazor.Index);
            }
            if (GameName == "Captive Queens Solitaire")
            {
                return typeof(CaptiveQueensSolitaireBlazor.Index);
            }
            if (GameName == "Demon Solitaire")
            {
                return typeof(DemonSolitaireBlazor.Index);
            }
            if (GameName == "Little Spider Solitaire")
            {
                return typeof(LittleSpiderSolitaireBlazor.Index);
            }
            if (GameName == "Raglan Solitaire")
            {
                return typeof(RaglanSolitaireBlazor.Index);
            }
            throw new BasicBlankException("Game Not Found");
        }
        protected override IGameBootstrapper ChooseGame()
        {
            if (GameName == "Accordian Solitaire")
            {
                return new AccordianSolitaireBlazor.Bootstrapper(Starts, Mode);
            }
            if (GameName == "Agnes Solitaire")
            {
                return new AgnesSolitaireBlazor.Bootstrapper(Starts, Mode);
            }
            if (GameName == "Alternation Solitaire")
            {
                return new AlternationSolitaireBlazor.Bootstrapper(Starts, Mode);
            }
            if (GameName == "Block Eleven Solitaire")
            {
                return new BlockElevenSolitaireBlazor.Bootstrapper(Starts, Mode);
            }
            if (GameName == "Calculation Solitaire")
            {
                return new CalculationSolitaireBlazor.Bootstrapper(Starts, Mode);
            }
            if (GameName == "Captive Queens Solitaire")
            {
                return new CaptiveQueensSolitaireBlazor.Bootstrapper(Starts, Mode);
            }
            if (GameName == "Demon Solitaire")
            {
                return new DemonSolitaireBlazor.Bootstrapper(Starts, Mode);
            }
            if (GameName == "Little Spider Solitaire")
            {
                return new LittleSpiderSolitaireBlazor.Bootstrapper(Starts, Mode);
            }
            if (GameName == "Raglan Solitaire")
            {
                return new RaglanSolitaireBlazor.Bootstrapper(Starts, Mode);
            }
            throw new BasicBlankException("Game Not Found");
        }
    }
}