
using System;
using System.Threading;

namespace Casino_csapat
{
    internal class Szerencsekerek
    {
        public int SzerencsekerekInditasa(int penz)
        {
            Random random = new Random();
            bool fut = true;

            string[] mezok =
            {
                "VESZTÉS", "x2", "VISSZA", "x3", "VESZTÉS", "x5"
            };

            while (fut)
            {
                Console.Clear();

                Console.WriteLine("================================");
                Console.WriteLine("          SZERENCSEKERÉK");
                Console.WriteLine("================================");
                Console.WriteLine();

                Console.WriteLine("Egyenleg: " + penz + " Ft");
                Console.WriteLine();

                Console.WriteLine("1 - Pörgetés");
                Console.WriteLine("2 - Vissza a játékok menüjébe");

                Console.WriteLine();
                Console.Write("Választás: ");

                ConsoleKeyInfo gomb = Console.ReadKey(true);

                if (gomb.Key == ConsoleKey.D1 ||
                    gomb.Key == ConsoleKey.NumPad1)
                {
                    Console.Clear();

                    if (penz <= 0)
                    {
                        Console.WriteLine("Nincs elegendő pénzed!");
                        Console.ReadKey(true);
                        break;
                    }

                    Console.WriteLine("Mekkora legyen a tét?");
                    Console.WriteLine("Egyenleg: " + penz + " Ft");
                    Console.Write("Tét: ");

                    int tet;

                    if (!int.TryParse(Console.ReadLine(), out tet) ||
                        tet <= 0 || tet > penz)
                    {
                        Console.WriteLine("Érvénytelen tét!");
                        Console.ReadKey(true);
                        continue;
                    }

                    penz -= tet;

                    // A nyerő mező előre kisorsolása
                    int eredmeny = random.Next(0, mezok.Length);

                    Console.Clear();
                    Console.WriteLine("================================");
                    Console.WriteLine("          SZERENCSEKERÉK");
                    Console.WriteLine("================================");
                    Console.WriteLine();

                    Console.WriteLine("A kerék forog...");
                    Console.WriteLine();

                    // Pörgetési animáció
                    int aktualisMezo = 0;

                    for (int i = 0; i < 24; i++)
                    {
                        aktualisMezo = random.Next(0, mezok.Length);

                        Console.Write("\r       [ " + mezok[aktualisMezo] + " ]       ");

                        // Fokozatos lassulás
                        Thread.Sleep(70 + i * 18);
                    }

                    // A tényleges eredmény megjelenítése
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine("A KERÉK MEGÁLLT!");
                    Console.WriteLine();

                    Thread.Sleep(700);

                    Console.WriteLine("Eredmény: " + mezok[eredmeny]);
                    Console.WriteLine();

                    switch (eredmeny)
                    {
                        case 0:
                            Console.WriteLine("Sajnos nem nyertél!");
                            break;

                        case 1:
                            Console.WriteLine("2-szeres nyeremény!");
                            penz += tet * 2;
                            break;

                        case 2:
                            Console.WriteLine("Visszakaptad a téted!");
                            penz += tet;
                            break;

                        case 3:
                            Console.WriteLine("3-szoros nyeremény!");
                            penz += tet * 3;
                            break;

                        case 4:
                            Console.WriteLine("Sajnos nem nyertél!");
                            break;

                        case 5:
                            Console.WriteLine("5-szörös nyeremény!");
                            penz += tet * 5;
                            break;
                    }

                    Console.WriteLine();
                    Console.WriteLine("Jelenlegi egyenleg: " + penz + " Ft");

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
