using System;
using System.Reflection;
namespace BasicGameFrameworkLibrary.CommandClasses
{
    public class BoardCommand : PlainCommand
    {
        public string Name { get; private set; } = ""; //the purpose of this is so it can search and get the proper command.
        public BoardCommand(object model, MethodInfo execute, Action action, CommandContainer container, string name) : base(model, execute, canExecuteM: null!, action, container)
        {
            Name = name;
        }
    }
}