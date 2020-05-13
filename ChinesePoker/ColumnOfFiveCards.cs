using System;
using System.Collections.Generic;

namespace ChinesePoker
{
    enum Rank
    {
        NotCalculated = 0,
        HighCard = 1,
        OnePair,
        TwoPair,
        ThreeOfAKind,
        Straight,
        Flush,
        FullHouse,
        FourOfAKind,
        StraightFlush,
        RoyalFlush = 10

    }
    public struct TwoPair
    {
        private int _highPair;
        private int _lowPair;


        public int LowPair
        {
            get { return _lowPair; }
            set { _lowPair = value; }
        }


        public int HighPair
        {
            get { return _highPair; }
            set { _highPair = value; }
        }

        public TwoPair(int lowPair, int highPair)
        {
            this._lowPair = lowPair;
            this._highPair = highPair;
        }
    }

    public struct FullHouse
    {
        public int threeOfAKind { get; set; }
        public int TwoPair { get; set; }
    }

    internal class ColumnOfFiveCards
    {
        internal List<Card> _cards = new List<Card>(5);
        Rank _rank;

        public ColumnOfFiveCards()
        {
            _rank = Rank.NotCalculated;
        }

        static public void test()
        {
            ColumnOfFiveCards A = new ColumnOfFiveCards()
            {
                _cards = new List<Card>(52)
                {
                    new Card(2,Color.Black,Suit.Club,'c'),
                    new Card(2,Color.Black,Suit.Club,'c'),
                    new Card(2,Color.Black,Suit.Club,'c'),
                    new Card(13,Color.Black,Suit.Club,'c'),
                    new Card(13,Color.Black,Suit.Club,'c')
                }
            };

            ColumnOfFiveCards B = new ColumnOfFiveCards()
            {
                _cards = new List<Card>(52)
                {
                    new Card(1,Color.Red,Suit.Diamond,'d'),
                    new Card(1,Color.Red,Suit.Diamond,'d'),
                    new Card(4,Color.Red,Suit.Diamond,'d'),
                    new Card(4,Color.Red,Suit.Diamond,'d'),
                    new Card(4,Color.Red,Suit.Diamond,'d')
                }
            };
            PointFor result = A.fullHouseTieBreak(B);
        }



        internal Rank calculateRank()
        {
            Rank result;
            if (checkRoyalFlush() == true)
            {
                result = Rank.RoyalFlush;
            }
            else if (checkStraightFlush() == true)
            {
                result = Rank.StraightFlush;
            }
            else if (checkFourOfAKind() != 0)
            {
                result = Rank.FourOfAKind;

            }
            else if (checkFullHouse().threeOfAKind != 0)
            {
                result = Rank.FullHouse;

            }
            else if (checkFlush() == true)
            {
                result = Rank.Flush;

            }
            else if (checkStraight() == true)
            {
                result = Rank.Straight;

            }
            else if (checkThreeOfAKind() != 0)
            {
                result = Rank.ThreeOfAKind;

            }
            else if ((checkTwoPair().LowPair != 0) && (checkTwoPair().HighPair != 0))
            {
                result = Rank.TwoPair;

            }
            else if (checkOnePair() != 0)
            {
                result = Rank.OnePair;

            }
            else
            {
                result = Rank.HighCard;

            }
            _rank = result;
            return result;
        }

        private int checkOnePair()
        {
            for (int i = 0; i < _cards.Count; i++)
            {
                for (int j = i + 1; j < _cards.Count; j++)
                {
                    if (_cards[i]._number == _cards[j]._number)
                        return _cards[i]._number;
                }
            }
            return 0;
        }

        private TwoPair checkTwoPair()
        {
            int pairs = 0;
            int lowPair = 0;
            int highPair = 0;
            this.SortCardsByNumber();
            for (int i = 0; i < _cards.Count - 1; i++)
            {
                if (_cards[i + 1]._number == (_cards[i]._number))
                {
                    pairs++;
                    if (pairs == 1)
                        lowPair = _cards[i]._number;
                    else if (pairs == 2)
                        highPair = _cards[i]._number;
                }
            }
            if (lowPair == 1)
            {
                lowPair = highPair;
                highPair = 14;
            }
            return new TwoPair(lowPair, highPair);



        }

