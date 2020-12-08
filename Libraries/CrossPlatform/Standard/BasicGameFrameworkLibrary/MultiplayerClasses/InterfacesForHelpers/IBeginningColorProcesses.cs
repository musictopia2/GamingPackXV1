using System;
using System.Threading.Tasks;
namespace BasicGameFrameworkLibrary.MultiplayerClasses.InterfacesForHelpers
{
    public interface IBeginningColorProcesses<E>
        where E : struct, Enum
    {
        Task ChoseColorAsync(E colorChosen);
        Task InitAsync();
        Action<string>? SetTurn { get; set; }
        Action<string>? SetInstructions { get; set; }
    }
}