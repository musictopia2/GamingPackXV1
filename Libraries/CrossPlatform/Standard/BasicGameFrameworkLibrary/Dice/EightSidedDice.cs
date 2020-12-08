using CommonBasicStandardLibraries.CollectionClasses;
using static CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.BasicDataFunctions;
using cs = CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.SColorString;
namespace BasicGameFrameworkLibrary.Dice
{
    public class EightSidedDice : BaseSpecialStyleDice
    {
        public override string DotColor { get; set; } = cs.Black;
        public override string FillColor { get; set; } = cs.Green;
        public override CustomBasicList<int> GetPossibleList => GetIntegerList(1, 8);
    }
}