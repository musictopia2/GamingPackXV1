using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.GamePieceModels;
using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
using CommonBasicStandardLibraries.CollectionClasses;
using CommonBasicStandardLibraries.MVVMFramework.ViewModels;
using System.Reflection;
using System.Threading.Tasks;
namespace BasicGameFrameworkLibrary.ChooserClasses
{
    public class GamePackageLoaderPickerCP : ObservableObject, IListViewPicker
    {
        public readonly CustomBasicList<ListPieceModel> TextList = new CustomBasicList<ListPieceModel>();
        ICustomCommand IListViewPicker.ItemSelectedCommand { get => ItemSelectedCommand; }
        public ICustomCommand ItemSelectedCommand { get; set; }

        public event ItemSelectedEventHandler? ItemSelectedAsync; // better to have too much information than not enough information.
        public delegate Task ItemSelectedEventHandler(int selectedIndex, string selectedText);
        CustomBasicList<ListPieceModel> IListViewPicker.TextList => TextList;
        public GamePackageLoaderPickerCP()
        {
            MethodInfo method = this.GetPrivateMethod(nameof(ProcessItemAsync));
            ItemSelectedCommand = new OldReflectiveCommand(this, method, canExecuteM: null!, null!);
        }

        public void LoadTextList(CustomBasicList<string> thisList)
        {
            CustomBasicList<ListPieceModel> tempList = new CustomBasicList<ListPieceModel>();
            int x;
            x = 1;
            foreach (var firstText in thisList)
            {
                ListPieceModel newText = new ListPieceModel();
                newText.Index = x;
                newText.DisplayText = firstText;
                tempList.Add(newText);
                x += 1;
            }
            TextList.ReplaceRange(tempList);
        }
        public void SelectSpecificItem(int index)
        {
            foreach (var thisText in TextList)
            {
                if (thisText.Index == index)
                {
                    thisText.IsSelected = true;
                }
                else
                {
                    thisText.IsSelected = false;
                }
            }
            return;
        }


        private async Task ProcessItemAsync(ListPieceModel piece)
        {
            SelectSpecificItem(piece.Index);
            if (ItemSelectedAsync == null)
            {
                return;
            }
            await ItemSelectedAsync.Invoke(piece.Index, piece.DisplayText);
        }


    }
}
