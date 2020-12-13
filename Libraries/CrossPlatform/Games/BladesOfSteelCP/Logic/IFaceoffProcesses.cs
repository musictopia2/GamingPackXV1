using BasicGameFrameworkLibrary.RegularDeckOfCards;
using System.Threading.Tasks;
namespace BladesOfSteelCP.Logic
{
    public interface IFaceoffProcesses
    {
        Task FaceOffCardAsync(RegularSimpleCard card);
    }
}