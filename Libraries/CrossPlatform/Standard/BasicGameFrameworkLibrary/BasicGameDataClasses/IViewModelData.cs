using System.ComponentModel;
namespace BasicGameFrameworkLibrary.BasicGameDataClasses
{
    /// <summary>
    /// the beginning point will be this.  the purpose of this being splitted out is so the game would no longer have access to the view model.
    /// but has access to here.
    /// obviously the view model hosting this will know about the extra data.
    /// this may even include decks, piles, etc.
    /// the view model needs to hook into somehow so if this changes,the view model can raise the proper events so anybody listening gets notified.
    /// </summary>
    public interface IViewModelData : INotifyPropertyChanged
    {
        string NormalTurn { get; set; }
        string Status { get; set; }
    }
}