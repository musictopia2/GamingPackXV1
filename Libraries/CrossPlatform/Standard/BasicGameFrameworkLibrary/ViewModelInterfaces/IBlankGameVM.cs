using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.CommonInterfaces;
namespace BasicGameFrameworkLibrary.ViewModelInterfaces
{
    public interface IBlankGameVM : IMainScreen
    {
        CommandContainer CommandContainer { get; set; }
    }
}