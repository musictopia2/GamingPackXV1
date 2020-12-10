using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.CommonInterfaces;
using BasicGameFrameworkLibrary.DrawableListsObservable;
using BasicGameFrameworkLibrary.Extensions;
using BasicGameFrameworkLibrary.SolitaireClasses.Cards;
using BasicGameFrameworkLibrary.SolitaireClasses.DataClasses;
using BasicGameFrameworkLibrary.SolitaireClasses.MainClasses;
using CommonBasicStandardLibraries.Messenging;
using CommonBasicStandardLibraries.MVVMFramework.UIHelpers;
using EagleWingsSolitaireCP.Data;
using System.Linq;
using System.Threading.Tasks; //most of the time, i will be using asyncs.
namespace EagleWingsSolitaireCP.Logic
{
    [SingletonGame]
    public class EagleWingsSolitaireMainGameClass : SolitaireGameClass<EagleWingsSolitaireSaveInfo>
    {
        public EagleWingsSolitaireMainGameClass(ISolitaireData solitaireData1,
            ISaveSinglePlayerClass thisState,
            IEventAggregator aggregator,
            IScoreData score,
            CommandContainer command
            )
            : base(solitaireData1, thisState, aggregator, score, command)
        {
        }
        protected override SolitaireCard CardPlayed()
        {
            var thisCard = base.CardPlayed();
            if (thisCard.Deck > 0)
            {
                if (GlobalClass.MainModel!.Heel1.IsSelected)
                {
                    return new SolitaireCard();
                }
                return thisCard;
            }
            thisCard = GlobalClass.MainModel!.Heel1!.DrawCard();
            thisCard.IsSelected = false;
            return thisCard;
        }

        protected override async Task<bool> HasOtherAsync(int pile)
        {
            if (GlobalClass.MainModel!.Heel1!.CardsLeft() != 1 || GlobalClass.MainModel.Heel1.IsSelected == false)
            {
                return await base.HasOtherAsync(pile);
            }
            int wastes = _thisMod!.WastePiles1!.OneSelected();
            if (wastes > -1)
            {
                await UIPlatform.ShowMessageAsync("Can choose either the waste pile or from heel; but not both");
                return true;
            }
            await UIPlatform.ShowMessageAsync("Cannot play from heel to wing since its the last card");
            return true;
        }
        public async Task HeelToMainAsync()
        {
            if (_thisMod!.WastePiles1!.OneSelected() > -1)
            {
                return;
            }
            int index = ValidMainColumn(GlobalClass.MainModel!.Heel1.RevealCard());
            if (index == -1)
            {
                GlobalClass.MainModel.Heel1.IsSelected = false;
                return;
            }
            var thisCard = GlobalClass.MainModel.Heel1.DrawCard();
            await FinishAddingToMainPileAsync(index, thisCard);
        }
        protected override void AfterShuffleCards()
        {
            var thisCol = CardList.Take(13).ToRegularDeckDict();
            CardList!.RemoveRange(0, 13);
            GlobalClass.MainModel!.Heel1.DeckStyle = EnumDeckPileStyle.Unknown;
            GlobalClass.MainModel.Heel1.OriginalList(thisCol);
            thisCol = CardList.Take(1).ToRegularDeckDict();
            CardList.RemoveRange(0, 1);
            AfterShuffle(thisCol);
            CardList.Clear(); //try this way.  hopefully i won't regret this.  because otherwise, the new game goes not work.
        }
        protected async override Task ContinueOpenSavedAsync()
        {
            //anything else you need will be here
            var newList = SaveRoot.HeelList.GetNewObjectListFromDeckList(DeckList);
            GlobalClass.MainModel!.Heel1.OriginalList(newList);
            if (newList.Count == 1)
            {
                GlobalClass.MainModel.Heel1.DeckStyle = EnumDeckPileStyle.AlwaysKnown;
            }
            else
            {
                GlobalClass.MainModel.Heel1.DeckStyle = EnumDeckPileStyle.Unknown;
            }
            await base.ContinueOpenSavedAsync();
        }
        protected async override Task FinishSaveAsync()
        {
            SaveRoot.HeelList = GlobalClass.MainModel!.Heel1.GetCardIntegers();
            await base.FinishSaveAsync();
        }
    }
}