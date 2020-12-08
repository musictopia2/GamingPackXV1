using BasicGameFrameworkLibrary.CommonInterfaces;
using CommonBasicStandardLibraries.CollectionClasses;
using CommonBasicStandardLibraries.MVVMFramework.ViewModels;
using static CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.BasicDataFunctions;
using cs = CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.SColorString;
namespace BasicGameFrameworkLibrary.Dice
{
    public class SimpleDice : ObservableObject, IStandardDice, IGenerateDice<int>, ISimpleValueObject<int>
    {
        public int HeightWidth { get; } = 60; //for now does not matter.
        public string DotColor { get; set; } = cs.Black; //you have to make it public.  otherwise, you can't save the color which is needed for games like kismet.
        public string FillColor { get; set; } = cs.White;
        private int _value;
        public int Value
        {
            get { return _value; }
            set
            {
                if (SetProperty(ref _value, value))
                {
                    //can decide what to do when property changes
                }
            }
        }
        public int Index { get; set; }
        public EnumDiceStyle Style { get; } = EnumDiceStyle.Regular;
        private bool _hold;
        public bool Hold
        {
            get { return _hold; }
            set
            {
                if (SetProperty(ref _hold, value))
                {
                    //can decide what to do when property changes
                }
            }
        }
        private bool _isSelected;
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                if (SetProperty(ref _isSelected, value))
                {
                    //can decide what to do when property changes
                }
            }
        }
        private bool _visible = true;
        public bool Visible
        {
            get { return _visible; }
            set
            {
                if (SetProperty(ref _visible, value))
                {
                    //can decide what to do when property changes
                }
            }
        }
        private bool _isEnabled;
        public bool IsEnabled
        {
            get { return _isEnabled; }
            set
            {
                if (SetProperty(ref _isEnabled, value))
                {
                    //can decide what to do when property changes
                }
            }
        }
        CustomBasicList<int> IGenerateDice<int>.GetPossibleList => GetIntegerList(1, 6); //this simple.
        int ISimpleValueObject<int>.ReadMainValue => Value;
        public virtual void Populate(int Chosen)
        {
            Value = Chosen;
        }
    }
}