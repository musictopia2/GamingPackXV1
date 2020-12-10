using BasicGameFrameworkLibrary.Attributes;
namespace SpiderSolitaireCP.Data
{
    [SingletonGame]
    public class LevelClass
    {
        public int LevelChosen { get; set; } = 1;
    }
}