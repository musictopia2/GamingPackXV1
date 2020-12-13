using BasicGameFrameworkLibrary.BasicDrawables.Dictionary;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.DrawableListsObservable;
using BasicGameFrameworkLibrary.Extensions;
using BasicGameFrameworkLibrary.RegularDeckOfCards;
using CommonBasicStandardLibraries.Exceptions;
using System.Linq;
using System.Threading.Tasks;
namespace GolfCardGameCP.CustomPiles
{
    public class Beginnings : GameBoardObservable<RegularSimpleCard>
    {
        private readonly CommandContainer _command;
        public Beginnings(CommandContainer command) : base(command)
        {
            Columns = 2;
            Rows = 2;
            HasFrame = true;
            Text = "Dealt Cards";
            IsEnabled = true;
            Visible = true;
            IsEnabled = true;
            _command = command;
        }
        public void ClearBoard(IDeckDict<RegularSimpleCard> thisList)
        {
            if (thisList.Count != 4)
            {
                throw new BasicBlankException("The card list must have 4 cards");
            }
            thisList.ForEach(thisCard =>
            {
                thisCard.IsUnknown = true;
            });
            ObjectList.ReplaceRange(thisList);
        }
        protected override Task ClickProcessAsync(RegularSimpleCard thisObject)
        {
            thisObject.IsSelected = !thisObject.IsSelected;
            _command.UpdateAll(); //try this first.
            return Task.CompletedTask;
        }
        public bool CanContinue => ObjectList.Count(items => items.IsSelected == true) == 2;
        public void GetSelectInfo(out DeckRegularDict<RegularSimpleCard> selectList, out DeckRegularDict<RegularSimpleCard> unselectList)
        {
            selectList = ObjectList.Where(items => items.IsSelected == true).ToRegularDeckDict();
            unselectList = ObjectList.Where(items => items.IsSelected == false).ToRegularDeckDict();
            if (selectList.Count != 2 || unselectList.Count != 2)
            {
                throw new BasicBlankException("There must be 2 selected and 2 unselected cards.  Find out what happened");
            }
        }
    }
}