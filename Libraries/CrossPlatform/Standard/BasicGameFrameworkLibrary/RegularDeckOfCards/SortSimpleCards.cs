using BasicGameFrameworkLibrary.BasicDrawables.Interfaces;
using System;
namespace BasicGameFrameworkLibrary.RegularDeckOfCards
{
    public class SortSimpleCards<R> : ISortObjects<R>
        where R : IRegularCard, new()
    {
        public EnumRegularCardsSortCategory SuitForSorting { get; set; }
        public int Compare(R x, R y)
        {
            switch (SuitForSorting)
            {
                case EnumRegularCardsSortCategory.NumberSuit:
                    {
                        if (x.Value < y.Value)
                        {
                            return -1;
                        }
                        else if (x.Value > y.Value)
                        {
                            return 1;
                        }
                        else
                        {
                            if (x.Suit < y.Suit)
                            {
                                return -1;
                            }
                            else if (x.Suit > y.Suit)
                            {
                                return 1;
                            }
                            return 0;
                        }
                    }
                case EnumRegularCardsSortCategory.SuitNumber:
                    {
                        if (x.Suit < y.Suit)
                        {
                            return -1;
                        }
                        else if (x.Suit > y.Suit)
                        {
                            return 1;
                        }
                        else if (x.Value < y.Value)
                        {
                            return -1;
                        }
                        else if (x.Value > y.Value)
                        {
                            return 1;
                        }
                        return 0;
                    }
                default:
                    {
                        throw new Exception("Sorting Not Supported.  Try creating a new class and replacing registration");
                    }
            }
        }
    }
}