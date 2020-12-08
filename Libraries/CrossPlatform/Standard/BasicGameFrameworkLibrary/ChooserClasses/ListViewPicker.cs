using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.DIContainers;
using BasicGameFrameworkLibrary.MiscProcesses;
using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
using CommonBasicStandardLibraries.CollectionClasses;
using CommonBasicStandardLibraries.Exceptions;
using BasicGameFrameworkLibrary.Extensions;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using BasicGameFrameworkLibrary.GamePieceModels;
namespace BasicGameFrameworkLibrary.ChooserClasses
{
    public class ListViewPicker : SimpleControlObservable, IListViewPicker
    {
        public readonly CustomBasicList<ListPieceModel> TextList = new CustomBasicList<ListPieceModel>(); //try list now.  since its intended to be used from blazor programming model.
        public enum EnumIndexMethod
        {
            Unknown = 0,
            ZeroBased = 1,
            OneBased = 2
        }
        public enum EnumSelectionMode
        {
            SingleItem = 1,
            MultipleItems = 2
        }

        private async Task ProcessClickAsync(ListPieceModel piece)
        {
            if (SelectionMode == EnumSelectionMode.SingleItem)
            {
                SelectSpecificItem(piece.Index);
            }
            else if (piece.IsSelected)
            {
                piece.IsSelected = false;
            }
            else
            {
                piece.IsSelected = true;
            }
            if (ItemSelectedAsync == null)
            {
                return; //ignore because not there.
            }
            await ItemSelectedAsync.Invoke(piece.Index, piece.DisplayText);
        }
        public ListViewPicker(CommandContainer container, IGamePackageResolver resolver) : base(container)
        {
            _privateChoose = new ItemChooserClass<ListPieceModel>(resolver);
            _privateChoose.ValueList = TextList;
            MethodInfo method = this.GetPrivateMethod(nameof(ProcessClickAsync));
            ItemSelectedCommand = new ControlCommand(this, method, container);
        }
        public EnumIndexMethod IndexMethod { get; set; } // so when i send the list, it knows whether to start with 0 or 1.
        public EnumSelectionMode SelectionMode { get; set; } = EnumSelectionMode.SingleItem;
        public event ItemSelectedEventHandler? ItemSelectedAsync; // better to have too much information than not enough information.
        public delegate Task ItemSelectedEventHandler(int selectedIndex, string selectedText);
        private readonly ItemChooserClass<ListPieceModel> _privateChoose;
        private int _selectedIndex = -1;
        public int SelectedIndex
        {
            get
            {
                return _selectedIndex;
            }
            set
            {
                if (SetProperty(ref _selectedIndex, value) == true)
                {
                }
            }
        }
        public string GetText(int index)
        {
            if (IndexMethod == EnumIndexMethod.ZeroBased)
            {
                return TextList[index].DisplayText;
            }
            if (IndexMethod == EnumIndexMethod.OneBased)
            {
                return TextList[index - 1].DisplayText;// i think
            }
            throw new BasicBlankException("Don't know the index method");
        }
        public int IndexOf(string text)
        {
            return TextList.Where(Items => Items.DisplayText == text).Single().Index;
        }
        public void LoadTextList(CustomBasicList<string> thisList)
        {
            if (IndexMethod == EnumIndexMethod.Unknown)
            {
                throw new BasicBlankException("Must know the index method in order to continue");
            }
            CustomBasicList<ListPieceModel> tempList = new CustomBasicList<ListPieceModel>();
            int x;
            if (IndexMethod == EnumIndexMethod.OneBased)
            {
                x = 1;
            }
            else
            {
                x = 0;
            }
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
        public void UnselectAll()
        {
            TextList.UnselectAllObjects();
        }
        public void SelectSpecificItem(int index)
        {
            if (SelectionMode == EnumSelectionMode.SingleItem)
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
            throw new BasicBlankException("Should have used SelectSeveralItems for selecting several items");
        }
        public void ShowOnlyOneSelectedItem(string text)
        {
            if (SelectionMode == EnumSelectionMode.MultipleItems)
            {
                throw new BasicBlankException("Must have single selection for showing one selected item");
            }
            ListPieceModel thisPick = TextList.Single(Items => Items.DisplayText == text);
            TextList.ReplaceAllWithGivenItem(thisPick); //i think this is best.  so a person see's just one item.
        }
        public int Count()
        {
            return TextList.Count;
        }
        public void SelectSeveralItems(CustomBasicList<int> thisList)
        {
            if (SelectionMode == EnumSelectionMode.SingleItem)
            {
                throw new BasicBlankException("Cannot select several items because you chose to select only one item.");
            }
            UnselectAll();
            foreach (var thisItem in thisList)
            {
                var news = (from Items in TextList
                            where Items.Index == thisItem
                            select Items).Single();
                news.IsSelected = true;
            }
        }
        public CustomBasicList<int> GetAllSelectedItems()
        {
            if (SelectionMode == EnumSelectionMode.SingleItem)
            {
                throw new BasicBlankException("Cannot get all selected items because there was only one selected.  Try using the property SelectedIndex");
            }
            return (from Items in TextList
                    where Items.IsSelected == true
                    select Items.Index).ToCustomBasicList();
        }

        public ControlCommand ItemSelectedCommand { get; set; } //this time you have it.
        ICustomCommand IListViewPicker.ItemSelectedCommand { get => ItemSelectedCommand; }
        CustomBasicList<ListPieceModel> IListViewPicker.TextList => TextList;
        protected override void EnableChange()
        {
            TextList.SetEnabled(IsEnabled); //i think this was needed too.
        }
        protected override void PrivateEnableAlways() { }
        public int ItemToChoose(bool requiredToChoose = true, bool useHalf = true)
        {
            return _privateChoose.ItemToChoose(requiredToChoose, useHalf);
        }
    }
}