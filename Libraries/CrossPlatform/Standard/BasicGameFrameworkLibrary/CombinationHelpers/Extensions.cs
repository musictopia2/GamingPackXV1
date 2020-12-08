using CommonBasicStandardLibraries.CollectionClasses;
namespace BasicGameFrameworkLibrary.CombinationHelpers
{
    public static class Extensions
    {
        public static CustomBasicList<CustomBasicList<T>> GetCombinations<T>(this CustomBasicList<T> thisList, int howMany)
        {
            CustomBasicList<CustomBasicList<int>> tempList;
            tempList = Combination.CheckScores(thisList.Count, howMany);
            CustomBasicList<CustomBasicList<T>> newList = new CustomBasicList<CustomBasicList<T>>();
            tempList.ForEach(firstItem =>
            {
                var fins = new CustomBasicList<T>();
                firstItem.ForEach(secondItem =>
                {
                    fins.Add(thisList[secondItem - 1]);
                });
                newList.Add(fins);
            });
            return newList;
        }
        public static CustomBasicList<CustomBasicList<T>> GetAllPossibleCombinations<T>(this CustomBasicList<T> thisList)
        {
            CustomBasicList<CustomBasicList<T>> finList = new CustomBasicList<CustomBasicList<T>>();
            var loopTo = thisList.Count;
            int x;
            for (x = 1; x <= loopTo; x++)
            {
                CustomBasicList<CustomBasicList<T>> tempList = thisList.GetCombinations(x);
                finList.AddRange(tempList);
            }
            return finList;
        }
    }
}