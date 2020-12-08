using BasicGameFrameworkLibrary.BasicDrawables.Interfaces;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.DrawableListsObservable;
using CommonBasicStandardLibraries.Exceptions;
using System;
using System.Threading.Tasks; //most of the time, i will be using asyncs.
namespace BasicGameFrameworkLibrary.SpecializedGameTypes.RummyClasses
{
    public abstract class SetInfo<S, C, R, T> : HandObservable<R>
        where S : Enum
        where C : Enum
        where R : IDeckObject, IRummmyObject<S, C>, new()
    {
        public abstract T SavedSet();
        public abstract void LoadSet(T save); //if risk pays off, does not have to deserialize again.
        private int _tempSection;
        public event SetClickedEventHandler? SetClickedAsync;
        public delegate Task SetClickedEventHandler(SetInfo<S, C, R, T> thisSet, int section);
        public new event ObjectClickedEventHandler? ObjectClickedAsync;
        public new delegate Task ObjectClickedEventHandler(SetInfo<S, C, R, T> thisSet, int deck, int section);
        protected bool CanExpandRuns; // if its not runs, then does not matter for that part
        protected virtual bool IsRun()
        {
            return CanExpandRuns; //can always override it.
        }
        public override bool HasSections => true;
        protected virtual bool CanClickMainBoard()
        {
            return true;
        }
        public virtual bool CanClickSingleObject()
        {
            if (IsRun() == false)
            {
                return true;
            }
            if (_tempSection == 0)
            {
                return true;
            }
            if (_tempSection == HandList.Count - 1)
            {
                return true;
            }
            return false;
        }
        protected override async Task ProcessObjectClickedAsync(R thisObject, int index)
        {
            if (ObjectClickedAsync == null)
            {
                return;
            }
            _tempSection = index;
            if (CanClickSingleObject() == false)
            {
                return;
            }
            int thisSection;
            if (IsRun() == true)
            {
                if (index == 0)
                {
                    thisSection = 1;
                }
                else
                {
                    thisSection = 2;
                }
            }
            else
            {
                thisSection = 1;
            }
            if (thisSection == 0)
            {
                throw new BasicBlankException("Section cannot be 0 for card clicked");
            }
            SectionClicked = 0; // reset back to 0
            await ObjectClickedAsync.Invoke(this, thisObject.Deck, thisSection);
        }
        protected override async Task PrivateBoardSingleClickedAsync()
        {
            if (SetClickedAsync == null)
            {
                return;
            }
            if (IsRun() == true)
            {
                return;
            }
            if (CanClickMainBoard() == false)
            {
                return;
            }
            int thisSection;
            thisSection = SectionClicked;
            if (CanExpandRuns == true)
            {
                thisSection = 1;// this means since it does not matter, will return 1.
            }
            SectionClicked = 0; // reset back to 0
            await SetClickedAsync.Invoke(this, thisSection);
        }
        public SetInfo(CommandContainer command) : base(command) { }
        
    }
}