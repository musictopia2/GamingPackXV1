using CommonBasicStandardLibraries.CollectionClasses;
using LifeBoardGameCP.Data;
using System.Threading.Tasks;
namespace LifeBoardGameCP.Logic
{
    public interface ITwinProcesses
    {
        Task GetTwinsAsync(CustomBasicList<EnumGender> twinList);
    }
}