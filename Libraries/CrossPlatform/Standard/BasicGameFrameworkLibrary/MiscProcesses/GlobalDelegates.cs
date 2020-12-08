using CommonBasicStandardLibraries.Messenging;
using System;
namespace BasicGameFrameworkLibrary.MiscProcesses
{
    public static class GlobalDelegates
    {
        public static Action<IEventAggregator>? RefreshSubscriptions { get; set; }
    }
}