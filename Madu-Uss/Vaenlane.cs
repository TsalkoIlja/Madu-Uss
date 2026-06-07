using System;
using System.Collections.Generic;
using System.Linq; // Нужен для проверки стен через .Any()

namespace Madu_Uss
{
    public class Vaenlane
    {
        private Random rnd = new Random();
        public Punkt Asukoht { get; private set; } // Хранит точку, где сейчас находится враг
        private int ekraaniLaius;
        private int ekraaniKõrgus;

        public Vaenlane(int laius, int kõrgus)
        {
            ekraaniLaius = laius;
            ekraaniKõrgus = kõrgus;
            // Alustame vaenlase loomist mänguvälja keskosast
            // Спавним врага точно по центру поля, делаем его фиолетовым 'X'
            Asukoht = new Punkt(laius / 2, kõrgus / 2, 'X', ConsoleColor.Magenta);
        }

        public void Joonista()
        {
            Asukoht.Joonista();
        }

        // ИИ врага: делает случайный шаг и проверяет, чтобы там не было стены
        public void Liigu(List<Punkt> seinad)
        {
            // Kustutame vaenlase vana asukoha
            Asukoht.Kustuta(); // Стираем врага со старой позиции

            int katseid = 0;
            while (katseid < 10) // 10 попыток найти свободную клетку
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
                // Проверяем через LINQ: если на новых координатах нет стены, шаг разрешен
                if (!seinad.Any(s => s.X == uusX && s.Y == uusY))
                {
                    Asukoht.X = uusX;
                    Asukoht.Y = uusY;
                    break; // Успешный шаг, выходим из цикла поиска
                }
                katseid++;
            }

            // Joonistame vaenlase uues asukohas
            Asukoht.Joonista();
        }
    }
}
