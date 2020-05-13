using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChinesePoker
{
    public enum Suit
    {
        Diamond = 1,
        Heart,
        Club,
        Spade

    }
    public enum Color
    {
        Red = 1,
        Black,

    }
    public class Card
    {
        internal char _suitShortcut;
        internal int _number;
        internal Color _color;
        internal Suit _suit;
        internal Image image;
        internal Image backImage;


        public Card(int i_number, Color i_color, Suit i_suit,char i_suitShortcut)
        {
            _number = i_number;
            _suit = i_suit;
            _suitShortcut = i_suitShortcut;
            _color = i_color;
            string imageString = string.Format("C:/Users/Harel/Desktop/הראל מדעי המחשב/שנה ג/visual studio solutions/ChinesePoker/ChinesePoker/images/{0}{1}.jpg", _number, _suitShortcut);
            image = Image.FromFile(imageString);
            string _backImage = string.Format("C:/Users/Harel/Desktop/הראל מדעי המחשב/שנה ג/visual studio solutions/ChinesePoker/ChinesePoker/images/cashier.jpg");
            backImage = Image.FromFile(_backImage);
        }
        public Card()
        {

        }


    }
}
