using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                //Cím
                string cim = "\\\\||||Grand Casino Royale||||////";
                int kozep = (Console.WindowWidth - cim.Length) / 2;

                Console.SetCursorPosition(kozep, 2);
                Console.WriteLine(cim);

                //választás
                Console.WriteLine("Új játék kezdése : 1 gomb");
                Console.WriteLine("Nehézség Kiválasztása : 2 gomb");
                Console.WriteLine("Játék Feladatása : 3 gomb");

                Console.WriteLine();
                Console.WriteLine("Kiválasztott nehézség: " + kivalasztottNehezseg);
                Console.WriteLine("Kezdőtőke: " + kezdoToke + " Ft");
                ConsoleKeyInfo gomb = Console.ReadKey();

                if (gomb.Key == ConsoleKey.D1)
                {
                    if (kivalasztottNehezseg == "Nincs kiválasztva")
                    {
                        Console.WriteLine("\nElőször válassz nehézségi szintet!");
                        Console.ReadKey();
                    }
                    else
                    {
                        Console.Clear();
                        Jatek jatek = new Jatek();
                        jatek.JatekInditasa();
                    }
                }
                else if (gomb.Key == ConsoleKey.D2)
                {
                    Console.Clear();

                    Nehezseg nehezseg = new Nehezseg();
                    nehezseg = nehezseg.NehezsegKivalasztasa();

                    if (nehezseg != null)
                    {
                        kivalasztottNehezseg = nehezseg.nehezsegiSzint;
                        kezdoToke = nehezseg.penz;

                        Console.Clear();

                        Console.WriteLine("Kiválasztott nehézség: " + nehezseg.nehezsegiSzint);
                        Console.WriteLine("Kezdőtőke: " + nehezseg.penz + " Ft");

                        Console.ReadKey();
                    }
                    else if (gomb.Key == ConsoleKey.D3)
                    {
                        Console.WriteLine("Kilépés...");
                        return;
                    }
                    else
                    {
                        Console.WriteLine("Érvénytelen választás!");
                         }
                }
            }
        }
    }
}