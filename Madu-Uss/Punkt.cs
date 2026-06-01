using System;
using System.Collections.Generic;
using System.Text;

namespace Madu_Uss
{
    public class Punkt
    {
        public int X { get; set; }
        public int Y { get; set; }
        public char Sümbol { get; set; } // Näiteks '*' ussi jaoks, '@' toidu jaoks

        public Punkt(int x, int y, char sümbol)
        {
            X = x;
            Y = y;
            Sümbol = sümbol;
        }

        public void Joonista()
        {
            Console.SetCursorPosition(X, Y);
            Console.Write(Sümbol);
        }

        public void Kustuta()
        {
            Console.SetCursorPosition(X, Y);
            Console.Write(' ');
        }
    }
}
