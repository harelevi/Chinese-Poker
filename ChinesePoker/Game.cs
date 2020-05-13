using System;
using System.Collections.Generic;

namespace ChinesePoker
{
    enum Turn
    {
        player1,
        player2
    }
    enum PointFor
    {
        player1,
        player2,
        tie
    }
    enum Winner
    {
        player1 = 1,
        player2,
        tie
    }
    internal class Game
    {
        internal Dealer _dealer;
        internal Player _player1, _player2;
        internal Turn _currentTurn;
        internal Winner _winner;


        public Game()
        {
            _player1 = new Player();
            _player2 = new Player();
            _dealer = new Dealer();

        }

        public void startAgame()
        {
            _dealer.shuffle();
            _dealer.shuffle();
            _dealer.shuffle();
            _dealer.deal(_player1, _player2);
            _currentTurn = Turn.player1;

        }

        internal void endGame()
        {
            List<ColumnOfFiveCards> player1Hands = _player1._FivecolumnOfFiveCards;
            List<ColumnOfFiveCards> player2Hands = _player2._FivecolumnOfFiveCards;
            int i = 0;
            foreach (ColumnOfFiveCards hand in player1Hands)
            {
               PointFor point =  hand.compare(player2Hands[i++]);
                if (point == PointFor.player1)
                {
                    _player1.increaseScore();
                }
                if (point == PointFor.player2)
                {
                    _player2.increaseScore();

                }
            
            }
            if (_player1.i_score > _player2.i_score)
                _winner = Winner.player1;
            else if (_player1.i_score < _player2.i_score)
                _winner = Winner.player2;
            else
                _winner = Winner.tie;


        }
    }
}