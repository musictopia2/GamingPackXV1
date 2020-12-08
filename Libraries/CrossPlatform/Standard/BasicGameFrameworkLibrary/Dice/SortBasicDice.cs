using System.Collections.Generic;
namespace BasicGameFrameworkLibrary.Dice
{
    public class SortBasicDice<D> : IComparer<D> where D : IStandardDice, new()
    {
        public bool IsDescending;
        public int Compare(D x, D y)
        {
            if (IsDescending == false)
            {
                return x.Value.CompareTo(y.Value);
            }
            return y.Value.CompareTo(x.Value);
        }
    }
}