        private int checkThreeOfAKind()
        {
            this.SortCardsByNumber();
            for (int i = 0; i < _cards.Count - 2; i++)
            {
                if ((_cards[i + 2]._number == _cards[i + 1]._number)
                 && (_cards[i + 1]._number == _cards[i]._number))
                    return _cards[i]._number;
            }
            return 0;
        }

        private bool checkStraight()
        {

            if (IsBroadwayStraight() == true)
                return true;
            else
            {
                for (int i = 0; i < _cards.Count - 1; i++)
                {
                    if (_cards[i + 1]._number != (_cards[i]._number) + 1)
                        return false;
                }
            }
            return true;

        }

        private void SortCardsByNumber()
        {

            _cards.Sort((x, y) => x._number.CompareTo(y._number));
        }

        private bool checkFlush()
        {
            Suit suit = _cards[0]._suit;
            foreach (Card card in _cards)
            {
                if (card._suit != suit)
                    return false;
            }
            return true;
        }

        private FullHouse checkFullHouse()
        {
            FullHouse result = new FullHouse();
            this.SortCardsByNumber();

            if ((_cards[4]._number == _cards[3]._number)
             && (_cards[2]._number == _cards[1]._number)
             && (_cards[1]._number == _cards[0]._number))
            {
                result.threeOfAKind = _cards[0]._number;
                result.TwoPair = _cards[4]._number;
            }

            else if ((_cards[4]._number == _cards[3]._number)
             && (_cards[3]._number == _cards[2]._number)
             && (_cards[1]._number == _cards[0]._number))
            {
                result.threeOfAKind = _cards[4]._number;
                result.TwoPair = _cards[0]._number;
            }
            else result.TwoPair = result.threeOfAKind = 0;

            return result;
        }

        private int checkFourOfAKind()
        {
            this.SortCardsByNumber();
            for (int i = 0; i < _cards.Count - 3; i++)
            {
                if ((_cards[i + 3]._number == _cards[i + 2]._number)
                 && (_cards[i + 2]._number == _cards[i + 1]._number)
                 && (_cards[i + 1]._number == _cards[i]._number))
                    return _cards[i]._number;
            }
            return 0;
        }

        private bool checkStraightFlush()
        {
            return (checkStraight() && checkFlush());
        }

        private bool checkRoyalFlush()
        {
            return (checkStraightFlush() && IsBroadwayStraight());
        }

        private bool IsBroadwayStraight()
        {
            bool result = false;
            this.SortCardsByNumber();
            if ((_cards[0]._number == 1)
                && (_cards[1]._number == 10)
                && (_cards[2]._number == 11)
                && (_cards[3]._number == 12)
                && (_cards[4]._number == 13))
                result = true;
            return result;


        }

        internal PointFor compare(ColumnOfFiveCards i_columnOfFiveCards)
        {
            PointFor point;
            Rank player1Rank = this.calculateRank();
            Rank player2Rank = i_columnOfFiveCards.calculateRank();
            if ((int)player1Rank > (int)player2Rank)
            {
                point = PointFor.player1;
            }
            else if ((int)player1Rank < (int)player2Rank)
            {
                point = PointFor.player2;

            }
            else point = this.tieBreak(i_columnOfFiveCards);
            return point;
        }

        private PointFor tieBreak(ColumnOfFiveCards i_columnOfFiveCards)
        {

            PointFor result = PointFor.player1;
            this.SortCardsByNumber();
            i_columnOfFiveCards.SortCardsByNumber();
            switch (_rank)
            {
                case Rank.HighCard:
                    result = highCardTieBreak(i_columnOfFiveCards);
                    break;
                case Rank.OnePair:
                    result = onePairTieBreak(i_columnOfFiveCards);
                    break;
                case Rank.TwoPair:
                    result = twoPairTieBreak(i_columnOfFiveCards);
                    break;
                case Rank.ThreeOfAKind:
                    result = threeOfAKindTieBreak(i_columnOfFiveCards);
                    break;
                case Rank.Straight:
                    result = straightTieBreak(i_columnOfFiveCards);
                    break;
                case Rank.Flush:
                    result = flushTieBreak(i_columnOfFiveCards);
                    break;
                case Rank.FullHouse:
                    result = fullHouseTieBreak(i_columnOfFiveCards);
                    break;
                case Rank.FourOfAKind:
                    result = fourOfAKindTieBreak(i_columnOfFiveCards);
                    break;
                case Rank.StraightFlush:
                    result = straightFlushTieBreak(i_columnOfFiveCards);
                    break;
                case Rank.RoyalFlush:
                    result = PointFor.tie;
                    break;

            }
            return result;

        }

