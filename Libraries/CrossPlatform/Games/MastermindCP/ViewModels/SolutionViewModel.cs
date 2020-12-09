using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGameFrameworkLibrary.ViewModelInterfaces;
using CommonBasicStandardLibraries.CollectionClasses;
using CommonBasicStandardLibraries.Exceptions;
using CommonBasicStandardLibraries.MVVMFramework.Blazor.ViewModels;
using MastermindCP.Data;
using MastermindCP.Logic;
namespace MastermindCP.ViewModels
{
    [InstanceGame]
    public class SolutionViewModel : BlazorScreenViewModel, IMainScreen, IBlankGameVM
    {
        public CustomBasicList<Bead> SolutionList = new CustomBasicList<Bead>();
        public SolutionViewModel(GlobalClass global, CommandContainer command)
        {
            if (global.Solution == null)
            {
                throw new BasicBlankException("There is no solution found.  Rethink");
            }
            CommandContainer = command;
            SolutionList = global.Solution;
        }
        public CommandContainer CommandContainer { get; set; }
    }
}