
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

                Console.WriteLine("          JÁTÉKOK MENÜJE");
                Console.WriteLine();

                Console.WriteLine("1 - Nyerőgép");
                Console.WriteLine("2 - Kockajáték");
                Console.WriteLine("3 - Blackjack");
                Console.WriteLine("4 - Rulett");
                Console.WriteLine("5 - Texas Hold'em póker");
                Console.WriteLine("6 - Szerencsekerek");
                Console.WriteLine("7 - Esélyek megtekintése");
                Console.WriteLine("8 - Kilépés");

                Console.WriteLine();
                Console.WriteLine("--------------------------------");
                Console.WriteLine("Egyenleg: " + penz + " pont");
                Console.WriteLine("--------------------------------");

                Console.WriteLine();
                Console.Write("Választás: ");

                ConsoleKeyInfo gomb = Console.ReadKey(true);

                // Nyerőgép
                if (gomb.Key == ConsoleKey.D1 ||
                    gomb.Key == ConsoleKey.NumPad1)
                {
                    Nyerogep nyerogep = new Nyerogep();
                    penz = nyerogep.NyerogepInditasa(penz);
                }

                // Kockajáték
                else if (gomb.Key == ConsoleKey.D2 ||
                         gomb.Key == ConsoleKey.NumPad2)
                {
                    Kockajatek kockajatek = new Kockajatek();
                    penz = kockajatek.KockajatekInditasa(penz);
                }

                // Blackjack
                else if (gomb.Key == ConsoleKey.D3 ||
                         gomb.Key == ConsoleKey.NumPad3)
                {
                    Blackjack blackjack = new Blackjack();
                    penz = blackjack.BlackjackInditasa(penz);
                }

                // Rulett
                else if (gomb.Key == ConsoleKey.D4 ||
                         gomb.Key == ConsoleKey.NumPad4)
                {
                    Roulette roulette = new Roulette();
                    penz = roulette.RouletteInditasa(penz);
                }

                // Texas Hold'em póker
                else if (gomb.Key == ConsoleKey.D5 ||
                         gomb.Key == ConsoleKey.NumPad5)
                {
                    Poker poker = new Poker();
                    penz = poker.PokerInditasa(penz);
                }

                // Szerencsekerek
                else if (gomb.Key == ConsoleKey.D6 ||
                         gomb.Key == ConsoleKey.NumPad6)
                {
                    Szerencsekerek szerencsekerek = new Szerencsekerek();
                    penz = szerencsekerek.SzerencsekerekInditasa(penz);
                }

                // Esélyek
                else if (gomb.Key == ConsoleKey.D7 ||
                         gomb.Key == ConsoleKey.NumPad7)
                {
                    EselyekMegjelenitese();
                }

                // Kilépés
                else if (gomb.Key == ConsoleKey.D8 ||
                         gomb.Key == ConsoleKey.NumPad8)
                {
                    VegsoEredmeny();
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

            return penz;
        }

        private void EselyekMegjelenitese()
        {
            Console.Clear();

            Console.WriteLine("================================");
            Console.WriteLine("          JÁTÉK ESÉLYEK");
            Console.WriteLine("================================");
            Console.WriteLine();

            Console.WriteLine("Nyerőgép: 10%");
            Console.WriteLine("Kockajáték: 17%");
            Console.WriteLine("Blackjack: 42%");
            Console.WriteLine();
            Console.WriteLine("Rulett:");
            Console.WriteLine("- Egy szám: 2,70%");
            Console.WriteLine("- Szín: 48,65%");
            Console.WriteLine("- Páros/páratlan: 48,65%");
            Console.WriteLine();
            Console.WriteLine("Texas Hold'em:");
            Console.WriteLine("- Az eredmény a kiosztott lapoktól függ.");
            Console.WriteLine("- A játékos és a gép 7 lapból választja");
            Console.WriteLine("  ki a legerősebb 5 lapos kombinációt.");
            Console.WriteLine();

            Console.WriteLine("Az esélyek tájékoztató jellegűek.");
            Console.WriteLine();
            Console.WriteLine("Visszalépéshez nyomj meg egy gombot!");

            Console.ReadKey(true);
        }

        private void VegsoEredmeny()
        {
            Console.Clear();

            Console.WriteLine("================================");
            Console.WriteLine("            JÁTÉK VÉGE");
            Console.WriteLine("================================");
            Console.WriteLine();

            Console.WriteLine("Megmaradt egyenleged: "
                + penz + " pont");

            Console.WriteLine("Végső pontszámod: "
                + penz + " pont");

            Console.WriteLine();
            Console.WriteLine("Köszönjük a játékot!");

            Console.WriteLine();
            Console.WriteLine("Készítette:");
            Console.WriteLine("Dobrán Attila");
            Console.WriteLine("Csáki Áron");
            Console.WriteLine("Kocsis Károly");

            Console.WriteLine();
            Console.WriteLine("Nyomj meg egy gombot a kilépéshez...");

            Console.ReadKey(true);
        }
    }
}
