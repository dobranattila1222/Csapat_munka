
using System;

namespace Casino_csapat
{
    internal class Jatekok
    {
        private int penz;

        public Jatekok(int kezdoPenz)
        {
            penz = kezdoPenz;
        }

        public int JatekokMenu()
        {
            bool fut = true;

            while (fut)
            {
                Console.Clear();

                Console.WriteLine("================================");
                Console.WriteLine("       GRAND CASINO ROYALE");
                Console.WriteLine("================================");
                Console.WriteLine();

                Console.WriteLine("JÁTÉKOK MENÜJE");
                Console.WriteLine();

                Console.WriteLine("1 - Nyerőgép");
                Console.WriteLine("2 - Kockajáték");
                Console.WriteLine("3 - Blackjack");
                Console.WriteLine("4 - Esélyek megtekintése");
                Console.WriteLine("5 - Kilépés");

                Console.WriteLine();
                Console.WriteLine("Egyenleg: " + penz + " Ft");
                Console.WriteLine();
                Console.Write("Választás: ");

                ConsoleKeyInfo gomb = Console.ReadKey(true);

                if (gomb.Key == ConsoleKey.D1 || gomb.Key == ConsoleKey.NumPad1)
                {
                    Nyerogep nyerogep = new Nyerogep();
                    penz = nyerogep.NyerogepInditasa(penz);
                }
                else if (gomb.Key == ConsoleKey.D2 || gomb.Key == ConsoleKey.NumPad2)
                {
                    Kockajatek kockajatek = new Kockajatek();
                    penz = kockajatek.KockajatekInditasa(penz);
                }
                else if (gomb.Key == ConsoleKey.D3 || gomb.Key == ConsoleKey.NumPad3)
                {
                    Blackjack blackjack = new Blackjack();
                    penz = blackjack.BlackjackInditasa(penz);
                }
                else if (gomb.Key == ConsoleKey.D4 || gomb.Key == ConsoleKey.NumPad4)
                {
                    EselyekMegjelenitese();
                }
                else if (gomb.Key == ConsoleKey.D5 || gomb.Key == ConsoleKey.NumPad5)
                {
                    VegsoEredmeny();
                    fut = false;
                }
            }

            return penz;
        }

        private void EselyekMegjelenitese()
        {
            Console.Clear();
            Console.WriteLine("JÁTÉK ESÉLYEK");
            Console.WriteLine();
            Console.WriteLine("Nyerőgép: 10%");
            Console.WriteLine("Kockajáték: 17%");
            Console.WriteLine("Blackjack: 42%");
            Console.WriteLine();
            Console.WriteLine("Az esélyek tájékoztató jellegűek.");
            Console.WriteLine();
            Console.WriteLine("Visszalépéshez nyomj meg egy gombot!");
            Console.ReadKey(true);
        }

        private void VegsoEredmeny()
        {
            Console.Clear();
            Console.WriteLine("JÁTÉK VÉGE");
            Console.WriteLine();
            Console.WriteLine("Megmaradt pénzed: " + penz + " Ft");
            Console.WriteLine("Végső pontszámod: " + penz + " pont");
            Console.WriteLine();
            Console.WriteLine("Köszönjük a játékot!");
            Console.ReadKey(true);
        }
    }
}
