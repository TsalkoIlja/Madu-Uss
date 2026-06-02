using System;
using System.Collections.Generic;
using System.Text;

namespace Madu_Uss
{
    public class Uss
    {
        private List<Punkt> keha = new List<Punkt>();
        public Suund PraeguneSuund { get; set; }

        public Uss(int algX, int algY, int pikkus)
        {
            PraeguneSuund = Suund.Paremale; // Alguses liigub paremale

            for (int i = 0; i < pikkus; i++)
            {
                Punkt p = new Punkt(algX - i, algY, '*');
                keha.Add(p);
                p.Joonista();
            }
        }

        public void Liigu()
        {
            Punkt pea = keha.First();
            Punkt uusPea = new Punkt(pea.X, pea.Y, '*');

            switch (PraeguneSuund)
            {
                case Suund.Paremale: uusPea.X++; break;
                case Suund.Vasakule: uusPea.X--; break;
                case Suund.Alla: uusPea.Y++; break;
                case Suund.Üles: uusPea.Y--; break;
            }

            keha.Insert(0, uusPea);
            uusPea.Joonista();

            Punkt saba = keha.Last();
            saba.Kustuta();
            keha.Remove(saba);
        }

        public Punkt HangiPea()
        {
            return keha.First();
        }

        public void Kasva()
        {
            keha.Add(new Punkt(keha.Last().X, keha.Last().Y, '*'));
        }

        // Lisa see meetod Uss.cs klassi sisse

        public bool KasPõrkasVastuEnnast()
        {
            Punkt pea = keha.First();

            // Kontrollime kõiki kehaosi peale pea (Skip(1))
            // Kui mõni kehaosa on peaga samal koordinaadil, tagastatakse true
            return keha.Skip(1).Any(kehaOsa => kehaOsa.X == pea.X && kehaOsa.Y == pea.Y);
        }
    }
}
