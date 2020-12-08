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
            if (game.MinPlayers == 3)
            {
                tempList.Add(1); //because you would already have at least 2 players.
            }
            do
            {
                x += 1;
                if (x > game.MaxPlayers)
                {
                    break;
                }
                if (x + 1 >= game.MinPlayers && x + 1 != game.NoPlayers)
                {
                    tempList.Add(x);
                }
            }
            while (true);
            tempList.RemoveLastItem(); //i think
            return tempList;
        }
    }
}