        private PointFor straightFlushTieBreak(ColumnOfFiveCards i_columnOfFiveCards)
        {
            return this.highCardTieBreak(i_columnOfFiveCards);
        }

        private PointFor fourOfAKindTieBreak(ColumnOfFiveCards i_columnOfFiveCards)
        {
            int player1Quad = this.checkFourOfAKind();
            int player2Quad = i_columnOfFiveCards.checkFourOfAKind();
            return player1Quad > player2Quad ? PointFor.player1 : PointFor.player2;
        }

        private PointFor fullHouseTieBreak(ColumnOfFiveCards i_columnOfFiveCards)
        {
            FullHouse player1FullHouse = this.checkFullHouse();
            FullHouse player2FullHouse = i_columnOfFiveCards.checkFullHouse();
            if (player1FullHouse.threeOfAKind == 1)
                player1FullHouse.threeOfAKind = 14;
            if (player2FullHouse.threeOfAKind == 1)
                player2FullHouse.threeOfAKind = 14;
            return player1FullHouse.threeOfAKind > player2FullHouse.threeOfAKind ? PointFor.player1 : PointFor.player2;
        }

        private PointFor flushTieBreak(ColumnOfFiveCards i_columnOfFiveCards)
        {
            return this.highCardTieBreak(i_columnOfFiveCards);
        }

        private PointFor straightTieBreak(ColumnOfFiveCards i_columnOfFiveCards)
        {
            ColumnOfFiveCards player1Hand = this.Clone();
            ColumnOfFiveCards player2Hand = i_columnOfFiveCards.Clone();
            PointFor result;
            if (this.IsBroadwayStraight() && !i_columnOfFiveCards.IsBroadwayStraight())
                result = PointFor.player1;
            else if (!this.IsBroadwayStraight() && i_columnOfFiveCards.IsBroadwayStraight())
                result = PointFor.player2;
            else if (this.IsBroadwayStraight() && i_columnOfFiveCards.IsBroadwayStraight())
                result = PointFor.tie;
            else
            {
                Card player1Max = player1Hand.getHighestCard(0);
                Card player2Max = player2Hand.getHighestCard(0); ;
                if (player1Max._number > player2Max._number)
                    result = PointFor.player1;
                else if (player1Max._number < player2Max._number)
                    result = PointFor.player2;
                else result = PointFor.tie;
            }
            return result;
        }

        private PointFor threeOfAKindTieBreak(ColumnOfFiveCards i_columnOfFiveCards)
        {
            int player1High = checkThreeOfAKind();
            int player2High = i_columnOfFiveCards.checkThreeOfAKind();
            if (player1High == 1)
            {
                player1High = 14;
            }
            if (player2High == 1)
            {
                player2High = 14;
            }

            return player2High > player1High ? PointFor.player2 : PointFor.player1;
        }

