namespace LifeBoardGameCP.Data
{
    public enum EnumWhatStatus
    {
        None = 0,
        NeedToSpin = 1,
        NeedToChooseSpace = 2, // this means there is a choice of 2 spaces.
        NeedChooseFirstOption = 3,
        NeedChooseFirstCareer = 4,
        NeedChooseHouse = 5,
        NeedSellBuyHouse = 6,
        NeedNight = 7,
        NeedTradeSalary = 8,
        NeedChooseStock = 9,
        NeedReturnStock = 10,
        NeedNewCareer = 11,
        NeedToEndTurn = 12,
        NeedFindSellPrice = 13,
        NeedChooseGender = 16,
        NeedChooseSalary = 17,
        NeedChooseRetirement = 18,
        NeedStealTile = 19,
        NeedSellHouse = 20,
        MakingMove = 21, // so when repainting and its animating move, will not have the borders anymore.
        LastSpin = 22 // this means it already spinned.
    }
}