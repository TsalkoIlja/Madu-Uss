using System;
using System.Collections.Generic;

namespace Madu_Uss
{
    public class Kaart
    {
        public List<Punkt> Takistused { get; private set; } = new List<Punkt>();

        public Kaart(int laius, int kõrgus)
        {
            // Alustame Y=1, et jätta rida Y=0 skooriakna jaoks vabaks
            for (int x = 0; x < laius; x++)
            {
                Takistused.Add(new Punkt(x, 1, '#', ConsoleColor.Red));
                Takistused.Add(new Punkt(x, kõrgus - 1, '#', ConsoleColor.Red));
            }
            for (int y = 1; y < kõrgus; y++)
            {
                Takistused.Add(new Punkt(0, y, '#', ConsoleColor.Red));
                Takistused.Add(new Punkt(laius - 1, y, '#', ConsoleColor.Red));
            }
        }

        public void Joonista()
        {
            foreach (var p in Takistused) p.Joonista();
        }
    }
}
