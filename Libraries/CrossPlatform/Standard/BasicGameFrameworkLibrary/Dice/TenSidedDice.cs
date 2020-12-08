using CommonBasicStandardLibraries.CollectionClasses;
using static CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.BasicDataFunctions;
using cs = CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.SColorString;
namespace BasicGameFrameworkLibrary.Dice
{
    public class TenSidedDice : BaseSpecialStyleDice
    {
        public override string DotColor { get; set; } = cs.Black; //has to be public.  or autoresume does not work.  learned from kismet.
        public override string FillColor { get; set; } = cs.Blue;
        public override CustomBasicList<int> GetPossibleList => GetIntegerList(1, 10);
    }
}