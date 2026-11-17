using System;

namespace Casino_csapat
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string kivalasztottNehezseg = "Nincs kiválasztva";
            int kezdoToke = 0;
            bool fut = true;

            while (fut)
            {
                Console.Clear();

                // Cím
                string cim = "\\\\||||Grand Casino Royale||||////";
                int kozep = (Console.WindowWidth - cim.Length) / 2;

                Console.SetCursorPosition(kozep, 2);
                Console.WriteLine(cim);

                // Főmenü
                Console.WriteLine();
                Console.WriteLine("[1] Új játék kezdése");
                Console.WriteLine("[2] Nehézség kiválasztása");
                Console.WriteLine("[3] Kilépés");

                Console.WriteLine();
                Console.WriteLine("Kiválasztott nehézség: " + kivalasztottNehezseg);
                Console.WriteLine("Kezdőtőke: " + kezdoToke + " Ft");

                ConsoleKeyInfo gomb = Console.ReadKey(true);

                // Új játék
                if (gomb.Key == ConsoleKey.D1 ||
                    gomb.Key == ConsoleKey.NumPad1)
                {
                    if (kivalasztottNehezseg == "Nincs kiválasztva")
                    {
                        Console.WriteLine();
                        Console.WriteLine("Először válassz nehézségi szintet!");
                        Console.ReadKey(true);
                    }
                    else
                    {
                        Jatek jatek = new Jatek(kezdoToke);
                        kezdoToke = jatek.JatekInditasa();
                    }
                }

                // Nehézség kiválasztása
                else if (gomb.Key == ConsoleKey.D2 ||
                         gomb.Key == ConsoleKey.NumPad2)
                {
                    Console.Clear();

                    Nehezseg nehezseg = new Nehezseg();
                    nehezseg = nehezseg.NehezsegKivalasztasa();

                    if (nehezseg != null)
                    {
                        kivalasztottNehezseg = nehezseg.nehezsegiSzint;
                        kezdoToke = nehezseg.penz;

                        Console.Clear();

                        Console.WriteLine("Kiválasztott nehézség: "
                            + kivalasztottNehezseg);

                        Console.WriteLine("Kezdőtőke: "
                            + kezdoToke + " Ft");

                        Console.WriteLine();
                        Console.WriteLine("Nyomj meg egy gombot a folytatáshoz...");
                        Console.ReadKey(true);
                    }
                }

                // Kilépés
                else if (gomb.Key == ConsoleKey.D3 ||
                         gomb.Key == ConsoleKey.NumPad3)
                {
                    Console.Clear();
                    Console.WriteLine("Kilépés a játékból...");
                    fut = false;
                }

                // Hibás választás
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("Érvénytelen választás!");
                    Console.ReadKey(true);
                }
            }
        }
    }
}