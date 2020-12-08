using BasicGameFrameworkLibrary.MultiplayerClasses.BasicPlayerClasses;
namespace BasicGameFrameworkLibrary.MultiplayerClasses.Extensions
{
    public static class ColorHelperClass
    {
        public static bool DidChooseColors<P>(this PlayerCollection<P> players)
            where P : class, IPlayerColors, new()
        {
            foreach (var player in players)
                if (player.DidChooseColor == false && player.InGame)
                    return false;
            return true;
        }
        public static void EraseColors<P>(this PlayerCollection<P> players)
            where P : class, IPlayerColors, new()
        {
            players!.ForEach(items =>
            {
                items.Clear();
            });
        }
    }
}