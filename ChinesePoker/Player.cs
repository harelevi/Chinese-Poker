using System;
using System.Collections.Generic;

namespace ChinesePoker
{
    internal class Player
    {
        internal List<ColumnOfFiveCards> _FivecolumnOfFiveCards = new List<ColumnOfFiveCards>(5);
        internal int i_score = 0;

        public Player()
        {
            for (int i = 0; i < 5; i++)
            {
                _FivecolumnOfFiveCards.Add(new ColumnOfFiveCards());
            }
        }

        internal void increaseScore()
        {
            i_score++;
        }
    }
}