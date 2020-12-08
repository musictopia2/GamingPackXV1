using BasicGameFrameworkLibrary.SpecializedGameTypes.YahtzeeStyleHelpers.Containers;
using CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.BasicExtensions;
using System.Linq;
namespace BasicGameFrameworkLibrary.SpecializedGameTypes.YahtzeeStyleHelpers.Logic
{
    public static class ScoreExtensions
    {
        public static bool HasFullHouse(this ScoreContainer scoreContainer)
        {
            var tempList = scoreContainer.DiceList.GroupOrderDescending(items => items.Value);
            if (tempList.Count() != 2)
                return false;
            if (tempList.First().Count() != 3)
                return false;
            return true;
        }
        public static bool HasAllFive(this ScoreContainer scoreContainer)
        {
            int count = scoreContainer.DiceList.MaximumDuplicates(items => items.Value);
            return count == 5;
        }
        public static bool HasKind(this ScoreContainer scoreContainer, int howMany)
        {
            int count = scoreContainer.DiceList.MaximumDuplicates(items => items.Value);
            return count >= howMany;
        }
        public static bool HasStraight(this ScoreContainer scoreContainer, bool smallOnly)
        {
            var tempList = scoreContainer.DiceList.OrderBy(items => items.Value).GroupBy(items => items.Value).ToCustomBasicList();
            if (tempList.Count != 5 && smallOnly == false)
                return false;
            if (tempList.Count < 4)
                return false;
            if (tempList.Count == 5)
            {
                if (tempList.First().Key == 1 && tempList.Last().Key == 6 && smallOnly == false)
                    return false;
                else if (smallOnly == false)
                    return true;
                bool rets = false;
                for (int x = 1; x <= 3; x++)
                {
                    for (int y = x; y <= x + 3; y++)
                    {
                        rets = tempList.Any(Items => Items.Key == y);
                        if (rets == false)
                            break;
                    }
                    if (rets == true)
                        return true;
                }
                return false;
            }
            for (int x = 1; x <= 3; x++)
            {
                if (tempList.First().Key == x && tempList.Last().Key == x + 3)
                    return true;
            }
            return false;
        }
        public static int CalculateDiceTotal(this ScoreContainer scoreContainer) => scoreContainer.DiceList.Sum(x => x.Value);
    }
}