using BasicGameFrameworkLibrary.CommandClasses;
using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
using CommonBasicStandardLibraries.CollectionClasses;
using CommonBasicStandardLibraries.Exceptions;
using System;
using System.Linq;
using System.Reflection;
namespace BasicGameFrameworkLibrary.Extensions
{
    public static class CommandExtensions
    {
        public static CustomBasicList<BoardCommand> GetBoardCommandList(this ISeveralCommands vm)
        {
            CustomBasicList<BoardCommand> output = new CustomBasicList<BoardCommand>();
            Type type = vm.GetType();
            CustomBasicList<MethodInfo> methods = type.GetMethods().ToCustomBasicList(); //decided to just show all methods period.
            methods.ForEach(x =>
            {
                output.Add(new BoardCommand(vm, x, vm.BlazorAction, vm.Command, x.Name));
            });
            BoardCommand board = output.First();
            return output;
        }
        public static bool CanExecuteBasics(this CommandContainer command)
        {
            if (command.IsExecuting == true || command.Processing)
            {
                return false;
            }
            return true;
        }
        private static MethodInfo? GetPrivateMethod(this object payLoad, string name)
        {
            Type type = payLoad.GetType();
            MethodInfo output = type.GetMethod(name, BindingFlags.Instance | BindingFlags.NonPublic);
            if (output != null)
            {
                return output;
            }
            output = type.GetMethod(name);
            if (output != null)
            {
                return output;
            }
            type = type.BaseType;
            output = type.GetMethod(name, BindingFlags.Instance | BindingFlags.NonPublic);
            return output;
        }
        public static PlainCommand GetPlainCommand(this ISeveralCommands payLoad, string name)
        {
            MethodInfo? method = payLoad.GetPrivateMethod(name);
            if (method == null)
            {
                Type type = payLoad.GetType();
                throw new BasicBlankException($"Method with the name of {name} was not found  Type was {type.Name}");
            }
            PlainCommand output = new PlainCommand(payLoad, method, canExecuteM: null!, payLoad.BlazorAction, payLoad.Command);
            return output;
        }
    }
}