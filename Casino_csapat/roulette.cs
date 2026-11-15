
using System;

namespace Casino_csapat
{
    internal class Roulette
    {
        private Random veletlen = new Random();

        public int RouletteInditasa(int penz)
        {
            bool fut = true;

            while (fut)
            {
                Console.Clear();

                Console.WriteLine("================================");
                Console.WriteLine("           RULETT");
                Console.WriteLine("================================");
                Console.WriteLine();

                Console.WriteLine("Egyenleg: " + penz + " Ft");
                Console.WriteLine();

                Console.WriteLine("1 - Fogadás egy számra");
                Console.WriteLine("2 - Fogadás színre");
                Console.WriteLine("3 - Fogadás páros vagy páratlan számra");
                Console.WriteLine("4 - Vissza a játékok menüjébe");
                Console.WriteLine();

                ConsoleKeyInfo gomb = Console.ReadKey(true);

                if (gomb.Key == ConsoleKey.D4 ||
                    gomb.Key == ConsoleKey.NumPad4)
                {
                    fut = false;
                    continue;
                }

                if (gomb.Key != ConsoleKey.D1 &&
                    gomb.Key != ConsoleKey.NumPad1 &&
                    gomb.Key != ConsoleKey.D2 &&
                    gomb.Key != ConsoleKey.NumPad2 &&
                    gomb.Key != ConsoleKey.D3 &&
                    gomb.Key != ConsoleKey.NumPad3)
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

                int valasztas = 0;
                string szinValasztas = "";
                string paritasValasztas = "";

                if (gomb.Key == ConsoleKey.D1 ||
                    gomb.Key == ConsoleKey.NumPad1)
                {
                    Console.Write("Válassz egy számot (0-36): ");

                    if (!int.TryParse(Console.ReadLine(), out valasztas) ||
                        valasztas < 0 || valasztas > 36)
                    {
                        Console.WriteLine("Érvénytelen szám!");
                        Console.ReadKey(true);
                        continue;
                    }
                }
                else if (gomb.Key == ConsoleKey.D2 ||
                         gomb.Key == ConsoleKey.NumPad2)
                {
                    Console.WriteLine("1 - Piros");
                    Console.WriteLine("2 - Fekete");
                    Console.Write("Választás: ");

                    string szin = Console.ReadLine();

                    if (szin == "1")
                        szinValasztas = "Piros";
                    else if (szin == "2")
                        szinValasztas = "Fekete";
                    else
                    {
                        Console.WriteLine("Érvénytelen választás!");
                        Console.ReadKey(true);
                        continue;
                    }
                }
                else
                {
                    Console.WriteLine("1 - Páros");
                    Console.WriteLine("2 - Páratlan");
                    Console.Write("Választás: ");

                    string paritas = Console.ReadLine();

                    if (paritas == "1")
                        paritasValasztas = "Páros";
                    else if (paritas == "2")
                        paritasValasztas = "Páratlan";
                    else
                    {
                        Console.WriteLine("Érvénytelen választás!");
                        Console.ReadKey(true);
                        continue;
                    }
                }

                penz -= tet;

                int eredmeny = veletlen.Next(0, 37);

                string[] pirosSzamok =
                {
                    "1", "3", "5", "7", "9", "12", "14",
                    "16", "18", "19", "21", "23", "25",
                    "27", "30", "32", "34", "36"
                };

                string eredmenySzin = "Zöld";

                if (eredmeny != 0)
                {
                    if (Array.Exists(pirosSzamok,
                        x => x == eredmeny.ToString()))
                    {
                        eredmenySzin = "Piros";
                    }
                    else
                    {
                        eredmenySzin = "Fekete";
                    }
                }

                int nyeremeny = 0;

                Console.Clear();

                Console.WriteLine("================================");
                Console.WriteLine("         RULETT EREDMÉNY");
                Console.WriteLine("================================");
                Console.WriteLine();

                Console.WriteLine("Kisorsolt szám: " + eredmeny);
                Console.WriteLine("Szín: " + eredmenySzin);
                Console.WriteLine();

                if (gomb.Key == ConsoleKey.D1 ||
                    gomb.Key == ConsoleKey.NumPad1)
                {
                    if (valasztas == eredmeny)
                    {
                        nyeremeny = tet * 36;
                        Console.WriteLine("ELTALÁLTAD A SZÁMOT!");
                    }
                    else
                    {
                        Console.WriteLine("Nem találtad el a számot.");
                    }
                }
                else if (gomb.Key == ConsoleKey.D2 ||
                         gomb.Key == ConsoleKey.NumPad2)
                {
                    if (szinValasztas == eredmenySzin)
                    {
                        nyeremeny = tet * 2;
                        Console.WriteLine("ELTALÁLTAD A SZÍNT!");
                    }
                    else
                    {
                        Console.WriteLine("Nem találtad el a színt.");
                    }
                }
                else
                {
                    bool paros = eredmeny != 0 && eredmeny % 2 == 0;

                    if ((paritasValasztas == "Páros" && paros) ||
                        (paritasValasztas == "Páratlan" &&
                         eredmeny != 0 && !paros))
                    {
                        nyeremeny = tet * 2;
                        Console.WriteLine("ELTALÁLTAD!");
                    }
                    else
                    {
                        Console.WriteLine("Nem találtad el.");
                    }
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