        private PointFor twoPairTieBreak(ColumnOfFiveCards i_columnOfFiveCards)
        {
            PointFor result = PointFor.player1;
            TwoPair player1TwoPair = this.checkTwoPair();
            TwoPair player2TwoPair = i_columnOfFiveCards.checkTwoPair();
            int player1Kicker = 0;
            int player2Kicker = 0;

            if (player2TwoPair.HighPair > player1TwoPair.HighPair)
                result = PointFor.player2;
            else if (player1TwoPair.HighPair > player2TwoPair.HighPair)
                result = PointFor.player1;
            else if (player2TwoPair.LowPair > player1TwoPair.LowPair)
                result = PointFor.player2;
            else if (player1TwoPair.LowPair > player2TwoPair.LowPair)
                result = PointFor.player1;
            else
            {


                foreach (Card card in _cards)
                {
                    int num = card._number;
                    if (num == 1)
                        num = 13;
                    if ((num != player1TwoPair.HighPair) &&
                        (num != player1TwoPair.LowPair))
                    {
                        player1Kicker = num;
                        break;
                    }
                }
                foreach (Card card in i_columnOfFiveCards._cards)
                {
                    int num = card._number;
                    if (num == 1)
                        num = 13;
                    if ((num != player2TwoPair.HighPair) &&
                        (num != player2TwoPair.LowPair))
                    {
                        player2Kicker = num;
                        break;
                    }
                }
                if (player1Kicker == 1)
                    player1Kicker = 14;
                if (player2Kicker == 1)
                    player2Kicker = 14;
                if (player1Kicker > player2Kicker)
                    result = PointFor.player1;
                else if (player1Kicker < player2Kicker)
                    result = PointFor.player2;
                else result = PointFor.tie;

            }
            return result;



        }

        private PointFor onePairTieBreak(ColumnOfFiveCards i_columnOfFiveCards)
        {
            int player1OnePair = this.checkOnePair();
            int player2OnePair = i_columnOfFiveCards.checkOnePair();
            if (player1OnePair == 1)
            {
                player1OnePair = 14;
            }
            if (player2OnePair == 1)
            {
                player2OnePair = 14;
            }
            if (player1OnePair > player2OnePair)
                return PointFor.player1;
            else if (player1OnePair < player2OnePair)
                return PointFor.player2;
            else
            {
                List<Card> player1ThreeRemainingCards = new List<Card>(3);
                List<Card> player2ThreeRemainingCards = new List<Card>(3);
                foreach (Card card in _cards)
                {
                    if (card._number != player1OnePair)
                        player1ThreeRemainingCards.Add(card);
                }
                foreach (Card card in i_columnOfFiveCards._cards)
                {
                    if (card._number != player2OnePair)
                        player2ThreeRemainingCards.Add(card);
                }

                int i = 0;
                foreach (Card card in player1ThreeRemainingCards)
                {
                    if (card._number > player2ThreeRemainingCards[i]._number)
                    {
                        return PointFor.player1;
                    }
                    else if (card._number < player2ThreeRemainingCards[i]._number)
                    {
                        return PointFor.player2;
                    }
                    i++;
                }
                return PointFor.tie;

            }


        }

        private PointFor highCardTieBreak(ColumnOfFiveCards i_columnOfFiveCards)
        {
            ColumnOfFiveCards player1Hand = this.Clone();
            ColumnOfFiveCards player2Hand = i_columnOfFiveCards.Clone();
            for (int i = 0; i < 5; i++)
            {
                Card player1Max = player1Hand.getHighestCard(1);
                Card player2Max = player2Hand.getHighestCard(1);
                player1Hand._cards.Remove(player1Max);
                player2Hand._cards.Remove(player2Max);

                if (player1Max._number > player2Max._number)
                    return PointFor.player1;
                else if (player1Max._number < player2Max._number)
                    return PointFor.player2;

            }
            return PointFor.tie;

        }

        private Card getHighestCard(int a)
        {
            if (a == 1)
            {

                if (this._cards[0]._number == 1)
                    this._cards[0]._number = 14;
            }
            int max = this._cards[0]._number;
            Card result = this._cards[0];
            for (int i = 1; i < this._cards.Count; i++)
            {
                if (a == 1)
                {

                    if (this._cards[i]._number == 1)
                        this._cards[i]._number = 14;
                }
                if (this._cards[i]._number > max)
                {
                    max = this._cards[i]._number;
                    result = this._cards[i];
                }

            }
            return result;
        }

        private ColumnOfFiveCards Clone()
        {
            ColumnOfFiveCards copy = new ColumnOfFiveCards();
            foreach (Card card in _cards)
            {
                copy._cards.Add(new Card(card._number, card._color, card._suit, card._suitShortcut));
            }
            copy._rank = this._rank;
            return copy;
        }
    }
}