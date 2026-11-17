
using System;

namespace Casino_csapat
{
    internal class Kockajatek
    {
        private Random veletlen = new Random();

        public int KockajatekInditasa(int penz)
        {
            bool fut = true;

            while (fut)
            {
                Console.Clear();

                Console.WriteLine("================================");
                Console.WriteLine("          KOCKAJATEK");
                Console.WriteLine("================================");
                Console.WriteLine();

                Console.WriteLine("Egyenleg: " + penz + " Ft");
                Console.WriteLine();

                Console.WriteLine("[1] - Új dobás");
                Console.WriteLine("[2] - Vissza a játékok menüjébe");
                Console.WriteLine();

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

                int jatekosElso = veletlen.Next(1, 7);
                int jatekosMasodik = veletlen.Next(1, 7);

                int gepElso = veletlen.Next(1, 7);
                int gepMasodik = veletlen.Next(1, 7);

                int jatekosOsszeg = jatekosElso + jatekosMasodik;
                int gepOsszeg = gepElso + gepMasodik;

                Console.WriteLine();
                Console.WriteLine("Dobás eredménye:");
                Console.WriteLine();

                Console.WriteLine("A te kockáid: "
                    + jatekosElso + " és " + jatekosMasodik);

                Console.WriteLine("Összesen: " + jatekosOsszeg);

                Console.WriteLine();

                Console.WriteLine("A gép kockái: "
                    + gepElso + " és " + gepMasodik);

                Console.WriteLine("Összesen: " + gepOsszeg);

                Console.WriteLine();

                int nyeremeny = 0;

                if (jatekosOsszeg > gepOsszeg)
                {
                    nyeremeny = tet * 2;
                    Console.WriteLine("Gratulálok! Nyertél!");
                }
                else if (jatekosOsszeg == gepOsszeg)
                {
                    nyeremeny = tet;
                    Console.WriteLine("Döntetlen! Visszakapod a tétedet.");
                }
                else
                {
                    Console.WriteLine("Sajnos a gép nyert!");
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
                Console.WriteLine("Nyomj meg egy gombot a folytatáshoz...");
                Console.ReadKey(true);
            }
            return penz;
        }
    }
}
