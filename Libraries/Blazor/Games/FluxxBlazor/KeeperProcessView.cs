using FluxxCP.ViewModels;
namespace FluxxBlazor
{
    public abstract class KeeperProcessView<K> : KeeperBaseView<K>
        where K: class
    {
        protected override EnumKeeperCategory KeeperCategory => EnumKeeperCategory.Process;
        protected override sealed string CommandText => nameof(KeeperActionViewModel.ProcessAsync);
    }
}