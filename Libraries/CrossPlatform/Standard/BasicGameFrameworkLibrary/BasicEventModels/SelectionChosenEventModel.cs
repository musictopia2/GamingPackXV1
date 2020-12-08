namespace BasicGameFrameworkLibrary.BasicEventModels
{
    /// <summary>
    /// this is used with the warning.  what happens is as follows:
    /// first, a warning is sent to the ui.
    /// the ui decides how to popup the option dialog.
    /// they choose the option.
    /// the ui then publishes the selection chosen by user.
    /// </summary>
    public class SelectionChosenEventModel
    {
        public EnumOptionChosen OptionChosen { get; set; }
    }
}