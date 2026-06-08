using System;
using System.Collections.Generic;
using System.Linq;

namespace Madu_Uss
{
    public class Uss
    {
        private List<Punkt> keha = new List<Punkt>();
        private bool peabKasvama = false; // Lüliti turvaliseks kasvamiseks (Переключение для безопасного роста)

        public Suund PraeguneSuund { get; set; }
        public List<Punkt> Keha => keha; // Avalik omadus keha andmetele ligipääsuks (Общедоступное свойство для доступа к данным тела)

        public Uss(int algX, int algY, int pikkus)
        {
            PraeguneSuund = Suund.Paremale;

            for (int i = 0; i < pikkus; i++)
            {
                Punkt p = new Punkt(algX - i, algY, '*', ConsoleColor.Green);
                keha.Add(p);
                p.Joonista();
            }
        }

        public void Liigu()
        {
            Punkt pea = keha.First();
            Punkt uusPea = new Punkt(pea.X, pea.Y, '*', ConsoleColor.Green);

            switch (PraeguneSuund)
            {
                case Suund.Paremale: uusPea.X++; break;
                case Suund.Vasakule: uusPea.X--; break;
                case Suund.Alla: uusPea.Y++; break;
                case Suund.Üles: uusPea.Y--; break;
            }

            keha.Insert(0, uusPea);
            uusPea.Joonista();

            // Kui lüliti on aktiivne, siis saba ei kustutata (uss kasvab)
            // Если переключатель активен, хвост не удаляется (червяк растет)
            if (peabKasvama)
            {
                peabKasvama = false;
            }
            else
            {
                Punkt saba = keha.Last();
                saba.Kustuta();
                keha.Remove(saba);
            }
        }

        public Punkt HangiPea() => keha.First();

        public void Kasva() => peabKasvama = true; // Aktiveerib lüliti  (Активирует переключатель)

        public bool KasPõrkasVastuEnnast()
        {
            Punkt pea = keha.First();
            return keha.Skip(1).Any(kehaOsa => kehaOsa.X == pea.X && kehaOsa.Y == pea.Y);
        }
    }
}