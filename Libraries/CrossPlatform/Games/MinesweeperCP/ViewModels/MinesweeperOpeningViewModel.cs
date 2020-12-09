using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.ChooserClasses;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGameFrameworkLibrary.DIContainers;
using BasicGameFrameworkLibrary.ViewModelInterfaces;
using CommonBasicStandardLibraries.CollectionClasses;
using CommonBasicStandardLibraries.Exceptions;
using CommonBasicStandardLibraries.MVVMFramework.Blazor.ViewModels;
using MinesweeperCP.Data;
using MinesweeperCP.Logic;
using System.Threading.Tasks; //most of the time, i will be using asyncs.
namespace MinesweeperCP.ViewModels
{
    [InstanceGame]
    public class MinesweeperOpeningViewModel : BlazorScreenViewModel, ILevelVM, IMainScreen, IBlankGameVM
    {

        private EnumLevel _levelChosen = EnumLevel.Easy;

        public EnumLevel LevelChosen
        {
            get { return _levelChosen; }
            set
            {
                if (SetProperty(ref _levelChosen, value))
                {
                    //can decide what to do when property changes
                    this.PopulateMinesNeeded();
                    _global.Level = value;
                }

            }
        }

        private int _howManyMinesNeeded;

        public int HowManyMinesNeeded
        {
            get { return _howManyMinesNeeded; }
            set
            {
                if (SetProperty(ref _howManyMinesNeeded, value))
                {
                    //can decide what to do when property changes
                }

            }
        }

        public CommandContainer CommandContainer { get; set; }

        public ListViewPicker LevelPicker;
        private readonly LevelClass _global;

        public MinesweeperOpeningViewModel(CommandContainer container, IGamePackageResolver resolver, LevelClass global)
        {
            LevelPicker = new ListViewPicker(container, resolver); //hopefully this simple.
            CommandContainer = container;
            LevelPicker.SelectionMode = ListViewPicker.EnumSelectionMode.SingleItem;
            LevelPicker.IndexMethod = ListViewPicker.EnumIndexMethod.OneBased;
            _global = global;
            LevelChosen = _global.Level;
            this.PopulateMinesNeeded();
            LevelPicker.ItemSelectedAsync += LevelPicker_ItemSelectedAsync;
            LevelPicker.LoadTextList(new CustomBasicList<string>() { "Easy", "Medium", "Hard" });
            switch (LevelChosen)
            {
                case EnumLevel.Easy:
                    LevelPicker.SelectSpecificItem(1);
                    break;
                case EnumLevel.Medium:
                    LevelPicker.SelectSpecificItem(2);
                    break;
                case EnumLevel.Hard:
                    LevelPicker.SelectSpecificItem(3);
                    break;
                default:
                    throw new BasicBlankException("Not Supported");
            }
            LevelPicker.IsEnabled = true; //take a risk this time.  not sure why it worked before.
        }

        private Task LevelPicker_ItemSelectedAsync(int selectedIndex, string selectedText)
        {
            LevelChosen = (EnumLevel)selectedIndex;
            return Task.CompletedTask;
        }

    }
}