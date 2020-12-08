namespace BasicGameFrameworkLibrary.CommonInterfaces
{
    public interface IPointsObject
    {
        int GetPoints { get; } //needs to be readonly.  if something is in there, its okay.  but whoever is using it only needs to know the points
    }
}