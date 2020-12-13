using ClueBoardGameCP.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClueBoardGameBlazor
{
    public class CharacterStartKey
    {
        public CharacterInfo? Character { get; set; }
        public int StartSpace { get; set; }
    }
}
