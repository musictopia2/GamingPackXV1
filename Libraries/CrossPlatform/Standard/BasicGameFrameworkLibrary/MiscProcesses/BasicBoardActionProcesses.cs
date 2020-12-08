using BasicGameFrameworkLibrary.CommandClasses;
using BasicGameFrameworkLibrary.Extensions;
using CommonBasicStandardLibraries.CollectionClasses;
using CommonBasicStandardLibraries.Exceptions;
using System;
using System.Linq;
namespace BasicGameFrameworkLibrary.MiscProcesses
{
    /// <summary>
    /// the purpose of this class is to allow cases where a gameboard can have several actions.  this handles processing those actions.
    /// </summary>
    public abstract class BasicBoardActionProcesses : ISeveralCommands
    {
        private readonly CustomBasicList<BoardCommand> _boardList;
        public BoardCommand GetCommand(string method)
        {
            var output = _boardList.SingleOrDefault(x => x.Name == method);
            if (output == null)
            {
                throw new BasicBlankException($"No method was found with the name of {method}.  Rethink");
            }
            return output;
        }

        public BasicBoardActionProcesses(CommandContainer command, Action action)
        {
            BlazorAction = action;
            Command = command;
            _boardList = this.GetBoardCommandList();
        }

        public CommandContainer Command { get; set; }
        public Action BlazorAction { get; set; }
    }
}