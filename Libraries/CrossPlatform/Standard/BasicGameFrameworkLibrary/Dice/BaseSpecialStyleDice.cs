using BasicGameFrameworkLibrary.CommonInterfaces;
using CommonBasicStandardLibraries.CollectionClasses;
using CommonBasicStandardLibraries.MVVMFramework.ViewModels;
using Newtonsoft.Json;
namespace BasicGameFrameworkLibrary.Dice
{
    public abstract class BaseSpecialStyleDice : ObservableObject, IStandardDice, IGenerateDice<int>, ISelectableObject
    {
        public int HeightWidth { get; } = 60; //for now does not matter.
        public EnumDiceStyle Style { get; } = EnumDiceStyle.DrawWhiteNumber;
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
        [JsonIgnore]
        public abstract CustomBasicList<int> GetPossibleList { get; }
        public abstract string DotColor { get; set; } //has to be public all the way.  otherwise, autoresume does not work.
        public abstract string FillColor { get; set; }
        public virtual void Populate(int chosen)
        {
            Value = chosen;
        }
    }
}