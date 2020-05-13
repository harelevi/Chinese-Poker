using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using static ChinesePoker.MainForm;

namespace ChinesePoker
{
    public class UIColumnOfFiveCards
    {
        int _id;
        internal Button putCard = new Button();
        internal List<PictureBox> _cards = new List<PictureBox>(4);
        internal Button _LastCard ;
        internal Card _LastCardLogic;
        bool lastCardShown = false;
        internal UIColumnOfFiveCards(int i_id,Direction i_direction, MainForm i_mainForm, Point p)
        {

            _id = i_id;
            int y = p.Y;
            if (i_direction == Direction.DownToUp)
            {

                for (int i = 5; i > 0; i--)
                {
                    if (i == 5)
                    {
                        _LastCard = new Button()
                        {
                            Location = new Point(p.X, y),
                            Size = new Size(150, 150),
                            Visible = false
                        };
                        i_mainForm.Controls.Add(_LastCard);
                        _LastCard.Click += _LastCard_Click;
                        y += 50;
                        continue ;

                    }
                    _cards.Add(new PictureBox()
                    {
                        Location = new Point(p.X, y),
                        Size = new Size(150, 150),
                    });
                    y += 50;

                }
            }
            else
            {
                for (int i = 0; i < 5; i++)
                {
                    if (i == 4)
                    {
                        _LastCard = new Button()
                        {
                            Location = new Point(p.X, y),
                            Size = new Size(150, 150),
                            Visible = false
                        };
                        i_mainForm.Controls.Add(_LastCard);
                        _LastCard.Click += _LastCard_Click;
                        y += 50;
                        break;

                    }
                    _cards.Add(new PictureBox()
                    {
                        Location = new Point(p.X, y),
                        Size = new Size(150, 150),
                    });
                    y += 50;

                }
            }

            i_mainForm.Controls.Add(putCard);
            foreach(PictureBox card in _cards)
            {
                i_mainForm.Controls.Add(card);
            }
            y += 50;
          //  putCard.Location = new Point(p.X,y);
            putCard.Text = _id.ToString();


        }

        private void _LastCard_Click(object sender, System.EventArgs e)
        {
            Button lastCard = sender as Button;
            if ( lastCardShown)
            {
                lastCard.BackgroundImage = _LastCardLogic.backImage;
                lastCardShown = false;
            }
            else
            {
                lastCard.BackgroundImage = _LastCardLogic.image;
                lastCardShown = true;
            }
        }
    }
}