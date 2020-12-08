using CommonBasicStandardLibraries.Exceptions;
using static BasicGameFrameworkLibrary.MahjongTileClasses.BasicMahjongTile;
namespace BasicGameFrameworkLibrary.MahjongTileClasses
{
    internal class MahjongBasicTileHelper
    {
        public static void PopulateTile(IMahjongTileInfo thisTile, int chosen)
        {
            if (chosen < 1 || chosen > 144)
            {
                throw new BasicBlankException($"The deck must be between 1 and 144, not {chosen}");
            }
            thisTile.Index = FindIndex(chosen);
            thisTile.Deck = chosen;
            thisTile.NumberUsed = 0;
            thisTile.WhatBonus = EnumBonusType.IsNoBonus;
            thisTile.WhatDirection = EnumDirectionType.IsNoDirection;
            thisTile.WhatColor = EnumColorType.IsNoColor;
            thisTile.WhatNumber = EnumNumberType.IsNoNumber;
            int index = thisTile.Index;
            if (thisTile.Index < 10)
            {
                thisTile.WhatNumber = EnumNumberType.IsCircle;
                thisTile.NumberUsed = thisTile.Index;
            }
            else if (index < 19)
            {
                thisTile.WhatNumber = EnumNumberType.IsBamboo;
                thisTile.NumberUsed = thisTile.Index - 9;
            }
            else if (index < 28)
            {
                thisTile.WhatNumber = EnumNumberType.IsCharacter;
                thisTile.NumberUsed = thisTile.Index - 18;
            }
            else if (index == 28)
            {
                thisTile.WhatDirection = EnumDirectionType.IsEast;
            }
            else if (index == 29)
            {
                thisTile.WhatDirection = EnumDirectionType.IsSouth;
            }
            else if (index == 30)
            {
                thisTile.WhatDirection = EnumDirectionType.IsWest;
            }
            else if (index == 31)
            {
                thisTile.WhatDirection = EnumDirectionType.IsNorth;
            }
            else if (index == 32)
            {
                thisTile.WhatColor = EnumColorType.IsRed;
            }
            else if (index == 33)
            {
                thisTile.WhatColor = EnumColorType.IsGreen;
            }
            else if (index == 34)
            {
                thisTile.WhatColor = EnumColorType.IsWhite;
            }
            else if (index < 39)
            {
                thisTile.WhatBonus = EnumBonusType.IsSeason;
            }
            else
            {
                thisTile.WhatBonus = EnumBonusType.IsFlower;
            }
        }
        private static int FindIndex(int deck) // i think
        {
            if (deck < 5)
            {
                return 1;
            }
            else if (deck < 9)
            {
                return 2;
            }
            else if (deck < 13)
            {
                return 3;
            }
            else if (deck < 17)
            {
                return 4;
            }
            else if (deck < 21)
            {
                return 5;
            }
            else if (deck < 25)
            {
                return 6;
            }
            else if (deck < 29)
            {
                return 7;
            }
            else if (deck < 33)
            {
                return 8;
            }
            else if (deck < 37)
            {
                return 9;
            }
            else if (deck < 41)
            {
                return 10;
            }
            else if (deck < 45)
            {
                return 11;
            }
            else if (deck < 49)
            {
                return 12;
            }
            else if (deck < 53)
            {
                return 13;
            }
            else if (deck < 57)
            {
                return 14;
            }
            else if (deck < 61)
            {
                return 15;
            }
            else if (deck < 65)
            {
                return 16;
            }
            else if (deck < 69)
            {
                return 17;
            }
            else if (deck < 73)
            {
                return 18;
            }
            else if (deck < 77)
            {
                return 19;
            }
            else if (deck < 81)
            {
                return 20;
            }
            else if (deck < 85)
            {
                return 21;
            }
            else if (deck < 89)
            {
                return 22;
            }
            else if (deck < 93)
            {
                return 23;
            }
            else if (deck < 97)
            {
                return 24;
            }
            else if (deck < 101)
            {
                return 25;
            }
            else if (deck < 105)
            {
                return 26;
            }
            else if (deck < 109)
            {
                return 27;
            }
            else if (deck < 113)
            {
                return 28;
            }
            else if (deck < 117)
            {
                return 29;
            }
            else if (deck < 121)
            {
                return 30;
            }
            else if (deck < 125)
            {
                return 31;
            }
            else if (deck < 129)
            {
                return 32;
            }
            else if (deck < 133)
            {
                return 33;
            }
            else if (deck < 137)
            {
                return 34;
            }
            else
            {
                return deck - 102;
            }
        }
    }
}