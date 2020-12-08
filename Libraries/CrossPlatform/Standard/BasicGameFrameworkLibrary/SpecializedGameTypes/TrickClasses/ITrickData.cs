namespace BasicGameFrameworkLibrary.SpecializedGameTypes.TrickClasses
{
    public interface ITrickData
    {
        bool FirstPlayerAnySuit { get; }
        bool FollowSuit { get; }
        bool MustFollow { get; } //some games you have to follow.  some games you don't
        bool HasTrump { get; }
        bool MustPlayTrump { get; } //if set to true, then games like california jack will require you to play trumps if you have it.
        EnumTrickStyle TrickStyle { get; }
        bool HasDummy { get; } //if this is true, then extra things has to happen.
    }
}