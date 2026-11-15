
using System;

namespace Casino_csapat
{
    internal class Nyerogep
    {
        private Random veletlen = new Random();

        public int NyerogepInditasa(int penz)
        {
            bool fut = true;

            string[] szimbolumok =
            {
                "Cseresznye",
                "Citrom",
                "Harang",
                "Csillag",
                "Hetes"
            };

            while (fut)
            {
                Console.Clear();

                Console.WriteLine("================================");
                Console.WriteLine("          NYEROGEP");
                Console.WriteLine("================================");
                Console.WriteLine();

                Console.WriteLine("Egyenleg: " + penz + " Ft");
                Console.WriteLine();

                Console.WriteLine("1 - Pörgetés");
                Console.WriteLine("2 - Vissza a játékok menüjébe");
                Console.WriteLine();

                ConsoleKeyInfo gomb = Console.ReadKey(true);

                if (gomb.Key == ConsoleKey.D1 ||
                    gomb.Key == ConsoleKey.NumPad1)
                {
                    Console.Clear();

                    Console.WriteLine("Egyenleg: " + penz + " Ft");
                    Console.WriteLine();

                    Console.Write("Mekkora legyen a tét? ");

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

                    string elso = szimbolumok[veletlen.Next(szimbolumok.Length)];
                    string masodik = szimbolumok[veletlen.Next(szimbolumok.Length)];
                    string harmadik = szimbolumok[veletlen.Next(szimbolumok.Length)];

                    Console.WriteLine();
                    Console.WriteLine("A tárcsák forognak...");
                    Console.WriteLine();

                    Console.WriteLine("[" + elso + "] [" + masodik + "] [" + harmadik + "]");

                    int nyeremeny = 0;

                    if (elso == masodik && masodik == harmadik)
                    {
                        nyeremeny = tet * 5;
                        Console.WriteLine();
                        Console.WriteLine("JACKPOT! Mindhárom szimbólum egyezik!");
                    }
                    else if (elso == masodik ||
                             elso == harmadik ||
                             masodik == harmadik)
                    {
                        nyeremeny = tet * 2;
                        Console.WriteLine();
                        Console.WriteLine("Szép! Két szimbólum egyezik!");
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine("Sajnos most nem nyertél.");
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
                else if (gomb.Key == ConsoleKey.D2 ||
                         gomb.Key == ConsoleKey.NumPad2)
                {
                    fut = false;
                }
            }

            return penz;
        }
    }
}
