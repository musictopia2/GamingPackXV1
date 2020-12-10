using BaseMahjongTilesCP;
using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGameFrameworkLibrary.MahjongTileClasses;
using BasicGameFrameworkLibrary.StandardImplementations.GlobalClasses;
using BasicGamingUIBlazorLibrary.Bootstrappers;
using MahJongSolitaireCP.ViewModels;
using System.Threading.Tasks; //most of the time, i will be using asyncs.
namespace MahJongSolitaireBlazor
{
    public class Bootstrapper : SinglePlayerBootstrapper<MahJongSolitaireShellViewModel>
    {
        public Bootstrapper(IStartUp starts, EnumGamePackageMode mode) : base(starts, mode)
        {
        }

        protected override Task ConfigureAsync()
        {
            OurContainer!.RegisterNonSavedClasses<MahJongSolitaireShellViewModel>();
            OurContainer!.RegisterType<BaseMahjongGlobals>(true);
            OurContainer.RegisterType<MahjongShuffler>(true);
            return Task.CompletedTask;
        }
    }
}