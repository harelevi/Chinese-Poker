using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;
using System;

namespace ChinesePoker
{
    internal class MainForm : Form
    {

        internal enum Direction
        {
            UpToDown,
            DownToUp

        }
        internal Game currentGame;
        private List<UIColumnOfFiveCards> _player1fiveColumnsOfCards = new List<UIColumnOfFiveCards>(5);
        private List<UIColumnOfFiveCards> _player2fiveColumnsOfCards = new List<UIColumnOfFiveCards>(5);
        private Label _currentTurn;
        private Label _scoreDetails;
        Button _cashierButton;
        Button _startButton;
        PictureBox _lastPopedCard;


        public MainForm()
        {
            this.BackColor = System.Drawing.Color.Tomato;
            this.ClientSize = new System.Drawing.Size(490, 335);

            currentGame = new Game();
            initializeBoard();
            //ColumnOfFiveCards.test();




        }

        private void initializeBoard()
        {

            _currentTurn = new Label()
            {
                Text = $"תור שחקן מספר 1 \n קח קלף מהקופה",
                Location = new Point(50, 50),
                Size = new Size(100, 100),
            };
            _startButton = new Button()
            {
                Location = new Point(50, 50),
                Size = new Size(100, 100),
                Text = "התחל משחק"
            };
            _cashierButton = new Button()
            {
                Location = new Point(50, 150),
                Size = new Size(150, 150),
                BackgroundImage = Image.FromFile("C:/Users/Harel/Desktop/הראל מדעי המחשב/שנה ג/visual studio solutions/ChinesePoker/ChinesePoker/images/cashier.jpg"),
                BackgroundImageLayout = ImageLayout.Stretch,
                Visible = false


            };
            _lastPopedCard = new PictureBox()
            {
                Location = new Point(50, 350),
                Size = new Size(150, 150),
                BackgroundImageLayout = ImageLayout.Stretch,

            };


            int column = 150;
            for (int i = 0; i < _player1fiveColumnsOfCards.Capacity; i++)
            {
                UIColumnOfFiveCards ui = new UIColumnOfFiveCards(i, Direction.DownToUp, this, new Point(column += 200, 0));
                _player1fiveColumnsOfCards.Add(ui);
                ui.putCard.Click += PutCard_Player1_Click;
                ui.putCard.Location = new Point(390 + i * 200, 350);

            }
            column = 150;
            for (int i = 0; i < _player2fiveColumnsOfCards.Capacity; i++)
            {
                UIColumnOfFiveCards ui = new UIColumnOfFiveCards(i, Direction.UpToDown, this, new Point(column += 200, 400));
                _player2fiveColumnsOfCards.Add(ui);
                ui.putCard.Click += PutCard_Player2_Click;
                ui.putCard.Location = new Point(390 + i * 200, 370);



            }

            changePlayerColumnButtonsStatus();
            _startButton.Click += new EventHandler(startButtonClickedEvent);
            _cashierButton.Click += new EventHandler(cashierButtonClickedEvent);

            this.Controls.Add(_startButton);
            this.Controls.Add(_cashierButton);
            this.Controls.Add(_currentTurn);
            this.Controls.Add(_lastPopedCard);


        }

        private void PutCard_Player1_Click(object sender, EventArgs e)
        {
            Button putCardButton = sender as Button;
            int i = int.Parse(putCardButton.Text);
            List<ColumnOfFiveCards> player1Hands = currentGame._player1._FivecolumnOfFiveCards;
            if (player1Hands[i]._cards.Count == 5)
            {
                player1Hands[i]._cards[4] = currentCard;
            }
            else
            {
                player1Hands[i]._cards.Add(currentCard);
            }
            currentGame._currentTurn = Turn.player2;
            changePlayerColumnButtonsStatus();
            _cashierButton.Enabled = true;
            updateBoard();


        }

        private void PutCard_Player2_Click(object sender, EventArgs e)
        {
            Button putCardButton = sender as Button;
            int i = int.Parse(putCardButton.Text);
            List<ColumnOfFiveCards> player2Hands = currentGame._player2._FivecolumnOfFiveCards;
            if (player2Hands[i]._cards.Count == 5)
            {
                player2Hands[i]._cards[4] = currentCard;
            }
            else
            {
                player2Hands[i]._cards.Add(currentCard);
            }
            currentGame._currentTurn = Turn.player1;
            changePlayerColumnButtonsStatus();
            _cashierButton.Enabled = true;
            updateBoard();
            Cashier cashier = currentGame._dealer._CashierOfCards;

            if (cashier._cards.Count == 0)
            {
                currentGame.endGame();
                _scoreDetails = new Label();
                string score = $"the score is : {currentGame._player1.i_score}-{currentGame._player2.i_score}";
                if (currentGame._winner == Winner.tie)
                {
                    _scoreDetails.Text = $"it was a {Winner.tie} !\n {score}";
                }
                else
                {
                    _scoreDetails.Text = $"{currentGame._winner} has won the game!\n {score}";
                }
                _scoreDetails.Location = new Point(50, 550);
                _scoreDetails.Size = new Size(200, 200);


                this.Controls.Add(_scoreDetails);
            }


        }

