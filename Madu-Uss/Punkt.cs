using System;

namespace Madu_Uss
{
    public class Punkt
    {
        public int X { get; set; }
        public int Y { get; set; }
        public char Sümbol { get; set; }
        public ConsoleColor Värv { get; set; } // Lisatud värvitoetus

        public Punkt(int x, int y, char sümbol, ConsoleColor värv = ConsoleColor.White)
        {
            X = x;
            Y = y;
            Sümbol = sümbol;
            Värv = värv;
        }

        public void Joonista()
        {
            Console.SetCursorPosition(X, Y);
            Console.ForegroundColor = Värv; // Määrame objekti värvi
            Console.Write(Sümbol);
            Console.ResetColor(); // Taastame konsooli algse värvi
        }

        public void Kustuta()
        {
            Console.SetCursorPosition(X, Y);
            Console.Write(' ');
        }
    }
}
