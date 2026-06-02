using System;
using System.Linq;
using System.Threading;

namespace Madu_Uss
{
    class Program
    {
        static void Main()
        {
            Console.CursorVisible = false;

            // 1. Mängu algus, raskusastme valik ja seadistamine
            Console.WriteLine("Vali raskus (1-3): ");
            int tase = int.Parse(Console.ReadLine());
            MänguSeaded seaded = new MänguSeaded(tase);

            Console.Clear();
            Console.SetWindowSize(seaded.Laius + 2, seaded.Kõrgus + 2);

            // Objektide algatamine vastavalt seadetele
            Kaart kaart = new Kaart(seaded.Laius, seaded.Kõrgus);
            Uss uss = new Uss(10, 10, 3);
            Toit toit = new Toit(seaded.Laius, seaded.Kõrgus);
            int skoor = 0;

            // Joonistame välisseinad ekraanile
            kaart.Joonista();

            // 2. Täiendatud mängu peatsükkel
            while (true)
            {
                // Sisendi lugemine ja suuna muutmise kontrollid
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo klahv = Console.ReadKey(true);
                    if (klahv.Key == ConsoleKey.UpArrow && uss.PraeguneSuund != Suund.Alla)
                        uss.PraeguneSuund = Suund.Üles;
                    else if (klahv.Key == ConsoleKey.DownArrow && uss.PraeguneSuund != Suund.Üles)
                        uss.PraeguneSuund = Suund.Alla;
                    else if (klahv.Key == ConsoleKey.LeftArrow && uss.PraeguneSuund != Suund.Paremale)
                        uss.PraeguneSuund = Suund.Vasakule;
                    else if (klahv.Key == ConsoleKey.RightArrow && uss.PraeguneSuund != Suund.Vasakule)
                        uss.PraeguneSuund = Suund.Paremale;
                }

                // Katkend failist Program.cs (Mängu tsükli seest):

                uss.Liigu();
                Punkt pea = uss.HangiPea();

                // 1. Kontroll: Kas uss põrkas vastu seina?
                if (kaart.Takistused.Any(t => t.X == pea.X && t.Y == pea.Y))
                {
                    Heliefektid.MängiKaotust();
                    break;
                }

                // 2. KONTROLL (UUS): Kas uss sõitis endale otsa?
                if (uss.KasPõrkasVastuEnnast())
                {
                    Heliefektid.MängiKaotust();
                    break; // Mäng läbi, väljume tsüklist
                }



                // Toidu söömise kontroll, skoori lisamine ja heliefekt
                if (pea.X == toit.Asukoht.X && pea.Y == toit.Asukoht.Y)
                {
                    skoor += 10;
                    uss.Kasva();
                    toit.LooUusToit();
                    Heliefektid.MängiSöömist();
                }

                // Dünaamiline kiirus vastavalt valitud tasemele
                Thread.Sleep(seaded.KiirusMS);
            }

            // 3. Mängu lõpetamine, nime küsimine ja edetabeli kuvamine
            Console.Clear();
            Console.SetWindowSize(50, 20); // Taastame mugava akna suuruse edetabeli jaoks
            Console.Write("Mäng läbi! Sinu skoor: " + skoor + "\nSisesta oma nimi: ");
            string nimi = Console.ReadLine();

            Edetabel.Salvesta(nimi, skoor);
            Edetabel.KuvaEdetabel();

            Console.ReadLine();
        }
    }
}