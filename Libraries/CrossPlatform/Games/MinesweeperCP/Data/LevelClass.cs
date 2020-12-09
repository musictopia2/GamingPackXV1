namespace MinesweeperCP.Data
{
    [BasicGameFrameworkLibrary.Attributes.SingletonGame]
    public class LevelClass
    {
        public EnumLevel Level { get; set; } = EnumLevel.Easy; //default to easy.
    }
}