
using System;
using System.Threading;

namespace Casino_csapat
{
    internal class Jatek
    {
        private int penz;

        public Jatek(int kezdoPenz)
        {
            penz = kezdoPenz;
        }

        public int JatekInditasa()
        {
            Console.Clear();

            Console.WriteLine("================================");
            Console.WriteLine("       GRAND CASINO ROYALE");
            Console.WriteLine("================================");
            Console.WriteLine();

            Console.WriteLine("A játék előkészítése...");
            Console.WriteLine("Kezdőtőkéd: " + penz + " Ft");
            Console.WriteLine();

            Thread.Sleep(2000);

            Jatekok jatekok = new Jatekok(penz);
            penz = jatekok.JatekokMenu();

            return penz;
        }
    }
}
