
using System;
using System.Collections.Generic;

namespace Casino_csapat
{
    internal class Blackjack
    {
        private Random veletlen = new Random();

        public int BlackjackInditasa(int penz)
        {
            bool fut = true;

            while (fut)
            {
                Console.Clear();

                Console.WriteLine("================================");
                Console.WriteLine("           BLACKJACK");
                Console.WriteLine("================================");
                Console.WriteLine();

                Console.WriteLine("Egyenleg: " + penz + " Ft");
                Console.WriteLine();
                Console.WriteLine("[1] - Új játék");
                Console.WriteLine("[2] - Vissza a játékok menüjébe");

                ConsoleKeyInfo gomb = Console.ReadKey(true);

                if (gomb.Key == ConsoleKey.D2 ||
                    gomb.Key == ConsoleKey.NumPad2)
                {
                    fut = false;
                    continue;
                }

                if (gomb.Key != ConsoleKey.D1 &&
                    gomb.Key != ConsoleKey.NumPad1)
                {
                    continue;
                }

                Console.Clear();

                Console.WriteLine("Egyenleg: " + penz + " Ft");
                Console.WriteLine();
                Console.Write("Add meg a tétet: ");

                if (!int.TryParse(Console.ReadLine(), out int tet))
                {
                    Console.WriteLine("Érvénytelen összeg!");
                    Console.ReadKey(true);
                    continue;
                }

                if (tet <= 0 || tet > penz)
                {
                    Console.WriteLine("Nem megfelelő tét!");
                    Console.ReadKey(true);
                    continue;
                }

                penz -= tet;

                List<int> jatekosLapok = new List<int>();
                List<int> osztoLapok = new List<int>();

                jatekosLapok.Add(LapHuzasa());
                jatekosLapok.Add(LapHuzasa());

                osztoLapok.Add(LapHuzasa());
                osztoLapok.Add(LapHuzasa());

                bool jatekosHuz = true;

                while (jatekosHuz)
                {
                    Console.Clear();

                    Console.WriteLine("BLACKJACK");
                    Console.WriteLine();

                    Console.WriteLine("A te lapjaid: "
                        + string.Join(", ", jatekosLapok));

                    Console.WriteLine("Pontszámod: "
                        + Pontszam(jatekosLapok));

                    Console.WriteLine();
                    Console.WriteLine("Az osztó egyik lapja: "
                        + osztoLapok[0]);

                    Console.WriteLine();
                    Console.WriteLine("1 - Lapot kérek");
                    Console.WriteLine("2 - Megállok");

                    ConsoleKeyInfo valasztas = Console.ReadKey(true);

                    if (valasztas.Key == ConsoleKey.D1 ||
                        valasztas.Key == ConsoleKey.NumPad1)
                    {
                        jatekosLapok.Add(LapHuzasa());

                        if (Pontszam(jatekosLapok) > 21)
                        {
                            jatekosHuz = false;
                        }
                    }
                    else if (valasztas.Key == ConsoleKey.D2 ||
                             valasztas.Key == ConsoleKey.NumPad2)
                    {
                        jatekosHuz = false;
                    }
                }

                int jatekosPont = Pontszam(jatekosLapok);

                if (jatekosPont <= 21)
                {
                    while (Pontszam(osztoLapok) < 17)
                    {
                        osztoLapok.Add(LapHuzasa());
                    }
                }

                int osztoPont = Pontszam(osztoLapok);

                Console.Clear();

                Console.WriteLine("BLACKJACK - EREDMÉNY");
                Console.WriteLine();

                Console.WriteLine("A te lapjaid: "
                    + string.Join(", ", jatekosLapok));

                Console.WriteLine("Pontszámod: " + jatekosPont);

                Console.WriteLine();

                Console.WriteLine("Az osztó lapjai: "
                    + string.Join(", ", osztoLapok));

                Console.WriteLine("Osztó pontszáma: " + osztoPont);

                Console.WriteLine();

                int nyeremeny = 0;

                if (jatekosPont > 21)
                {
                    Console.WriteLine("Túllépted a 21 pontot! Vesztettél.");
                }
                else if (osztoPont > 21 || jatekosPont > osztoPont)
                {
                    nyeremeny = tet * 2;
                    Console.WriteLine("Gratulálok! Nyertél!");
                }
                else if (jatekosPont == osztoPont)
                {
                    nyeremeny = tet;
                    Console.WriteLine("Döntetlen!");
                }
                else
                {
                    Console.WriteLine("Az osztó nyert!");
                }

                penz += nyeremeny;

                Console.WriteLine();
                Console.WriteLine("Nyeremény: " + nyeremeny + " Ft");
                Console.WriteLine("Új egyenleg: " + penz + " Ft");

                if (penz == 0)
                {
                    Console.WriteLine();
                    Console.WriteLine("Elfogyott az egyenleged!");
                    fut = false;
                }

                Console.WriteLine();
                Console.WriteLine("Nyomj meg egy gombot...");
                Console.ReadKey(true);
            }
            return penz;
        }

        private int LapHuzasa()
        {
            return veletlen.Next(2, 12);
        }

        private int Pontszam(List<int> lapok)
        {
            int osszeg = 0;

            foreach (int lap in lapok)
            {
                osszeg += lap;
            }

            return osszeg;
        }
    }
}
