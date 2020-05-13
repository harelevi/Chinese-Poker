using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChinesePoker
{
    public class Cashier
    {
        internal List<Card> _cards;
        public Cashier()
        {
            _cards = new List<Card>(52);
            for (int i = 1; i <= 13; i++)
            {
                _cards.Add(new Card(i, Color.Red, Suit.Diamond,'d'));
                _cards.Add(new Card(i, Color.Red, Suit.Heart,'h'));
                _cards.Add(new Card(i, Color.Black, Suit.Club,'c'));
                _cards.Add(new Card(i, Color.Black, Suit.Spade,'s'));
            }
        }


    }
}
