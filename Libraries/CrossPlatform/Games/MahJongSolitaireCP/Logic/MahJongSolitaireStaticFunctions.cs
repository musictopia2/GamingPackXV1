using CommonBasicStandardLibraries.CollectionClasses;
using MahJongSolitaireCP.Data;
using System.Threading.Tasks;
using js = CommonBasicStandardLibraries.AdvancedGeneralFunctionsAndProcesses.JsonSerializers.NewtonJsonStrings; //just in case i need those 2.
namespace MahJongSolitaireCP.Logic
{
    public static class MahJongSolitaireStaticFunctions
    {

        private static async Task<CustomBasicList<BoardInfo>> GetPreviousListAsync(CustomBasicList<BoardInfo> BoardList)
        {
            string ThisStr = await js.SerializeObjectAsync(BoardList);
            return await js.DeserializeObjectAsync<CustomBasicList<BoardInfo>>(ThisStr);
        }

        public static async Task SaveMoveAsync(MahJongSolitaireSaveInfo Games)
        {
            Games.PreviousList = await GetPreviousListAsync(Games.BoardList);
        }

    }
}