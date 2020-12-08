namespace BasicGameFrameworkLibrary.RegularDeckOfCards
{
    public interface IGenerateRegularCard<R> where R : IRegularCard // you always send in a deck for this one.
    {
        R GetRegularCard(int chosen);
    }
}