using System;
using System.Collections.Generic;
using System.Linq;

namespace Madu_Uss
{
    public class Vaenlane
    {
        private Random rnd = new Random();
        public Punkt Asukoht { get; private set; }
        private int ekraaniLaius;
        private int ekraaniKõrgus;

        public Vaenlane(int laius, int kõrgus)
        {
            ekraaniLaius = laius;
            ekraaniKõrgus = kõrgus;
            // Alustame vaenlase loomist mänguvälja keskosast
            Asukoht = new Punkt(laius / 2, kõrgus / 2, 'X', ConsoleColor.Magenta);
        }

        public void Joonista()
        {
            Asukoht.Joonista();
        }

        public void Liigu(List<Punkt> seinad)
        {
            // Kustutame vaenlase vana asukoha
            Asukoht.Kustuta();

            int katseid = 0;
            while (katseid < 10)
            {
                int uusX = Asukoht.X;
                int uusY = Asukoht.Y;

                // Juhuslik suunavalik (0: üles, 1: alla, 2: vasakule, 3: paremale)
                int suund = rnd.Next(0, 4);
                switch (suund)
                {
                    case 0: uusY--; break;
                    case 1: uusY++; break;
                    case 2: uusX--; break;
                    case 3: uusX++; break;
                }

                // Kontrollime, et uus asukoht ei põrkaks kokku seina takistusega
                if (!seinad.Any(s => s.X == uusX && s.Y == uusY))
                {
                    Asukoht.X = uusX;
                    Asukoht.Y = uusY;
                    break;
                }
                katseid++;
            }

            // Joonistame vaenlase uues asukohas
            Asukoht.Joonista();
        }
    }
}
