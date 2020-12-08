using System;
namespace BasicGameFrameworkLibrary.CommandClasses
{
    public interface ISeveralCommands //this is needed for some extensions.
    {
        CommandContainer Command { get; set; }
        Action BlazorAction { get; set; }
    }
}