using BasicGameFrameworkLibrary.NetworkingClasses.Data;
namespace GamePackageSignalRClasses
{

    public record CustomEventHandler(EnumNetworkCategory Category, string Message = "");
}