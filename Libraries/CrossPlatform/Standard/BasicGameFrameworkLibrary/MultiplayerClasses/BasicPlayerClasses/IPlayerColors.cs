namespace BasicGameFrameworkLibrary.MultiplayerClasses.BasicPlayerClasses
{
    public interface IPlayerColors : IPlayerItem
    {
        bool DidChooseColor { get; }
        /// <summary>
        /// this is to show no color was chosen.
        /// </summary>
        void Clear();
    }
}