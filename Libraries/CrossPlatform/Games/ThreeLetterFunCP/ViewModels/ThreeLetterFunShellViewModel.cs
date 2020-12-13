using BasicGameFrameworkLibrary.BasicDrawables.BasicClasses;
using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGameFrameworkLibrary.DIContainers;
using BasicGameFrameworkLibrary.MultiplayerClasses.SavedGameClasses;
using BasicGameFrameworkLibrary.TestUtilities;
using BasicGameFrameworkLibrary.ViewModels;
using CommonBasicStandardLibraries.CollectionClasses;
using CommonBasicStandardLibraries.Exceptions;
using CommonBasicStandardLibraries.Messenging;
using CommonBasicStandardLibraries.MVVMFramework.Blazor.ViewModels;
using CommonBasicStandardLibraries.MVVMFramework.UIHelpers;
using System;
using System.Threading.Tasks;
using ThreeLetterFunCP.Data;
using ThreeLetterFunCP.EventModels;
namespace ThreeLetterFunCP.ViewModels
{
    public class ThreeLetterFunShellViewModel : BasicMultiplayerShellViewModel<ThreeLetterFunPlayerItem>, IHandleAsync<NextScreenEventModel>
    {
        public static EnumTestCategory TestMode = EnumTestCategory.None;
        public ThreeLetterFunShellViewModel(IGamePackageResolver mainContainer,
            CommandContainer container,
            IGameInfo gameData,
            BasicData basicData,
            IMultiplayerSaveState save,
            TestOptions test)
            : base(mainContainer, container, gameData, basicData, save, test)
        {
        }
        protected override async Task ActivateAsync()
        {
            if (BasicData.MultiPlayer == true)
            {
                UIPlatform.ShowError("Needs to rethink multiplayer.");
                return;
            }
            await base.ActivateAsync();
            if (TestMode != EnumTestCategory.None && BasicData.GamePackageMode == EnumGamePackageMode.Production)
            {
                throw new BasicBlankException("Cannot have a test mode for screens because its in production");
            }
            if (TestMode != EnumTestCategory.None)
            {
                IBlazorScreen screen;
                switch (TestMode)
                {
                    case EnumTestCategory.FirstOption:
                        FirstScreen = MainContainer.Resolve<FirstOptionViewModel>();
                        screen = FirstScreen;
                        break;
                    case EnumTestCategory.CardsPlayer:
                        CardsScreen = MainContainer.Resolve<CardsPlayerViewModel>();
                        screen = CardsScreen;
                        break;
                    case EnumTestCategory.Advanced:
                        AdvancedScreen = MainContainer.Resolve<AdvancedOptionsViewModel>();
                        screen = AdvancedScreen;
                        break;
                    default:
                        throw new BasicBlankException("Rethink");
                }
                await LoadScreenAsync(screen);
            }
        }
        protected override bool CanStartWithOpenScreen => TestMode == EnumTestCategory.None;
        public FirstOptionViewModel? FirstScreen { get; set; }
        public AdvancedOptionsViewModel? AdvancedScreen { get; set; }
        public CardsPlayerViewModel? CardsScreen { get; set; }
        //for each step, will close the screens it was up to.
        protected override CustomBasicList<Type> GetAdditionalObjectsToReset()
        {
            return new CustomBasicList<Type>()
            {
                typeof(GenericCardShuffler<ThreeLetterFunCardData>)
            };
        }
        private async Task CloseStartingScreensAsync()
        {
            if (FirstScreen != null)
            {
                await CloseSpecificChildAsync(FirstScreen);
                FirstScreen = null;
                return;
            }
            if (AdvancedScreen != null)
            {
                await CloseSpecificChildAsync(AdvancedScreen);
                AdvancedScreen = null;
            }
            if (CardsScreen != null)
            {
                await CloseSpecificChildAsync(CardsScreen);
                CardsScreen = null;
            }
        }
        protected override async Task GetStartingScreenAsync()
        {

            FirstScreen = MainContainer.Resolve<FirstOptionViewModel>();
            await LoadScreenAsync(FirstScreen);
        }
        protected override IMainScreen GetMainViewModel()
        {
            var model = MainContainer.Resolve<ThreeLetterFunMainViewModel>();
            return model;
        }
        async Task IHandleAsync<NextScreenEventModel>.HandleAsync(NextScreenEventModel message)
        {
            await CloseStartingScreensAsync();
            switch (message.Screen)
            {
                case BeginningClasses.EnumNextScreen.Advanced:
                    AdvancedScreen = MainContainer.Resolve<AdvancedOptionsViewModel>();
                    await LoadScreenAsync(AdvancedScreen);
                    break;
                case BeginningClasses.EnumNextScreen.Cards:
                    CardsScreen = MainContainer.Resolve<CardsPlayerViewModel>();
                    await LoadScreenAsync(CardsScreen);
                    break;
                case BeginningClasses.EnumNextScreen.Finished:
                    await StartNewGameAsync();
                    break;
                default:
                    throw new BasicBlankException("Next screen not supported");
            }
        }
    }
}