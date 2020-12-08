using CommonBasicStandardLibraries.Exceptions;
using static CommonBasicStandardLibraries.BasicDataSettingsAndProcesses.BasicDataFunctions;
namespace BasicGameFrameworkLibrary.DIContainers
{
    public static class Helpers
    {
        public static void PopulateContainer(IAdvancedDIContainer thisMain) //this is probably the best thing to do.
        {
            if (thisMain.MainContainer != null)
                return;
            if (cons == null)
            {
                throw new BasicBlankException("Never populated the di container in old static.  Rethink");
            }
            thisMain.MainContainer = (IGamePackageResolver)cons;
        }
    }
}