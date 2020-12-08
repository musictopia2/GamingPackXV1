namespace BasicGameFrameworkLibrary.BasicEventModels
{
    public class UpdateCountEventModel
    {
        public int ObjectCount { get; set; } //so for rummy games, the temp sets can communicate their count.  may not need for blazor but not sure.  might as well have it.
    }
}