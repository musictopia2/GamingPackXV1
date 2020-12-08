using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.GamePieceModels;
using CommonBasicStandardLibraries.CollectionClasses;
using CommonBasicStandardLibraries.MVVMFramework.ViewModels;
namespace BasicGameFrameworkLibrary.ChooserClasses
{
    public interface IListViewPicker : INotifyPropertyChangedEx
    {
        ICustomCommand ItemSelectedCommand { get; }
        CustomBasicList<ListPieceModel> TextList { get; }
    }
}