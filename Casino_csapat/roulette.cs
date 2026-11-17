
using System;
using System.Threading;

namespace Casino_csapat
{
    internal class Roulette
    {
        private Random veletlen = new Random();

        private readonly int[] pirosSzamok =
        {
            1, 3, 5, 7, 9, 12, 14, 16, 18,
            19, 21, 23, 25, 27, 30, 32, 34, 36
        };

        private readonly int[] kerekSorrend =
        {
            0, 32, 15, 19, 4, 21, 2, 25, 17,
            34, 6, 27, 13, 36, 11, 30, 8, 23,
            10, 5, 24, 16, 33, 1, 20, 14, 31,
            9, 22, 18, 29, 7, 28, 12, 35, 3,
            26
        };

        public int RouletteInditasa(int penz)
        {
            bool fut = true;

            while (fut)
            {
                Console.Clear();

                Console.WriteLine("================================");
                Console.WriteLine("          EURÓPAI RULETT");
                Console.WriteLine("================================");
                Console.WriteLine();

                Console.WriteLine("Virtuális pontok: " + penz);
                Console.WriteLine();

                Console.WriteLine("[1] - Rulett pörgetése");
                Console.WriteLine("[2] - Számok és színek megtekintése");
                Console.WriteLine("[3] - Vissza a játékok menüjébe");
                Console.WriteLine();

                Console.Write("Választás: ");

                ConsoleKeyInfo gomb = Console.ReadKey(true);

                if (gomb.Key == ConsoleKey.D3 ||
                    gomb.Key == ConsoleKey.NumPad3)
                {
                    fut = false;
                }
                else if (gomb.Key == ConsoleKey.D1 ||
                         gomb.Key == ConsoleKey.NumPad1)
                {
                    if (penz <= 0)
                    {
                        Console.WriteLine();
                        Console.WriteLine("Nincs több virtuális pontod!");
                        Console.ReadKey(true);
                        continue;
                    }

                    string tipp = TippBekerese();
                    int tet = TetBekerese(penz);

                    if (tet <= 0)
                        continue;

                    int eredmeny = PorgesAnimacio();

                    penz = EredmenyMegjelenitese(
                        eredmeny, tipp, tet, penz);

                    Console.WriteLine();
                    Console.WriteLine("Nyomj meg egy gombot...");
                    Console.ReadKey(true);
                }
                else if (gomb.Key == ConsoleKey.D2 ||
                         gomb.Key == ConsoleKey.NumPad2)
                {
                    SzinekMegjelenitese();
                }
            }

            return penz;
        }

        private int TetBekerese(int penz)
        {
            Console.Clear();

            Console.WriteLine("================================");
            Console.WriteLine("        PONTOK KIVÁLASZTÁSA");
            Console.WriteLine("================================");
            Console.WriteLine();

            Console.WriteLine("Jelenlegi pontszám: " + penz);
            Console.WriteLine();

            Console.Write("Hány pontot szeretnél felhasználni? ");

            int tet;

            while (!int.TryParse(Console.ReadLine(), out tet) ||
                   tet <= 0 || tet > penz)
            {
                Console.WriteLine("Érvénytelen mennyiség!");
                Console.Write("Próbáld újra: ");
            }

            return tet;
        }

        private string TippBekerese()
        {
            Console.Clear();

            Console.WriteLine("================================");
            Console.WriteLine("          RULETT TIPPEK");
            Console.WriteLine("================================");
            Console.WriteLine();

            Console.WriteLine("[1] Konkrét szám (0-36)");
            Console.WriteLine("[2] Szín (piros/fekete/zöld)");
            Console.WriteLine("[3] Páros vagy páratlan");
            Console.WriteLine();

            Console.Write("Választás: ");

            string valasztas = Console.ReadLine();

            if (valasztas == "1")
            {
                int szam;

                Console.Write("Melyik számra tippelsz (0-36)? ");

                while (!int.TryParse(Console.ReadLine(), out szam)
                       || szam < 0 || szam > 36)
                {
                    Console.Write("Érvénytelen szám. Újra: ");
                }

                return "SZAM:" + szam;
            }
            else if (valasztas == "2")
            {
                Console.Write("Piros, fekete vagy zöld? ");

                string szin = Console.ReadLine().Trim().ToLower();

                while (szin != "piros" &&
                       szin != "fekete" &&
                       szin != "zöld" &&
                       szin != "zold")
                {
                    Console.Write("Érvénytelen szín. Újra: ");
                    szin = Console.ReadLine().Trim().ToLower();
                }

                return "SZIN:" + szin;
            }
            else if (valasztas == "3")
            {
                Console.Write("Páros vagy páratlan? ");

                string paritas = Console.ReadLine().Trim().ToLower();

                while (paritas != "páros" &&
                       paritas != "paros" &&
                       paritas != "páratlan" &&
                       paritas != "paratlan")
                {
                    Console.Write("Érvénytelen választás. Újra: ");
                    paritas = Console.ReadLine().Trim().ToLower();
                }

                return "PARITAS:" + paritas;
            }

            Console.WriteLine("Érvénytelen választás!");
            Thread.Sleep(1000);

            return TippBekerese();
        }

