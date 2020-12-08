using BasicGameFrameworkLibrary.SpecializedGameTypes.YahtzeeStyleHelpers.Data;
using CommonBasicStandardLibraries.CollectionClasses;
using System;
namespace BasicGameFrameworkLibrary.SpecializedGameTypes.YahtzeeStyleHelpers.Containers
{
    //this will have to be registered manually.  of course, the base class may be able to do it.
    public class ScoreContainer
    {
        //this will hold the dice list
        //can hold anything else as needed as well.
        public CustomBasicList<DiceInformation> DiceList = new CustomBasicList<DiceInformation>();
        public CustomBasicList<RowInfo> RowList = new CustomBasicList<RowInfo>();
        //for each time this loads, will repopulate from the players.
        //because dice means different for scores than real dice, it needs a special container.
        public Action? StartTurn { get; set; }
    }
}