using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.GamePieceModels;
using BasicGameFrameworkLibrary.MiscProcesses;
using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
using CommonBasicStandardLibraries.CollectionClasses;
using CommonBasicStandardLibraries.Exceptions;
using System;
using System.Reflection;
using System.Threading.Tasks;
namespace BasicGameFrameworkLibrary.ChooserClasses
{
    public class SimpleEnumPickerVM<E> : SimpleControlObservable
        where E : struct, Enum
    {
        public CustomBasicList<BasicPickerData<E>> ItemList = new CustomBasicList<BasicPickerData<E>>(); //since its intended to work with blazor now, we don't need the collection anymore.  could change my mind though.

        private EnumAutoSelectCategory _autoSelectCategory;
        public EnumAutoSelectCategory AutoSelectCategory
        {
            get
            {
                return _autoSelectCategory;
            }
            set
            {
                if (SetProperty(ref _autoSelectCategory, value) == true)
                {
                }
            }
        }
        private E? _itemChosen;
        public E? ItemChosen
        {
            get { return _itemChosen; }
            set
            {
                if (SetProperty(ref _itemChosen, value))
                {
                    ItemSelectionChanged!(value);
                }
            }
        }
        public event ItemClickedEventHandler? ItemClickedAsync;
        public delegate Task ItemClickedEventHandler(E piece);
        public event ItemChangedEventHandler? ItemSelectionChanged;
        public delegate void ItemChangedEventHandler(E? piece); //can't be async since property does this.
        protected CustomBasicList<BasicPickerData<E>> PrivateGetList()
        {
            CustomBasicList<E> firstList = _thisChoice.GetEnumList();
            CustomBasicList<BasicPickerData<E>> tempList = new CustomBasicList<BasicPickerData<E>>();
            firstList.ForEach(items =>
            {
                BasicPickerData<E> thisTemp = new BasicPickerData<E>();
                thisTemp.EnumValue = items;
                thisTemp.IsSelected = false;
                thisTemp.IsEnabled = IsEnabled; //start with false.  to prove the problem with bindings.
                tempList.Add(thisTemp);
            });
            return tempList;
        }
        public void LoadEntireList()
        {
            var tempList = PrivateGetList();
            ItemList.ReplaceRange(tempList);
        }
        public void LoadEntireListExcludeOne(E thisEnum)
        {
            var firstList = PrivateGetList();
            firstList.KeepConditionalItems(x => x.Equals(thisEnum) == false);
            ItemList.ReplaceRange(firstList);
        }
        private readonly IEnumListClass<E> _thisChoice;
        public ControlCommand EnumChosenCommand { get; set; }

        private async Task ProcessClickAsync(BasicPickerData<E> chosen)
        {
            if (AutoSelectCategory == EnumAutoSelectCategory.AutoEvent)
            {
                await ItemClickedAsync!.Invoke(chosen.EnumValue);
                return;
            }
            if (AutoSelectCategory == EnumAutoSelectCategory.AutoSelect)
            {
                SelectSpecificItem(chosen.EnumValue);
                ItemSelectionChanged!.Invoke(chosen.EnumValue);
                return;
            }
            throw new BasicBlankException("Not Supported");
        }
        public SimpleEnumPickerVM(CommandContainer container, IEnumListClass<E> thisChoice) : base(container)
        {
            _thisChoice = thisChoice;
            LoadEntireList();
            MethodInfo method = this.GetPrivateMethod(nameof(ProcessClickAsync));
            EnumChosenCommand = new ControlCommand(this, method, container);

        }
        protected override bool CanEnableFirst()
        {
            return AutoSelectCategory == EnumAutoSelectCategory.AutoEvent || AutoSelectCategory == EnumAutoSelectCategory.AutoSelect;
        }
        protected override void EnableChange()
        {
            EnumChosenCommand.ReportCanExecuteChange();
            ItemList.ForEach(x =>
            {
                x.IsEnabled = IsEnabled;
            });
        }
        public E SelectedItem(int index)
        {
            return ItemList[index].EnumValue;
        }
        public void UnselectAll()
        {
            ItemList.ForEach(items => items.IsSelected = false);
        }
        public void SelectSpecificItem(E optionChosen)
        {
            ItemList.ForEach(items =>
            {
                if (items.EnumValue.Equals(optionChosen) == true)
                {
                    items.IsSelected = true;
                }
                else
                {
                    items.IsSelected = false;
                }
            });
        }
        public void ChooseItem(E optionChosen)
        {
            ItemList.KeepConditionalItems(x => x.EnumValue.Equals(optionChosen));
            if (ItemList.Count != 1)
            {
                throw new BasicBlankException("Did not have just one choice for option chosen.  Rethink");
            }
        }
        public E ItemToChoose()
        {
            return ItemList.GetRandomItem().EnumValue;
        }
        protected override void PrivateEnableAlways() { }
    }
}