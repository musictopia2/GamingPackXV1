using BasicGameFrameworkLibrary.BasicDrawables.Interfaces;
using static CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.BasicDataFunctions;
using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGameFrameworkLibrary.MiscProcesses;
using BasicGameFrameworkLibrary.MultiplePilesObservable;
using CommonBasicStandardLibraries.Messenging;
using CommonBasicStandardLibraries.MVVMFramework.UIHelpers;
using System;
using System.Threading.Tasks; //most of the time, i will be using asyncs.
namespace BasicGameFrameworkLibrary.BasicEventModels
{
    public static class EventExtensions
    {
        public static async Task SendGameOverAsync(this IAggregatorContainer aggregator)
        {
            try
            {
                await aggregator.Aggregator.PublishAsync(new GameOverEventModel()); //problem seems to be whoever handles the game over in this case.
            }
            catch (Exception ex)
            {
                UIPlatform.ShowError(ex.Message);
            }
        }
        public static bool AnimationCompleted { get; set; } //needs to bring this back.  because otherwise the animation of cards did not work properly.  not sure why others worked but does not matter.
        public static async Task AnimateMovePiecesAsync<S>(this IEventAggregator thisE, Vector previousSpace,
            Vector moveToSpace, S temporaryObject, bool useColumn = false) where S : class
        {
            AnimatePieceEventModel<S> thisA = new AnimatePieceEventModel<S>();
            thisA.MoveToSpace = moveToSpace;
            thisA.PreviousSpace = previousSpace;
            thisA.TemporaryObject = temporaryObject;
            thisA.UseColumn = useColumn;
            await thisE.PublishAsync(thisA);
        }

        //decided to risk another breaking change.
        //since we discovered by doing the trick and the game ones we did not need the timer afterall.
        public static void RepaintBoard(this IEventAggregator thisE)
        {
            thisE.RepaintMessage(EnumRepaintCategories.Main); //if nothing is specified, then do from skiaboard.
        }
        public static void RepaintMessage(this IEventAggregator thisE, EnumRepaintCategories thisCategory)
        {
            thisE.Publish(new RepaintEventModel(), thisCategory.ToString());
        }

        #region Animation Objects Helpers
        public static async Task AnimatePlayAsync<D>(this IEventAggregator thisE,
            D thisCard, Action? finalAction = null) where D : class, IDeckObject, new()
        {
            await thisE.AnimateCardAsync(thisCard, EnumAnimcationDirection.StartUpToCard, "maindiscard", finalAction: finalAction!);
        }
        public static async Task AnimatePlayAsync<D>(this IEventAggregator thisE, D thisCard,
            EnumAnimcationDirection direction, Action? finalAction = null) where D : class, IDeckObject, new()
        {
            await thisE.AnimateCardAsync(thisCard, direction, "maindiscard", finalAction: finalAction!);
        }
        public static async Task AnimateDrawAsync<D>(this IEventAggregator thisE, D thisCard) where D : class, IDeckObject, new()
        {
            await thisE.AnimateDrawAsync(thisCard, EnumAnimcationDirection.StartCardToDown);
        }
        public static async Task AnimateDrawAsync<D>(this IEventAggregator thisE, D thisCard
            , EnumAnimcationDirection direction) where D : class, IDeckObject, new()
        {
            await thisE.AnimateCardAsync(thisCard, direction, "maindeck");
        }
        public static async Task AnimatePickUpDiscardAsync<D>(this IEventAggregator thisE, D thisCard) where D : class, IDeckObject, new()
        {
            await thisE.AnimatePickUpDiscardAsync(thisCard, EnumAnimcationDirection.StartCardToDown);
            //ResetDiscard(thisE);
        }
        public static async Task AnimatePickUpDiscardAsync<D>(this IEventAggregator thisE, D thisCard
            , EnumAnimcationDirection direction) where D : class, IDeckObject, new()
        {
            await thisE.AnimateCardAsync(thisCard, direction, "maindiscard");
            //ResetDiscard(thisE);
        }
        private static async Task CompleteAnimationAsync()
        {
            do
            {
                await Task.Delay(1);
                if (AnimationCompleted == true)
                {
                    return; //because done.
                }
            } while (true);
        }

        private static void ResetDiscard(this IEventAggregator thisE)
        {
            thisE.Publish(new ResetCardsEventModel()); //try this way.
        }
        private static bool? _useFast;
        public static async Task AnimateCardAsync<D>(this IEventAggregator thisE,
            D thisCard, EnumAnimcationDirection direction, string tag
            , BasicPileInfo<D>? pile1 = null, Action? finalAction = null) where D : class, IDeckObject, new()
        {

            //if (_useFast.HasValue == false)
            //{
            //    BasicData data = cons!.Resolve<BasicData>();
            //    _useFast = data.FastAnimation;
            //}
            //if (_useFast == true)
            //{
            //    if (finalAction != null)
            //    {
            //        finalAction.Invoke();
            //    }
            //    return; //for now, no animations
            //}
            //try with animations again.  hopefully i can get the animations to work for web assembly.
            AnimateCardInPileEventModel<D> thisA = new AnimateCardInPileEventModel<D>();
            thisA.Direction = direction;
            thisA.ThisCard = thisCard;
            thisA.ThisPile = pile1;
            AnimationCompleted = false;
            if (thisE.HandlerExistsFor(thisA.GetType(), tag, EnumActionCategory.Async))
            {
                await thisE.PublishAsync(thisA, tag, false);
                await CompleteAnimationAsync();
                //if (_useFast == false)
                //{
                //    await CompleteAnimationAsync();
                //}
                //else
                //{
                //    await Task.Delay(20); //hopefully enough now.
                //}
            }
            if (finalAction != null)
            {
                finalAction.Invoke();
            }
            ResetDiscard(thisE);
        }
        #endregion
    }
}