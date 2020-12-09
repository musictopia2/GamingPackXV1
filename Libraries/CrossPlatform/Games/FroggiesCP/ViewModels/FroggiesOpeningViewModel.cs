using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.ChooserClasses;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.DIContainers;
using BasicGameFrameworkLibrary.ViewModelInterfaces;
using CommonBasicStandardLibraries.MVVMFramework.Blazor.ViewModels;
using FroggiesCP.Data;
using System.Threading.Tasks; //most of the time, i will be using asyncs.

namespace FroggiesCP.ViewModels
{
    [SingletonGame]
    public class FroggiesOpeningViewModel : BlazorScreenViewModel, IBlankGameVM
    {
        public int NumberOfFrogs { get; set; }
        public CommandContainer CommandContainer { get; set; }

        public NumberPicker LevelPicker;
        private readonly LevelClass _global;
        public FroggiesOpeningViewModel(CommandContainer container,
            IGamePackageResolver resolver,
            LevelClass global,
            BasicData data
            )
        {
            _global = global;
            CommandContainer = container;
            NumberOfFrogs = _global.NumberOfFrogs;
            LevelPicker = new NumberPicker(container, resolver);

            LevelPicker.LoadNormalNumberRangeValues(3, 60); //maybe 60 at a max is good enough.


            //if (data.IsXamarinForms == false)
            //{
            //    LevelPicker.LoadNormalNumberRangeValues(3, 70);
            //}
            //else
            //{
            //    LevelPicker.LoadNormalNumberRangeValues(3, 40); //maybe can load more.  well see.
            //}
            LevelPicker.SelectNumberValue(NumberOfFrogs);
            LevelPicker.ChangedNumberValueAsync += LevelPicker_ChangedNumberValueAsync;
        }

        private Task LevelPicker_ChangedNumberValueAsync(int chosen)
        {
            NumberOfFrogs = chosen;
            _global.NumberOfFrogs = chosen;
            return Task.CompletedTask;
        }

    }
}
