using BasicGameFrameworkLibrary.Attributes;
using System;
namespace FiveCrownsCP.Logic
{
    [SingletonGame]
    public class FiveCrownsDelegates
    {
        public Func<int>? CardsToPassOut { get; set; }
    }
}
