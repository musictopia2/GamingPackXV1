using BasicGameFrameworkLibrary.BasicDrawables.Interfaces;
namespace BasicGameFrameworkLibrary.BasicEventModels
{
    public class AnimateCardInPileEventModel<D> where D : class, IDeckObject, new()
    {
        public EnumAnimcationDirection Direction { get; set; }
        public D? ThisCard { get; set; }
        public MultiplePilesObservable.BasicPileInfo<D>? ThisPile { get; set; }
    }
}