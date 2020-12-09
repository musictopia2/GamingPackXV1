using System;
using System.Collections.Generic;
using System.Text;

namespace FroggiesCP.Data
{
    public class LilyPadModel
    {
        public int Row { get; set; }
        public int Column { get; set; }
        public bool HasFrog { get; set; }
        public bool IsSelected { get; set; }
        public bool IsTarget { get; set; }
        public bool StartedWithFrog { get; set; } //i think this way may be fine now.
        public LilyPadModel(int x, int y, bool pHasFrog)
        {
            Column = x;
            Row = y;
            HasFrog = pHasFrog;
        }
    }
}
