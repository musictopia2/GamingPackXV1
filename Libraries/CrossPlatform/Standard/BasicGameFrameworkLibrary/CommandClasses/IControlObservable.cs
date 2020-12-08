namespace BasicGameFrameworkLibrary.CommandClasses
{
    public interface IControlObservable
    {
        bool CanExecute();
        void ReportCanExecuteChange(); //not sure if we need this (may though).
        EnumCommandBusyCategory BusyCategory { get; set; }
    }
}