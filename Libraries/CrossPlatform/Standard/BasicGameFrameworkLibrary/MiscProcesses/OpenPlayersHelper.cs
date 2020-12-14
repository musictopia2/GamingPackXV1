using BasicGameFrameworkLibrary.BasicGameDataClasses;
using CommonBasicStandardLibraries.CollectionClasses;
namespace BasicGameFrameworkLibrary.MiscProcesses
{
    public static class OpenPlayersHelper
    {
        public static bool CanHuman(IGameInfo game)
        {
            if (game.SinglePlayerChoice == EnumPlayerChoices.HumanOnly)
            {
                return true;
            }
            if (game.SinglePlayerChoice == EnumPlayerChoices.Either)
            {
                return true;
            }
            return false;
        }
        public static bool CanComputer(IGameInfo game)
        {
            if (game.SinglePlayerChoice == EnumPlayerChoices.ComputerOnly)
            {
                return true;
            }
            if (game.SinglePlayerChoice == EnumPlayerChoices.Either)
            {
                return true;
            }
            return false;
        }
        public static CustomBasicList<int> GetPossiblePlayers(IGameInfo game)
        {
            int x;
            x = 0;
            CustomBasicList<int> tempList = new CustomBasicList<int>();
            do
            {
                x += 1;
                if (x > game.MaxPlayers)
                {
                    break;
                }
                if (x  >= game.MinPlayers && x  != game.NoPlayers)
                {
                    tempList.Add(x - 1); //has to do minus 1 because there is always at least one player because of self.
                }
            }
            while (true);
            return tempList;
        }
    }
}