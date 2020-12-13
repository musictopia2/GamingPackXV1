namespace PaydayCP.Data
{
    public enum EnumStatus
    {
        None = 0, // so it will be compatible
        Starts = 1,
        ChooseLottery = 2,
        RollLottery = 3,
        RollRadio = 4,
        ChoosePlayer = 5,
        ChooseDeal = 6,
        ChooseBuy = 7,
        MakeMove = 8,
        EndingTurn = 9,
        DealOrBuy = 10,
        RollCharity,
        ViewMail,
        ViewYardSale // so it can trigger to show the dice not visible.
    }
}