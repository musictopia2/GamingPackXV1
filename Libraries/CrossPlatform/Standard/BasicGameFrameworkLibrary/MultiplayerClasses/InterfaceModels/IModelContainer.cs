using BasicGameFrameworkLibrary.BasicGameDataClasses;
namespace BasicGameFrameworkLibrary.MultiplayerClasses.InterfaceModels
{
    public interface IModelContainer<M>
        where M : class, IViewModelData
    {
        M Model { get; set; }
    }
}