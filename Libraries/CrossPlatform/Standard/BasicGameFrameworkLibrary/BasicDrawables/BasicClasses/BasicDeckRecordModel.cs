namespace BasicGameFrameworkLibrary.BasicDrawables.BasicClasses
{
    /// <summary>
    /// this is used to determine if something having to do with deck stuff changed.  needed for performance improvements on some uis.  however, this does not care what ui is used.
    /// </summary>
    public record BasicDeckRecordModel(int Deck, bool IsSelected, bool Drew, bool IsUnknown, bool IsEnabled, bool Visible, string Tag = "");
    //attempt no more visible because something else should handle the visible part so no disposing of anything.
}