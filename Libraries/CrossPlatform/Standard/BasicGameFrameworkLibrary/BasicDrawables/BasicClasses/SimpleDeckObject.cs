using CommonBasicStandardLibraries.MVVMFramework.ViewModels;
using System;
using System.Drawing;
namespace BasicGameFrameworkLibrary.BasicDrawables.BasicClasses
{
    public abstract class SimpleDeckObject : ObservableObject, IEquatable<SimpleDeckObject>
    {
        protected virtual void ChangeDeck() { }

        private int _deck;
        public int Deck
        {
            get { return _deck; }
            set
            {
                if (SetProperty(ref _deck, value))
                {
                    //can decide what to do when property changes
                    ChangeDeck(); //so games like fluxx can change another property in response to this.
                }
            }
        }
        private bool _drew;
        public bool Drew
        {
            get { return _drew; }
            set
            {
                if (SetProperty(ref _drew, value))
                {
                    //can decide what to do when property changes
                }
            }
        }
        private bool _isUnknown;
        public bool IsUnknown
        {
            get { return _isUnknown; }
            set
            {
                if (SetProperty(ref _isUnknown, value))
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
        private bool _isEnabled = true;
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


        public virtual BasicDeckRecordModel GetRecord => new BasicDeckRecordModel(Deck, IsSelected, Drew, IsUnknown, IsEnabled, Visible);

        public virtual string GetKey() => Guid.NewGuid().ToString(); //most of the time, had to use a guid.

        public SizeF DefaultSize { get; set; } //needed after all.
        public bool Rotated { get; set; }
#pragma warning disable CS8765 // Nullability of type of parameter doesn't match overridden member (possibly because of nullability attributes).
        public override bool Equals(object obj)
#pragma warning restore CS8765 // Nullability of type of parameter doesn't match overridden member (possibly because of nullability attributes).
        {
            if (obj is not SimpleDeckObject temps)
            {
                return false;
            }
            return Deck.Equals(temps.Deck);
        }
#pragma warning disable CS8767 // Nullability of reference types in type of parameter doesn't match implicitly implemented member (possibly because of nullability attributes).
        public bool Equals(SimpleDeckObject other)
#pragma warning restore CS8767 // Nullability of reference types in type of parameter doesn't match implicitly implemented member (possibly because of nullability attributes).
        {
            return other != null &&
                   Deck == other.Deck;
        }
        public override int GetHashCode()
        {
            return Deck.GetHashCode();
        }
    }
}