using BasicGameFrameworkLibrary.BasicDrawables.Dictionary;
using BasicGameFrameworkLibrary.BasicDrawables.Interfaces;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.DIContainers;
using BasicGameFrameworkLibrary.DrawableListsObservable;
using BasicGameFrameworkLibrary.Extensions;
using System;
using System.Threading.Tasks; //most of the time, i will be using asyncs.
namespace BasicGameFrameworkLibrary.SpecializedGameTypes.RummyClasses
{
    public class RummyHandObservable<S, C, R> : HandObservable<R>
        where S : Enum
        where C : Enum
        where R : IDeckObject, IRummmyObject<S, C>, new()
    {
        public event SetClickedEventHandler? SetClickedAsync;
        public delegate Task SetClickedEventHandler(RummyHandObservable<S, C, R> ThisSet);
        public bool DidClickObject { get; set; } = false; //sometimes this is needed for mobile.
        public void RemoveObject(int deck)
        {
            HandList.RemoveObjectByDeck(deck);
        }
        protected override Task ProcessObjectClickedAsync(R thisObject, int index)
        {
            DidClickObject = true; //this is needed too.  so if other gets raised, will be ignored because already handled.
            thisObject.IsSelected = !thisObject.IsSelected; //try here.  hopefully works well.
            return Task.CompletedTask;
        }
        protected override async Task PrivateBoardSingleClickedAsync()
        {
            if (SetClickedAsync == null)
            {
                return;
            }
            await SetClickedAsync.Invoke(this);
        }
        private ISortObjects<R>? _thisSort;
        private readonly IGamePackageResolver _resolver;

        public void AddCards(IDeckDict<R> thisList)
        {
            HandList.AddRange(thisList);
            SortCards();
            HandList.UnselectAllObjects();
        }
        private void SortCards()
        {
            if (_thisSort != null)
            {
                HandList.Sort(_thisSort);
            }
            else
            {
                HandList.Sort();
            }
        }
        private void PrepSort()
        {
            bool rets;
            rets = _resolver.RegistrationExist<ISortObjects<R>>();
            if (rets == true)
            {
                _thisSort = _resolver.Resolve<ISortObjects<R>>();
            }
        }
        public RummyHandObservable(CommandContainer container, IGamePackageResolver resolver) : base(container)
        {
            _resolver = resolver; //has to be set first.
            PrepSort();
        }
    }
}