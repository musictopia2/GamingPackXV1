using BasicGameFrameworkLibrary.Dominos;
using System.Threading.Tasks;
namespace BasicGameFrameworkLibrary.MultiplayerClasses.InterfacesForHelpers
{
    public interface IDominoDrawProcesses<D> : IEndTurn
        where D : IDominoInfo, new()
    {
        Task DrawDominoAsync(D domino);
    }
}