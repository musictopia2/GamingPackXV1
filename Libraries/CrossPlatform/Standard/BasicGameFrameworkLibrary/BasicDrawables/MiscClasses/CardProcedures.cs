using BasicGameFrameworkLibrary.BasicDrawables.Dictionary;
using BasicGameFrameworkLibrary.BasicDrawables.Interfaces;
using BasicGameFrameworkLibrary.MultiplayerClasses.BasicPlayerClasses;
using CommonBasicStandardLibraries.CollectionClasses;
using CommonBasicStandardLibraries.Exceptions;
using System.Linq;
namespace BasicGameFrameworkLibrary.BasicDrawables.MiscClasses
{
    public static class CardProcedures
    {
        public static void PassOutCards<P, D>(this IPlayerCollection<P> playerList, ICustomBasicList<D> thisCol, bool noComputerPass) where P : IPlayerObject<D>, new()
            where D : IDeckObject, new()
        {
            playerList.ForEach(items => items.MainHandList.Clear());
            int z = 0;
            int HowMany;
            HowMany = thisCol.Count;
            do
            {
                foreach (var thisPlayer in playerList)
                {
                    if (thisPlayer.PlayerCategory == EnumPlayerCategory.Computer && noComputerPass == true)
                    {
                    }
                    else
                    {
                        thisPlayer.MainHandList.Add(thisCol[z]); //hopefully no problem still (?)
                        z += 1;
                    }

                    if (z == HowMany)
                    {
                        return;
                    }
                }
            }
            while (true);
        }
        public static void PassOutCards<P, D>(this IPlayerCollection<P> playerList, ICustomBasicList<D> thisCol
            , int howMany, int testCount, bool noComputerPass, ref DeckRegularDict<D> leftOverList) where P : IPlayerObject<D>, new()
            where D : IDeckObject, new()
        {
            int players;
            players = playerList.Count();
            if (noComputerPass == true)
            {
                int subs = playerList.Count(Items => Items.PlayerCategory == EnumPlayerCategory.Computer);
                players -= subs;
            }
            int newcount;
            newcount = players * howMany;
            newcount -= testCount; //because less is being dealt out.
            if (newcount > thisCol.Count)
            {
                throw new BasicBlankException("There needs to be at least " + newcount + " cards.  However, there are only " + thisCol.Count + " cards to pass out");
            }
            int x;
            if (newcount == thisCol.Count)
            {
                leftOverList = new DeckRegularDict<D>();
            }
            else
            {
                var loopTo = thisCol.Count;
                if (leftOverList == null)
                {
                    leftOverList = new DeckRegularDict<D>();
                }
                for (x = newcount + 1; x <= loopTo; x++)
                {
                    leftOverList.Add(thisCol[x - 1]);// because 0 based
                }
            }
            int y;
            y = 0;
            CustomBasicList<CustomBasicList<D>> thisList = new CustomBasicList<CustomBasicList<D>>();
            foreach (var newPlayer in playerList)
            {
                CustomBasicList<D> temps = new CustomBasicList<D>();
                temps.AddRange(newPlayer.StartUpList);
                thisList.Add(temps);
            }

            int z;
            var loopTo1 = howMany;
            for (x = 1; x <= loopTo1; x++)
            {
                z = 0;
                foreach (var newPlayer in playerList)
                {
                    if (newPlayer.PlayerCategory == EnumPlayerCategory.Computer && noComputerPass == true)
                    {
                    }
                    else
                    {
                        if (newPlayer.StartUpList.Count == 0)
                        {
                            var TempList = thisList[z];
                            TempList.Add(thisCol[y]);
                            y += 1;

                        }
                        else
                        {
                            newPlayer.StartUpList.RemoveFirstItem(); //i think because the card is implied being added.
                        }
                    }
                    z += 1;
                }
            }
            z = 0;
            foreach (var newPlayer in playerList)
            {
                if (newPlayer.PlayerCategory == EnumPlayerCategory.Computer && noComputerPass == true)
                {
                }
                else
                {
                    newPlayer.MainHandList.ReplaceRange(thisList[z]); // i think
                }
                z += 1; //this could be it.
            }
        }
    }
}