        private int PorgesAnimacio()
        {
            int eredmeny = veletlen.Next(0, 37);

            int vegsoPozicio = Array.IndexOf(
                kerekSorrend, eredmeny);

            int kezdoPozicio = veletlen.Next(0, 37);

            int kulonbseg =
                (vegsoPozicio - kezdoPozicio + 37) % 37;

            int lepesek = 37 * 4 + kulonbseg;

            int pozicio = kezdoPozicio;

            Console.Clear();

            Console.WriteLine("================================");
            Console.WriteLine("          RULETT PÖRGÉS");
            Console.WriteLine("================================");
            Console.WriteLine();

            Console.WriteLine("A kerék forog...");
            Console.WriteLine();

            for (int i = 0; i < lepesek; i++)
            {
                int szam = kerekSorrend[pozicio];

                string szin = SzinLekerdezese(szam);

                Console.SetCursorPosition(0, 5);

                Console.WriteLine("                         ");
                Console.WriteLine("                         ");
                Console.WriteLine("                         ");

                Console.SetCursorPosition(0, 5);

                Console.WriteLine("       RULETTKERÉK");

                Console.ForegroundColor =
                    SzinKodLekerdezese(szin);

                Console.WriteLine("          " + szam);
                Console.WriteLine("          " + szin);

                Console.ResetColor();

                pozicio = (pozicio + 1) % 37;

                Thread.Sleep(25 + i * 2);
            }

            return eredmeny;
        }

        private int EredmenyMegjelenitese(
            int eredmeny, string tipp, int tet, int penz)
        {
            string szin = SzinLekerdezese(eredmeny);

            Console.Clear();

            Console.WriteLine("================================");
            Console.WriteLine("         RULETT EREDMÉNY");
            Console.WriteLine("================================");
            Console.WriteLine();

            Console.ForegroundColor =
                SzinKodLekerdezese(szin);

            Console.WriteLine("          " + eredmeny);
            Console.WriteLine("          " + szin);

            Console.ResetColor();

            Console.WriteLine();
            Console.WriteLine("--------------------------------");

            string[] adatok = tipp.Split(':');

            bool talalat = false;
           

            if (adatok[0] == "SZAM")
            {
                talalat = int.Parse(adatok[1]) == eredmeny;
            }
            else if (adatok[0] == "SZIN")
            {
                talalat =
                    adatok[1] == szin.ToLower() ||
                    (adatok[1] == "zold" && szin == "Zöld");
            }
            else if (adatok[0] == "PARITAS")
            {
                if (eredmeny != 0)
                {
                    bool paros = eredmeny % 2 == 0;

                    talalat = paros
                        ? adatok[1] == "páros" ||
                          adatok[1] == "paros"
                        : adatok[1] == "páratlan" ||
                          adatok[1] == "paratlan";
                }
            }

            Console.WriteLine();

            if (talalat)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("TALÁLAT!");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Most nem talált!");
            }

            Console.ResetColor();

            Console.WriteLine();
            Console.WriteLine("Felhasznált pontok: " + tet);

            // Ez a változat találgatós játék:
            // a pontszámot nem módosítja pénznyereményként.
            Console.WriteLine("Jelenlegi pontszám: " + penz);

            return penz;
        }

        private void SzinekMegjelenitese()
        {
            Console.Clear();

            Console.WriteLine("================================");
            Console.WriteLine("       RULETT SZÁMOK ÉS SZÍNEK");
            Console.WriteLine("================================");
            Console.WriteLine();

            for (int i = 0; i <= 36; i++)
            {
                string szin = SzinLekerdezese(i);

                Console.ForegroundColor =
                    SzinKodLekerdezese(szin);

                Console.Write("[" + i + " " + szin + "] ");

                Console.ResetColor();

                if ((i + 1) % 4 == 0)
                    Console.WriteLine();
            }

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Nyomj meg egy gombot...");
            Console.ReadKey(true);
        }

        private string SzinLekerdezese(int szam)
        {
            if (szam == 0)
                return "Zöld";

            if (Array.Exists(pirosSzamok,
                x => x == szam))
            {
                return "Piros";
            }

            return "Fekete";
        }

        private ConsoleColor SzinKodLekerdezese(string szin)
        {
            if (szin == "Piros")
                return ConsoleColor.Red;

            if (szin == "Fekete")
                return ConsoleColor.Gray;

            return ConsoleColor.Green;
        }
    }
}
