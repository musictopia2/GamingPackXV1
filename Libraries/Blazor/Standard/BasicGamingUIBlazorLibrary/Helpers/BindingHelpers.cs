using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.ViewModelInterfaces;
using CommonBasicStandardLibraries.Exceptions;
using CommonBasicStandardLibraries.MVVMFramework.Attributes;
using System;
using System.Reflection;
namespace BasicGamingUIBlazorLibrary.Helpers
{
    public static class BindingHelpers
    {
        public static bool HasCommandAttribute(this MethodInfo method)
        {
            var item = method.GetCustomAttribute<CommandAttribute>();
            return item != null;
        }
        public static bool HasOpenAttribute(this MethodInfo method)
        {
            var item = method.GetCustomAttribute<OpenChildAttribute>();
            return item != null;
        }
        //decided we need customcommand period now.
        public static ICustomCommand GetGameCommand(object viewModel, MethodInfo method, MethodInfo? validateM, PropertyInfo? foundProperty, Action? action)
        {
            var item = method.GetCustomAttribute<CommandAttribute>();
            if (item == null)
            {
                throw new BasicBlankException("Was not even a custom command.  Rethink");
            }
            ICustomCommand? output;
            if (!(viewModel is IBlankGameVM blank))
            {
                throw new BasicBlankException("This is not a blank game view model.  Rethink");
            }
            if (blank.CommandContainer == null)
            {
                throw new BasicBlankException("The command container for command not there.  Rethink");
            }
            switch (item.Category)
            {
                case EnumCommandCategory.Plain:
                    if (foundProperty == null && validateM != null)
                    {
                        output = new PlainCommand(viewModel, method, validateM, action!, blank.CommandContainer);
                    }
                    else
                    {
                        output = new PlainCommand(viewModel, method, foundProperty!, action!, blank.CommandContainer);
                    }
                    break;
                case EnumCommandCategory.Game:
                    if (!(viewModel is IBasicEnableProcess basics))
                    {
                        throw new BasicBlankException("You need to implement the IEnableAlways in order to use out of turn command.  Rethink");
                    }
                    if (foundProperty == null && validateM != null)
                    {
                        output = new BasicGameCommand(basics, method, validateM, action!, blank.CommandContainer);
                    }
                    else
                    {
                        output = new BasicGameCommand(basics, method, foundProperty!, action!, blank.CommandContainer);
                    }
                    break;
                case EnumCommandCategory.Limited:
                    if (!(viewModel is IBasicEnableProcess basics2))
                    {
                        throw new BasicBlankException("You need to implement the IEnableAlways in order to use out of turn command.  Rethink");
                    }
                    if (foundProperty == null && validateM != null)
                    {
                        output = new LimitedGameCommand(basics2, method, validateM, action!, blank.CommandContainer);
                    }
                    else
                    {
                        output = new LimitedGameCommand(basics2, method, foundProperty!, action!, blank.CommandContainer);
                    }
                    break;
                case EnumCommandCategory.OutOfTurn:

                    if (!(viewModel is IEnableAlways enables))
                    {
                        throw new BasicBlankException("You need to implement the IEnableAlways in order to use out of turn command.  Rethink");
                    }

                    output = new OutOfTurnCommand(enables, method, action!, blank.CommandContainer);
                    break;
                case EnumCommandCategory.Open:
                    if (foundProperty == null && validateM != null)
                    {
                        output = new OpenCommand(viewModel, method, validateM, action!, blank.CommandContainer);
                    }
                    else
                    {
                        output = new OpenCommand(viewModel, method, foundProperty!, action!, blank.CommandContainer);
                    }
                    break;
                case EnumCommandCategory.Control:
                    if (!(viewModel is IControlObservable control))
                    {
                        throw new BasicBlankException("You need to implement the IControlVM in order to use control command.  Rethink");
                    }
                    output = new ControlCommand(control, method, blank.CommandContainer);
                    break;
                case EnumCommandCategory.Old:
                    if (foundProperty == null && validateM != null)
                    {
                        output = new OldReflectiveCommand(viewModel, method, validateM, action!);
                    }
                    else
                    {
                        output = new OldReflectiveCommand(viewModel, method, foundProperty!, action!);
                    }
                    break;
                default:
                    throw new BasicBlankException("Not supported");
            }
            if (output == null)
            {
                throw new BasicBlankException("No command.   Rethink");
            }
            return output;
        }
    }
}