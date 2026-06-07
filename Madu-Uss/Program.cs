using System;
using System.Linq;
using System.Threading;

namespace Madu_Uss
{
    class Program
    {
        static void Main()
        {
            Console.Title = "Madu-Uss Arkaad";
            Console.CursorVisible = false;

            // UUS: Kutsume välja interaktiivse peamenüü ja saame sealt taseme numbri
            //  Внедрение нового меню и динамических настроек экрана
            int tase = KuvaMenüü();

            MänguSeaded seaded = new MänguSeaded(tase);
            Console.Clear();

            // Akna ja puhvri suurus seadistatakse pärast menüüd
            // Запас по высоте (+4), чтобы сверху поместился интерфейс счета
            int windowWidth = seaded.Laius + 4;
            int windowHeight = seaded.Kõrgus + 4;
            Console.SetWindowSize(windowWidth, windowHeight);
            Console.SetBufferSize(windowWidth, windowHeight);

            // Objektide lähtestamine
            Kaart kaart = new Kaart(seaded.Laius, seaded.Kõrgus);
            Uss uss = new Uss(10, 10, 3);
            Toit toit = new Toit(seaded.Laius, seaded.Kõrgus);

            // Инициализация нашего нового объекта-врага
            Vaenlane vaenlane = new Vaenlane(seaded.Laius, seaded.Kõrgus);
            int skoor = 0;

            kaart.Joonista();
            toit.LooUusToit(uss.Keha);
            vaenlane.Joonista();

            // Вызов нашего кастомного метода отрисовки счета в самом верху (Y=0)
            UuendaSkoor(skoor);

            // MÄNGU PEATSÜKKEL
            while (true)
            {
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

                uss.Liigu();

                // Моя доработка: запуск движения врага на каждом кадре
                vaenlane.Liigu(kaart.Takistused);

                Punkt pea = uss.HangiPea();

                if (kaart.Takistused.Any(t => t.X == pea.X && t.Y == pea.Y))
                {
                    Heliefektid.MängiKaotust();
                    break;
                }

                if (uss.KasPõrkasVastuEnnast())
                {
                    Heliefektid.MängiKaotust();
                    break;
                }

                // Моя доработка: Проверка столкновения змейки с врагом (Game Over)
                if (pea.X == vaenlane.Asukoht.X && pea.Y == vaenlane.Asukoht.Y)
                {
                    Heliefektid.MängiKaotust();
                    break;
                }

                if (pea.X == toit.Asukoht.X && pea.Y == toit.Asukoht.Y)
                {
                    skoor += 10;
                    UuendaSkoor(skoor);
                    uss.Kasva();
                    toit.LooUusToit(uss.Keha);
                    Heliefektid.MängiSöömist();
                }

                Thread.Sleep(seaded.KiirusMS);
            }

            // Mängu lõpetamise aken
            Console.Clear();
            Console.SetWindowSize(50, 20);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("MÄNG LÄBI!");
            Console.ResetColor();
            Console.Write($"Sinu skoor: {skoor}\nSisesta oma nimi: ");

            string nimi = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(nimi)) nimi = "Mängija";

            Edetabel.Salvesta(nimi, skoor);
            Edetabel.KuvaEdetabel();

            Console.ReadLine();
        }

        // UUS: Interaktiivne nooleklahvidega menüü
        static int KuvaMenüü()
        {
            int valitudIndeks = 0; // Индекс подсвеченного пункта
            string[] valikud = { " Tase 1 (Kerge) ", " Tase 2 (Keskmine) ", " Tase 3 (Raske) " };

            Console.SetWindowSize(50, 15);
            Console.SetBufferSize(50, 15);

            while (true)
            {
                Console.Clear();
                Console.SetCursorPosition(12, 2);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("=== MADU-USS ARKAAD ===");
                Console.ResetColor();
                Console.SetCursorPosition(10, 4);
                Console.WriteLine("Vali raskusaste nooleklahvidega:");

                for (int i = 0; i < valikud.Length; i++)
                {
                    Console.SetCursorPosition(14, 7 + i);

                    if (i == valitudIndeks) // Если пункт выбран, инвертируем цвета (зеленый фон, белый текст)
                    {
                        // Toome aktiivse valiku värviga esile
                        Console.BackgroundColor = ConsoleColor.DarkGreen;
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine(valikud[i]);
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.WriteLine(valikud[i]);
                    }
                }

                // Считываем нажатие клавиши в меню
                ConsoleKeyInfo klahv = Console.ReadKey(true);

                if (klahv.Key == ConsoleKey.UpArrow)
                {
                    valitudIndeks = (valitudIndeks == 0) ? valikud.Length - 1 : valitudIndeks - 1;
                }
                else if (klahv.Key == ConsoleKey.DownArrow)
                {
                    valitudIndeks = (valitudIndeks == valikud.Length - 1) ? 0 : valitudIndeks + 1;
                }
                else if (klahv.Key == ConsoleKey.Enter)
                {
                    // Tagastame taseme numbri (1, 2 või 3)
                    return valitudIndeks + 1;
                }
            }
        }

        static void UuendaSkoor(int skoor)
        {
            Console.SetCursorPosition(2, 0); // Перемещаем курсор на самую верхнюю строчку (Y=0) над картой
            Console.ForegroundColor = ConsoleColor.Cyan; // Неоновый голубой цвет для интерфейса
            Console.Write($"SKOOR: {skoor}  ");  // Пробелы в конце стирают старые символы
            Console.ResetColor(); 
        }
    }
}