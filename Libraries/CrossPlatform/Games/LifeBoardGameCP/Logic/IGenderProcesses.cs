using LifeBoardGameCP.Data;
using System;
using System.Threading.Tasks;
namespace LifeBoardGameCP.Logic
{
    public interface IGenderProcesses
    {
        Action<string>? SetTurn { get; set; }
        Action<string>? SetInstructions { get; set; }
        Task ChoseGenderAsync(EnumGender gender);
    }
}