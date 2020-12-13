using BasicGameFrameworkLibrary.Attributes;
using System;
using System.Threading.Tasks;
namespace BladesOfSteelCP.Logic
{
    [SingletonGame]
    public class BladesOfSteelScreenDelegates
    {
        internal Func<Task>? ReloadFaceoffAsync { get; set; }
        internal Func<Task>? LoadMainGameAsync { get; set; }
    }
}