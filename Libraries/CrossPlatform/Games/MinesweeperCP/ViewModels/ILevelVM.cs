using MinesweeperCP.Data;
namespace MinesweeperCP.ViewModels
{
    public interface ILevelVM
    {
        EnumLevel LevelChosen { get; set; }
        int HowManyMinesNeeded { get; set; }
    }
}