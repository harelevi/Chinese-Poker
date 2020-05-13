using System;
using System.Collections.Generic;

namespace ChinesePoker
{
    internal static class DealerService
    {
        public static void ShuffleMe<T>(this IList<T> list)
        {
            Random random = new Random();
            int n = list.Count;

            for (int i = list.Count - 1; i > 1; i--)
            {
                int rnd = random.Next(i + 1);

                T value = list[rnd];
                list[rnd] = list[i];
                list[i] = value;
            }
        }
    }
    internal class Dealer
    {
        internal Cashier _CashierOfCards;

        public Dealer()
        {
            _CashierOfCards = new Cashier();

        }

        internal void shuffle()
        {
            _CashierOfCards._cards.ShuffleMe();
        }

        internal void deal( Player _player1, Player _player2)
        {
            for (int i = 0; i < 5; i++)
            {
                Card cardForPlayer1 = _CashierOfCards._cards[0];
                Card cardForPlayer2 = _CashierOfCards._cards[1];
                _CashierOfCards._cards.RemoveAt(0);
                _CashierOfCards._cards.RemoveAt(0);
                _player1._FivecolumnOfFiveCards[i]._cards.Add(cardForPlayer1);
                _player2._FivecolumnOfFiveCards[i]._cards.Add(cardForPlayer2);
            }
        }

        internal Card pop()
        {
            Card card = _CashierOfCards._cards[0];
            _CashierOfCards._cards.RemoveAt(0);
            
            return card;
        }
    }
}