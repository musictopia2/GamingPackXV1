using System.Threading.Tasks; //most of the time, i will be using asyncs.
namespace BasicGameFrameworkLibrary.SolitaireClasses.MiscClasses
{
    public delegate Task WastePileSelectedEventHandler(int index);
    public delegate Task WasteDoubleClickEventHandler(int index);
    public delegate Task MainPileClickedEventHandler(int index);
}