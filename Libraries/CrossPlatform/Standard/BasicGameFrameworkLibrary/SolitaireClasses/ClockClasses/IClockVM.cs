using System.Threading.Tasks; //most of the time, i will be using asyncs.
namespace BasicGameFrameworkLibrary.SolitaireClasses.ClockClasses
{
    public interface IClockVM
    {
        Task ClockClickedAsync(int index); //will be 0 based.
    }
}