using System.Drawing;
namespace BasicGameFrameworkLibrary.BasicDrawables.Interfaces
{
    public interface ILocationDeck : IDeckObject
    {
        PointF Location { get; set; } //location will be needed for the scattering pieces.
    }
}