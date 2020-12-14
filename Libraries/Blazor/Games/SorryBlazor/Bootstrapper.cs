using BasicGameFrameworkLibrary.BasicDrawables.BasicClasses;
using BasicGameFrameworkLibrary.BasicDrawables.Interfaces;
using BasicGameFrameworkLibrary.BasicDrawables.MiscClasses;
using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGameFrameworkLibrary.StandardImplementations.GlobalClasses;
using BasicGamingUIBlazorLibrary.Bootstrappers;
using SorryCP.Data;
using SorryCP.ViewModels;
using System.Threading.Tasks; //most of the time, i will be using asyncs.
namespace SorryBlazor
{
    public class Bootstrapper : MultiplayerBasicBootstrapper<SorryShellViewModel>
    {
        public Bootstrapper(IStartUp starts, EnumGamePackageMode mode) : base(starts, mode)
        {
        }
        protected override Task ConfigureAsync()
        {
            OurContainer!.RegisterNonSavedClasses<SorryShellViewModel>();
            OurContainer.RegisterCommonMultplayerClasses<SorryPlayerItem, SorrySaveInfo>();
            OurContainer.RegisterBeginningColors<EnumColorChoice, SorryPlayerItem, SorrySaveInfo>();
            OurContainer.RegisterType<DrawShuffleClass<CardInfo, SorryPlayerItem>>();
            OurContainer.RegisterType<GenericCardShuffler<CardInfo>>();
            OurContainer.RegisterSingleton<IDeckCount, DeckCount>();
            return Task.CompletedTask;
        }
    }
}