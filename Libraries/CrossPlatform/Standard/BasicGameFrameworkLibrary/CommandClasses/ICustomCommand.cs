namespace BasicGameFrameworkLibrary.CommandClasses
{
    public interface ICustomCommand : IAsyncCommand //this eventually has to be put into the standard functions library.
    {
        void ReportCanExecuteChange(); //in order to keep compatibility with old, has to do as void.
        object Context { get; }
    }
}