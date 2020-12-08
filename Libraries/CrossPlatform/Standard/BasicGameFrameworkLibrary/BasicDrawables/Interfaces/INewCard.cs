namespace BasicGameFrameworkLibrary.BasicDrawables.Interfaces
{
    public interface INewCard<D> where D : IDeckObject, new()
    {
        D GetNewCard(int chosen); //will be a function.  which means games like fluxx can return a different card based on what is chosen.
    }
}