        private void changePlayerColumnButtonsStatus()
        {
            _lastPopedCard.BackgroundImage = null;

            if (currentGame._currentTurn == Turn.player1)
            {
                _currentTurn.Text = $"תור שחקן מספר 1 \n קח קלף מהקופה";

                foreach (UIColumnOfFiveCards fiveCards in _player1fiveColumnsOfCards)
                {
                    fiveCards.putCard.Visible = true;

                }
                foreach (UIColumnOfFiveCards fiveCards in _player2fiveColumnsOfCards)
                {
                    fiveCards.putCard.Visible = false;

                }
            }
            else
            {
                _currentTurn.Text = $"תור שחקן מספר 2 \n קח קלף מהקופה";

                foreach (UIColumnOfFiveCards fiveCards in _player2fiveColumnsOfCards)
                {
                    fiveCards.putCard.Visible = true;

                }
                foreach (UIColumnOfFiveCards fiveCards in _player1fiveColumnsOfCards)
                {
                    fiveCards.putCard.Visible = false;

                }
            }


        }

        private Card currentCard;
        private void cashierButtonClickedEvent(object sender, EventArgs e)
        {
            Dealer dealer = currentGame._dealer;

            currentCard = dealer.pop();
            _lastPopedCard.BackgroundImage = currentCard.image;
            _cashierButton.Enabled = false;

        }

        private void startButtonClickedEvent(object sender, EventArgs e)
        {
            currentGame.startAgame();
            updateBoard();
            _startButton.Dispose();
            this.Controls.Remove(_startButton);
            _cashierButton.Visible = true;

        }

        private void updateBoard()
        {

            List<ColumnOfFiveCards> player1Cards = currentGame._player1._FivecolumnOfFiveCards;
            List<ColumnOfFiveCards> player2Cards = currentGame._player2._FivecolumnOfFiveCards;
            int i = 0;
            bool loop = true;
            foreach (ColumnOfFiveCards column in player1Cards)
            {
                int j = 3;
                foreach (Card card in column._cards)
                {
                    if (j == -1)
                    {
                        _player1fiveColumnsOfCards[i]._LastCardLogic = card;
                        loop = false;
                        break;
                    }
                    _player1fiveColumnsOfCards[i]._cards[j].BackgroundImage = card.image;
                    _player1fiveColumnsOfCards[i]._cards[j].BackgroundImageLayout = ImageLayout.Stretch;
                    _player1fiveColumnsOfCards[i]._cards[j].BringToFront();
                    j--;
                }

                if (loop == false)
                {

                    _player1fiveColumnsOfCards[i]._LastCard.Visible = true;
                    _player1fiveColumnsOfCards[i]._LastCard.BackgroundImage = player1Cards[i]._cards[4].backImage;
                    _player1fiveColumnsOfCards[i]._LastCard.BackgroundImageLayout = ImageLayout.Stretch;
                    _player1fiveColumnsOfCards[i]._LastCard.BringToFront();
                }
                loop = true;
                i++;
            }



            i = 0;
            loop = true;
            foreach (ColumnOfFiveCards column in player2Cards)
            {
                int j = 0;
                foreach (Card card in column._cards)
                {
                    if (j == 4)
                    {
                        _player2fiveColumnsOfCards[i]._LastCardLogic = card;

                        loop = false;
                        break;
                    }
                    _player2fiveColumnsOfCards[i]._cards[j].BackgroundImage = card.image;
                    _player2fiveColumnsOfCards[i]._cards[j].BackgroundImageLayout = ImageLayout.Stretch;
                    _player2fiveColumnsOfCards[i]._cards[j].BringToFront();
                    j++;
                }
                if (loop == false)
                {
                    _player2fiveColumnsOfCards[i]._LastCard.Visible = true;
                    _player2fiveColumnsOfCards[i]._LastCard.BackgroundImage = player2Cards[i]._cards[4].backImage;
                    _player2fiveColumnsOfCards[i]._LastCard.BackgroundImageLayout = ImageLayout.Stretch;
                    _player2fiveColumnsOfCards[i]._LastCard.BringToFront();
                }
                loop = true;
                i++;
            }

        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // MainForm
            // 
            this.ClientSize = new System.Drawing.Size(1067, 456);
            this.Name = "MainForm";
            this.ResumeLayout(false);

        }


    }






}
