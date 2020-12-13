using FluxxCP.Containers;
using static CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.BasicDataFunctions;
namespace FluxxBlazor
{
    public class CompleteContainerClass
    {
        public CompleteContainerClass()
        {
            GameContainer = cons!.Resolve<FluxxGameContainer>();
            KeeperContainer = cons.Resolve<KeeperContainer>();
            ActionContainer = cons.Resolve<ActionContainer>();
            GameData = cons.Resolve<FluxxVMData>();
        }
        public FluxxGameContainer GameContainer { get; set; }
        public KeeperContainer KeeperContainer { get; set; }
        public ActionContainer ActionContainer { get; set; }
        public FluxxVMData GameData { get; set; }
    }
}