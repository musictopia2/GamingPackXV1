using BasicGameFrameworkLibrary.MiscProcesses;
namespace BasicGameFrameworkLibrary.GameBoardCollections
{
    public interface IBasicSpace
    {
        void ClearSpace(); //anything that clears it. this will allow the collection to have a method to clear.
        bool IsFilled(); //i think this needs to know whether its filled or not.
        Vector Vector { get; set; }
    }
}