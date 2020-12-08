using BasicGameFrameworkLibrary.MiscProcesses;
namespace BasicGameFrameworkLibrary.BasicEventModels
{
    public class AnimatePieceEventModel<T> where T : class
    {
        public Vector PreviousSpace { get; set; }
        public Vector MoveToSpace { get; set; }
        public T? TemporaryObject { get; set; }
        public bool UseColumn { get; set; } // if using columns, needs to be a little different (like for games like connect four)
    }
}