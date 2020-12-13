using System.Threading.Tasks;
namespace UnoCP.Logic
{
    public interface ISayUnoProcesses
    {
        Task ProcessUnoAsync(bool saiduno);
    }
}