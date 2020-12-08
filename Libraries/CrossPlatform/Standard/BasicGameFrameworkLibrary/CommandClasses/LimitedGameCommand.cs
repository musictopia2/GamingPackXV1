using System;
using System.Reflection;
namespace BasicGameFrameworkLibrary.CommandClasses
{
    public class LimitedGameCommand : BasicGameCommand
    {
        public LimitedGameCommand(IBasicEnableProcess model, MethodInfo execute, MethodInfo canExecuteM, Action action, CommandContainer container) : base(model, execute, canExecuteM, action, container)
        {
            BusyCategory = EnumCommandBusyCategory.Limited;
        }

        public LimitedGameCommand(IBasicEnableProcess model, MethodInfo execute, PropertyInfo? canExecute, Action action, CommandContainer container) : base(model, execute, canExecute, action, container)
        {
            BusyCategory = EnumCommandBusyCategory.Limited;
        }
    }
}