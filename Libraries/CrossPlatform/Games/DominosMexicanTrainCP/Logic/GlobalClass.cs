using BasicGameFrameworkLibrary.AnimationClasses;
using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.Dominos;
using DominosMexicanTrainCP.Data;
namespace DominosMexicanTrainCP.Logic
{
    [SingletonGame]
    [AutoReset]
    public class GlobalClass
    {
        public AnimateBasicGameBoard? Animates;
        public MexicanDomino? MovingDomino { get; set; }
        internal DominosBoneYardClass<MexicanDomino>? BoneYard { get; set; }
        internal TrainStationBoardProcesses? TrainStation1 { get; set; }
    }
}