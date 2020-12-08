using BasicGameFrameworkLibrary.CommonInterfaces;
namespace BasicGameFrameworkLibrary.Dice
{
    public interface IStandardDice : IBasicDice<int>, IHoldDice, ICommonObject
    {
        string FillColor { get; } //decided that standard dice has fill color.   something can be a dice without fill color.
        string DotColor { get; }
        EnumDiceStyle Style { get; } //maybe no need to specify dice because we know its for dice because of the class name.
    }
}