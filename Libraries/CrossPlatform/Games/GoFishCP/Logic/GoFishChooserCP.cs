using BasicGameFrameworkLibrary.ChooserClasses;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.GamePieceModels;
using BasicGameFrameworkLibrary.RegularDeckOfCards;
using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
using CommonBasicStandardLibraries.CollectionClasses;
using GoFishCP.Data;
using System.Linq;
namespace GoFishCP.Logic
{
    public class GoFishChooserCP : SimpleEnumPickerVM<EnumRegularCardValueList>
    {
        public void LoadFromHandCardValues(GoFishPlayerItem thisPlayer) //its smart enough to take their hand part
        {
            var thisList = thisPlayer.MainHandList.GroupBy(items => items.Value).Select(Items => Items.Key).ToCustomBasicList();
            CustomBasicList<BasicPickerData<EnumRegularCardValueList>> tempList = new CustomBasicList<BasicPickerData<EnumRegularCardValueList>>();
            thisList.ForEach(items =>
            {
                BasicPickerData<EnumRegularCardValueList> thisPiece = new BasicPickerData<EnumRegularCardValueList>();
                thisPiece.EnumValue = items;
                thisPiece.IsEnabled = IsEnabled;
                thisPiece.IsSelected = false;
                tempList.Add(thisPiece);
            });
            ItemList.ReplaceRange(tempList);
        }
        public GoFishChooserCP(CommandContainer command) : base(command, new CardValueListChooser()) { }
    }
}