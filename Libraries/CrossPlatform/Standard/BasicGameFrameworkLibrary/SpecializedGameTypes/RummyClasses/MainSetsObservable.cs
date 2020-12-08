using BasicGameFrameworkLibrary.BasicDrawables.Interfaces;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.DrawableListsObservable;
using BasicGameFrameworkLibrary.MiscProcesses;
using CommonBasicStandardLibraries.CollectionClasses;
using CommonBasicStandardLibraries.Exceptions;
using System;
using System.Threading.Tasks;
namespace BasicGameFrameworkLibrary.SpecializedGameTypes.RummyClasses
{
    public class MainSetsObservable<SU, CO, RU, SE, T> : SimpleControlObservable
        where SU : Enum
        where CO : Enum
        where RU : IRummmyObject<SU, CO>, IDeckObject, new()
        where SE : SetInfo<SU, CO, RU, T>
    {
        public bool HasFrame { get; set; } = false; // i think default should actually be false  if i am wrong; can change
        private string _text = "Main Sets";
        public string Text
        {
            get
            {
                return _text;
            }
            set
            {
                if (SetProperty(ref _text, value) == true) { }
            }
        }
        public CustomBasicCollection<SE> SetList = new CustomBasicCollection<SE>();
        public MainSetsObservable(CommandContainer command) : base(command) { }
        public event SetClickedEventHandler? SetClickedAsync; //we may have more than one so just do old fashioned events.
        public delegate Task SetClickedEventHandler(int setNumber, int section, int deck);
        public virtual CustomBasicList<T> SavedSets()
        {
            CustomBasicList<T> output = new CustomBasicList<T>();
            foreach (var set in SetList)
            {
                output.Add(set.SavedSet());
            }
            return output;
        }
        public void ClearBoard()
        {
            if (string.IsNullOrEmpty(Text))
            {
                throw new BasicBlankException("The text cannot be blank when starting to clear the board");
            }
            foreach (var thisSet in SetList)
            {
                thisSet.ObjectClickedAsync += ThisSet_ObjectClickedAsync;
                thisSet.SetClickedAsync += ThisSet_SetClickedAsync;
            }
            SetList.Clear(); // well see.  if this breaks the bindings, will require some rethinking (?)
        }

        public override void SendEnableProcesses(IBasicEnableProcess nets, Func<bool> fun)
        {
            base.SendEnableProcesses(nets, fun);
            SetList.ForEach(x => x.SendEnableProcesses(nets, fun));
        }
        public void CreateNewSet(SE thisSet)
        {
            thisSet.ObjectClickedAsync += ThisSet_ObjectClickedAsync;
            thisSet.SetClickedAsync += ThisSet_SetClickedAsync;
            thisSet.AutoSelect = EnumHandAutoType.None;
            if (_useSpecial)
            {
                thisSet.SendEnableProcesses(_networkProcess!, _customFunction!);
            }
            SetList.Add(thisSet); // we don't have scrolltoset anymore.
        }
        protected void RemoveSet(SE thisSet)
        {
            foreach (var tempSet in SetList)
            {
                if (tempSet.Equals(thisSet))
                {
                    tempSet.ObjectClickedAsync -= ThisSet_ObjectClickedAsync;
                    tempSet.SetClickedAsync -= ThisSet_SetClickedAsync;
                    SetList.RemoveSpecificItem(thisSet);
                    return;
                }
            }
            throw new BasicBlankException("Failed to remove set.  Rethink");
        }
        private async Task ThisSet_SetClickedAsync(SetInfo<SU, CO, RU, T> thisSet, int section)
        {
            if (SetClickedAsync == null)
            {
                return;
            }
            SE tempSet = (SE)thisSet;
            await SetClickedAsync.Invoke(SetList.IndexOf(tempSet) + 1, section, 0);
        }
        private async Task ThisSet_ObjectClickedAsync(SetInfo<SU, CO, RU, T> thisSet, int deck, int section)
        {
            if (SetClickedAsync == null)
            {
                return;
            }
            SE tempSet = (SE)thisSet;
            await SetClickedAsync.Invoke(SetList.IndexOf(tempSet) + 1, section, deck);
        }
        public virtual void LoadSets(CustomBasicList<T> output)
        {
            if (SetList.Count == 0 && output.Count != 0)
            {
                throw new BasicBlankException("I think you forgot to create sets manually first.  This can't do it because of the design");
            }
            if (output.Count != SetList.Count)
            {
                throw new BasicBlankException($"Does not reconcile before loading sets  output was {output.Count} but setlist was {SetList.Count}");
            }
            int x = 0;
            SetList.ForEach(items =>
            {
                items.LoadSet(output[x]);
                x++;
            });
        }
        public void EndTurn()
        {
            SetList.ForEach(Items => Items.EndTurn());
        }
        public int HowManySets => SetList.Count;
        public SE GetIndividualSet(int index)
        {
            return SetList[index - 1];
        }
        protected override void EnableChange() { }
        protected override void PrivateEnableAlways() { }
    }
}