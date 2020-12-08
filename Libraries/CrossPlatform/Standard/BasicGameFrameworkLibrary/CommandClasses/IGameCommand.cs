namespace BasicGameFrameworkLibrary.CommandClasses
{
    public interface IGameCommand : ICustomCommand
    {
        EnumCommandBusyCategory BusyCategory { get; set; }
    }
}