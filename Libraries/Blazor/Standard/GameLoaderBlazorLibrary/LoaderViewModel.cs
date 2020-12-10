using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.ChooserClasses;
using BasicGameFrameworkLibrary.CommonInterfaces;
using CommonBasicStandardLibraries.CollectionClasses;
using CommonBasicStandardLibraries.MVVMFramework.Blazor.ViewModels;
using static CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.BasicDataFunctions;
using System;
using System.Threading.Tasks;
using BasicGamingUIBlazorLibrary.Bootstrappers;
using CommonBasicStandardLibraries.Exceptions;
using BasicGamingUIBlazorLibrary.StartupClasses;
using Microsoft.AspNetCore.Components;
namespace GameLoaderBlazorLibrary
{
    public abstract class LoaderViewModel : BlazorScreenViewModel, ILoaderVM
    {
        public GamePackageLoaderPickerCP? PackagePicker { get; set; }
        public Action? StateChanged { get; set; }
        public string GameName { get; private set; } = "";
        public RenderFragment? GameRendered { get; private set; }
        public abstract string Title { get; }

        protected IStartUp Starts;
        protected EnumGamePackageMode Mode;
        protected CustomBasicList<string> GameList = new CustomBasicList<string>();
        //the one being used must be registered here.
        public LoaderViewModel()
        {
            Starts = cons!.Resolve<IStartUp>(); //use custom di.
            //BasicLoaderPage.Multiplayer = Multiplayer;
            GenerateGameList();
            PackagePicker = new GamePackageLoaderPickerCP();
            PackagePicker.LoadTextList(GameList);
            Mode = EnumGamePackageMode.Production; //can now test the production processes.
            PackagePicker.ItemSelectedAsync += PackagePicker_ItemSelectedAsync;

            GlobalStartUp.StartBootStrap = () =>
            {
                IGameBootstrapper _ = ChooseGame(GameName);
            };

        }
        protected abstract void GenerateGameList();
        protected abstract IGameBootstrapper ChooseGame(string gameChosen);
        protected abstract Type GetGameType();
        private Task PackagePicker_ItemSelectedAsync(int selectedIndex, string selectedText)
        {
            if (StateChanged == null)
            {
                throw new BasicBlankException("Must send in statehaschanged");
            }
            GameName = selectedText;
            GameRendered = (builder) =>
            {
                Type type = GetGameType();
                builder.OpenComponent(0, type);
                builder.CloseComponent();
            };
            StateChanged.Invoke();
            return Task.CompletedTask;
            //can't close out of it.  otherwise, delegates don't work.
        }
    }
}