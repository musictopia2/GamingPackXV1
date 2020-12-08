using BasicGameFrameworkLibrary.BasicGameDataClasses;
using BasicGameFrameworkLibrary.MultiplayerClasses.BasicPlayerClasses;
namespace BasicGameFrameworkLibrary.MultiplayerClasses.Extensions
{
    public static class MessageHelper
    {
        public static bool CanSendMessage<P>(this P singleInfo, BasicData thisData) where P : class, IPlayerItem, new()
        {
            if (thisData.MultiPlayer == false)
            {
                return false;
            }
            if (singleInfo.PlayerCategory == EnumPlayerCategory.Self)
            {
                return true;
            }
            if (thisData.Client == false && singleInfo.PlayerCategory == EnumPlayerCategory.Computer)
            {
                return true;
            }
            return false;
        }
    }
}