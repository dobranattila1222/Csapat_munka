using System;

namespace Casino_csapat
{
    internal class Nehezseg
    {

        public int penz;
        public string nehezsegiSzint;

        public Nehezseg NehezsegKivalasztasa()
        {
            Console.WriteLine("Válassz nehézségi szintet!");
            Console.WriteLine("[1] - Könnyű (100 000 Ft)");
            Console.WriteLine("[2] - Közepes (50 000 Ft)");
            Console.WriteLine("[3] - Nehéz (25 000 Ft)");

            ConsoleKeyInfo valasztas = Console.ReadKey();

            if (valasztas.Key == ConsoleKey.D1)
            {
                penz = 100000;
                nehezsegiSzint = "Könnyű";
            }
            else if (valasztas.Key == ConsoleKey.D2)
            {
                penz = 50000;
                nehezsegiSzint = "Közepes";
            }
            else if (valasztas.Key == ConsoleKey.D3)
            {
                penz = 25000;
                nehezsegiSzint = "Nehéz";
            }
            else
            {
                Console.WriteLine("\nÉrvénytelen választás!");
                return null;
            }

            return this;
        }
    }
}