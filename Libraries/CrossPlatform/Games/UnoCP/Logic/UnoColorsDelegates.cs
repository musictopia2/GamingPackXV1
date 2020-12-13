using BasicGameFrameworkLibrary.Attributes;
using System;
using System.Threading.Tasks;
namespace UnoCP.Logic
{
    [SingletonGame]
    public class UnoColorsDelegates
    {
        internal Func<Task>? CloseColorAsync { get; set; }
        internal Func<Task>? OpenColorAsync { get; set; }
    }
}