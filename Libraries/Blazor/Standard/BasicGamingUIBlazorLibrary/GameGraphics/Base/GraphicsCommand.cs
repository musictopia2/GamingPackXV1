using BasicBlazorLibrary.Components.Basic;
using BasicGameFrameworkLibrary.CommandClasses;
using BasicGamingUIBlazorLibrary.Helpers;
using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
using CommonBasicStandardLibraries.CollectionClasses;
using CommonBasicStandardLibraries.Exceptions;
using CommonBasicStandardLibraries.MVVMFramework.UIHelpers;
using Microsoft.AspNetCore.Components;
using SvgHelper.Blazor.Logic.Classes.Interfaces;
using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using aa = CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.BasicDataFunctions;
namespace BasicGamingUIBlazorLibrary.GameGraphics.Base
{
    public abstract class GraphicsCommand : KeyComponentBase, IDisposable
    {
        private bool _disposedValue;
        //related to the game button.  but this time only focus on the command part.
        [Parameter]
        public string MethodName { get; set; } = ""; //this is used for the reflection command.

        //hopefully the risk pays off.  trying for froggies first.
        protected Assembly GetAssembly => Assembly.GetAssembly(GetType())!;
        /// <summary>
        /// this is the view model being used.  can be main view model, deck, etc.  this is not the card, etc.
        /// </summary>
        [Parameter]
        public object? DataContext { get; set; }

        [Parameter]
        public object? CommandParameter { get; set; }

        [Parameter]
        public Action? AfterChange { get; set; } //hopefully this simple.

        [Parameter]
        public ICustomCommand? CommandObject { get; set; }

        private ICustomCommand? _command; //this way anybody that inherits it can populate if needed.

        private bool _didAdd;
        private string _privateMethod = "";

        protected virtual bool NeedsCommand()
        {
            return true; //so overrided versions can decide not to do the command for performance reasons.
        }

        protected override void OnParametersSet()
        {
            if (NeedsCommand() == false)
            {
                return;
            }
            if (CommandObject != null)
            {
                _didAdd = true;
                _command = CommandObject;
                return;
            }
            if (DataContext != null && MethodName != "")
            {
                _didAdd = true;
                if (_privateMethod == MethodName)
                {
                    return;
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

                bool isOpenChild = method.Equals(openRealMethod);
                string tempMethod = MethodName.Replace("Async", "");
                var foundProperty = properties.FirstOrDefault(x => x.Name == "Can" + tempMethod);
                MethodInfo? validateM = null;
                if (foundProperty == null && isOpenChild == false)
                {
                    validateM = pList.FirstOrDefault(x => x.Name == "Can" + tempMethod);
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
            base.OnParametersSet();
        }
        public GraphicsCommand()
        {
            CustomCanDo = PrivateTo;
        }
        private bool PrivateTo() => CanProcess;
        public Func<bool> CustomCanDo { get; set; }
        public bool CanProcess
        {
            get
            {
                if (_command == null)
                {
                    return false;
                }
                return _command.CanExecute(CommandParameter);
            }
        }
        public void CreateClick(ISvg svg) //needs to be entire svg
        {
            if (_command != null) //don't always need the datacontext anymore.
            {
                svg.EventData.ActionClicked = Clicked;
            }
        }
        private async Task Clicked(object args1, object args2)
        {
            await Submit();
        }

        //needs to be protected so i can use razor syntax to override and would still work.
        protected virtual async Task Submit() //anything may need to do this.
        {
            try
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
            catch (Exception ex)
            {
                await UIPlatform.ShowMessageAsync($"There was an error.  The error was {ex.Message}"); //try this way.
            }
            
           
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing && _didAdd)
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
                _disposedValue = true;
            }
        }
        public virtual void Dispose()
        {
            Console.WriteLine("Disposing Graphics Command");
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}