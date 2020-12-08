using BasicGameFrameworkLibrary.CommandClasses;
using BasicGamingUIBlazorLibrary.Helpers;
using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
using CommonBasicStandardLibraries.CollectionClasses;
using CommonBasicStandardLibraries.Exceptions;
using CommonBasicStandardLibraries.MVVMFramework.UIHelpers;
using Microsoft.AspNetCore.Components;
using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using aa = CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.BasicDataFunctions;
using cc = CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.SColorString;
namespace BasicGamingUIBlazorLibrary.BasicControls.SimpleControls
{
    public partial class GameButtonComponent : IDisposable
    {
        [Parameter]
        public string MethodName { get; set; } = ""; //this is used for the reflection command.

        [Parameter]
        public string VisibleName { get; set; } = "";

        [Parameter]
        public string Width { get; set; } = "";

        [Parameter]
        public string Display { get; set; } = "";

        [Parameter]
        public object? DataContext { get; set; }

        [Parameter]
        public object? CommandParameter { get; set; }

        [Parameter]
        public ICustomCommand? CommandObject { get; set; } //even this time sometimes you already have the command.

        [Parameter]
        public Action? AfterChange { get; set; } //hopefully this simple.

        [Parameter]
        public string FontSize { get; set; } = "3vh"; //i like the idea of default to based on view height.
        [Parameter]
        public string BackgroundColor { get; set; } = cc.Aqua; //defaults to aqua.  however, sometimes, we need a button that has a different like minesweeper game.
        /// <summary>
        /// needs to have the option that if true, then starts on new line for a button.
        /// default has to be false.
        /// </summary>
        [Parameter]
        public bool StartOnNewLine { get; set; } = false;

        [Parameter]
        public bool Hints { get; set; } = false;

        private bool IsVisible()
        {
            if (_visibleProperty == null)
            {
                return true;
            }
            //hint.  if i wanted to have a basic submit, then most likely would create a new control but just send into this.
            if (DataContext == null || MethodName == "")
            {
                return false; //because no data.
            }
            return (bool)_visibleProperty.GetValue(DataContext);
        }
        private string ExtraInfo()
        {
            if (Width == "")
            {
                return "";
            }
            return $"width: {Width};";
        }
        private string GetTextColor()
        {
            if (IsDisabled())
            {
                return cc.Gray.ToWebColor();
            }
            return cc.Navy.ToWebColor();
        }
        private bool IsDisabled()
        {
            if (_command == null)
            {
                return true;
            }
            return !_command.CanExecute(CommandParameter); //this time, no problem doing bang character.  unfortunately, for buttons, they chose disabled, not isenabled.
        }
        private PropertyInfo? _visibleProperty;

        private ICustomCommand? _command;

        private async Task Submit()
        {
            if (_command == null)
            {
                return; //nothing to submit
            }
            if (_command.CanExecute(CommandParameter) == false)
            {
                return;
            }
            await _command.ExecuteAsync(CommandParameter);
        }
        private string _privateMethod = "";
        protected override void OnParametersSet()
        {
            if (DataContext != null && MethodName != "")
            {
                if (MethodName == _privateMethod)
                {
                    return; //try this so it does not have to keep redoing the reflection since you already have it.
                }
                var viewType = DataContext.GetType();
                var properties = viewType.GetProperties().ToCustomBasicList();
                var methods = viewType.GetMethods().ToCustomBasicList();
                CustomBasicList<MethodInfo> specialList = methods.Where(x => x.HasOpenAttribute()).ToCustomBasicList();
                MethodInfo? openRealMethod = specialList.SingleOrDefault();
                var pList = methods.Where(x => x.Name.StartsWith("Can")).ToCustomBasicList();
                methods.KeepConditionalItems(x => x.HasCommandAttribute()); //because only
                if (methods.Any(x =>
                {
                    if (x.ReturnType.Name != "Task" && x.ReturnType.Name != "Void")
                    {
                        return true;
                    }
                    return false;
                }))
                {
                    throw new BasicBlankException("Cannot put as command if the return type of any is not void or task.  Rethink");
                }
                PropertyInfo? openFunProperty = null;
                if (openRealMethod != null)
                {
                    openFunProperty = properties.Where(x => x.Name == "CanOpenChild").SingleOrDefault();
                    if (openFunProperty == null)
                    {
                        throw new BasicBlankException("Did not detect CanOpenChild function in the view model.  Rethink");
                    }
                }
                var method = methods.SingleOrDefault(x => x.Name == $"{MethodName}Async");
                if (method == null)
                {
                    method = methods.Single(x => x.Name == MethodName); //since i use nameof, no excuse for misspellings even case.
                }
                if (VisibleName != "")
                {
                    _visibleProperty = properties.Single(x => x.Name == VisibleName);
                }
                bool isOpenChild = method.Equals(openRealMethod);
                string tempName = MethodName;
                tempName = tempName.Replace("Async", ""); //so if async, still can pick it up.
                var foundProperty = properties.FirstOrDefault(x => x.Name == "Can" + tempName);
                MethodInfo? validateM = null;
                if (foundProperty == null && isOpenChild == false)
                {
                    validateM = pList.FirstOrDefault(x => x.Name == "Can" + tempName);
                }
                else if (isOpenChild == true && openFunProperty != null)
                {
                    foundProperty = openFunProperty;
                }
                if (foundProperty != null && validateM != null)
                {
                    throw new BasicBlankException("Cannot have the can for both property and method.  Rethink");
                }
                _privateMethod = MethodName;
                _command = BindingHelpers.GetGameCommand(DataContext, method, validateM, foundProperty, AfterChange);
            }
            else if (CommandObject != null)
            {
                _command = CommandObject;
            }
            base.OnParametersSet();
        }

        public void Dispose()
        {
            CommandContainer command = aa.cons!.Resolve<CommandContainer>();
            if (_command is IGameCommand other)
            {
                command.RemoveCommand(other, AfterChange!);
            }
            else
            {
                command.RemoveAction(AfterChange!);
            }
        }
    }
}