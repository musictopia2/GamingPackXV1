using BasicGameFrameworkLibrary.Attributes;
namespace MastermindCP.Data
{
    [SingletonGame]
    public class LevelClass
    {
        public int LevelChosen { get; set; } = 3; //this is the default one if not chosen.
    }
}