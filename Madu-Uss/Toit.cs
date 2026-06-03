using System;
using System.Collections.Generic;
using System.Linq; // Vajalik LINQ kontrolliks
using System.Text;

namespace Madu_Uss
{
    public class Toit
    {
        private Random rnd = new Random();
        private int ekraaniLaius;
        private int ekraaniKõrgus;
        public Punkt Asukoht { get; private set; }

        public Toit(int laius, int kõrgus)
        {
            ekraaniLaius = laius;
            ekraaniKõrgus = kõrgus;
        }

        // Uus meetod välistab toidu tekkimise ussi keha sisse
        public void LooUusToit(List<Punkt> ussiKeha)
        {
            while (true)
            {
                int x = rnd.Next(2, ekraaniLaius - 2);
                int y = rnd.Next(2, ekraaniKõrgus - 2);

                // LINQ kontroll: kui ussi kehas EI OLE ühtegi punkti nende koordinaatidega
                if (!ussiKeha.Any(p => p.X == x && p.Y == y))
                {
                    Asukoht = new Punkt(x, y, '@', ConsoleColor.Yellow);
                    Asukoht.Joonista();
                    break; // Väljub tsüklist, kui asukoht on turvaline
                }
            }
        }
    }
}
