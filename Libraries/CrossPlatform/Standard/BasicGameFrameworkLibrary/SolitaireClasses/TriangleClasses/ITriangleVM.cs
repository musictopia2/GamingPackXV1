using BasicGameFrameworkLibrary.SolitaireClasses.Cards;
using System.Threading.Tasks; //most of the time, i will be using asyncs.
namespace BasicGameFrameworkLibrary.SolitaireClasses.TriangleClasses
{
    public interface ITriangleVM
    {
        Task CardClickedAsync(SolitaireCard thisCard);
    }
}