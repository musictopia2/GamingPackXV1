namespace BasicGameFrameworkLibrary.MahjongTileClasses
{
    public interface IMahjongTileInfo
    {
        int Deck { get; set; }
        int Index { get; set; }
        float Left { get; set; }
        bool NeedsBottom { get; set; }
        bool NeedsLeft { get; set; }
        bool NeedsRight { get; set; }
        bool NeedsTop { get; set; }
        int NumberUsed { get; set; }
        float Top { get; set; }
        BasicMahjongTile.EnumBonusType WhatBonus { get; set; }
        BasicMahjongTile.EnumColorType WhatColor { get; set; }
        BasicMahjongTile.EnumDirectionType WhatDirection { get; set; }
        BasicMahjongTile.EnumNumberType WhatNumber { get; set; }
    }
}