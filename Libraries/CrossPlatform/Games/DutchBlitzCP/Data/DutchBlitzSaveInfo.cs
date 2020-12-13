using BasicGameFrameworkLibrary.Attributes;
using BasicGameFrameworkLibrary.MultiplayerClasses.SavedGameClasses;
using DutchBlitzCP.Cards;
namespace DutchBlitzCP.Data
{
    [SingletonGame]
    public class DutchBlitzSaveInfo : BasicSavedCardClass<DutchBlitzPlayerItem, DutchBlitzCardInformation> { }
}