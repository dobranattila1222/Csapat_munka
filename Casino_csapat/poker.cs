
using System;
using System.Collections.Generic;
using System.Linq;

namespace Casino_csapat
{
    internal class Poker
    {
        private int jatekosPont = 0;
        private int gepPont = 0;

        private const int CelPont = 10;

        private int nehezseg = 1;
        // Ennyi bajnoksági pont jár egy megnyert partiért.
        // Ez nem pénz és nem zseton, csak eredménypont.
        private int partiPontErteke = 1;
        private Random random = new Random();

        // Kizárólag játékbeli, virtuális zsetonok.
        private int aktualisPenz;
        private int nevezesiDij;
        private bool bajnoksagNevezesAktiv;
        private bool jutalomKifizetve;

        private class Kartya
        {
            public int Ertek;
            public string Szin;

            public Kartya(int ertek, string szin)
            {
                Ertek = ertek;
                Szin = szin;
            }

            public override string ToString()
            {
                string nev;

                switch (Ertek)
                {
                    case 11: nev = "J"; break;
                    case 12: nev = "Q"; break;
                    case 13: nev = "K"; break;
                    case 14: nev = "A"; break;
                    default: nev = Ertek.ToString(); break;
                }

                return nev + " " + Szin;
            }
        }

        private class KezErtek
        {
            public int Kategoria;
            public List<int> Erossegek = new List<int>();
            public string Nev;
            public List<Kartya> Lapok;
        }

        public int PokerInditasa(int penz)
        {
            aktualisPenz = penz;

            // Az első bajnokság előtt nevezni kell.
            if (!BajnoksagInditasa())
                return aktualisPenz;

            bool fut = true;

            while (fut)
            {
                Console.Clear();
                Fejlec();

                Console.WriteLine("1 - Új parti");
                Console.WriteLine("2 - Nehézség kiválasztása");
                Console.WriteLine("3 - Parti pontértékének beállítása");
                Console.WriteLine("4 - Új bajnokság indítása (nevezés)");
                Console.WriteLine("5 - Vissza a játékok menüjébe");
                Console.WriteLine();
                Console.WriteLine("Virtuális egyenleg: " + aktualisPenz + " pénz");

                Console.WriteLine();
                PontTabla();

                if (jatekosPont >= CelPont || gepPont >= CelPont)
                {
                    Console.WriteLine();
                    Console.WriteLine("A BAJNOKSÁG BEFEJEZŐDÖTT!");

                    if (jatekosPont >= CelPont)
                        Console.WriteLine("Megnyerted a bajnokságot!");
                    else
                        Console.WriteLine("A gép nyerte a bajnokságot!");

                    Console.WriteLine();
                    Console.WriteLine("Új bajnokság indításához válaszd a 4-es menüpontot.");
                    Console.WriteLine("A nevezési díj ötszörösét csak a teljes bajnokság megnyerésekor kapod meg.");
                }

                Console.WriteLine();
                Console.Write("Választás: ");

                ConsoleKeyInfo gomb = Console.ReadKey(true);

                if (gomb.Key == ConsoleKey.D1 ||
                    gomb.Key == ConsoleKey.NumPad1)
                {
                    if (jatekosPont < CelPont && gepPont < CelPont)
                    {
                        JatekKor();
                    }
                    else
                    {
                        Uzenet("A bajnokság már véget ért!");
                    }
                }
                else if (gomb.Key == ConsoleKey.D2 ||
                         gomb.Key == ConsoleKey.NumPad2)
                {
                    NehezsegValasztasa();
                }
                else if (gomb.Key == ConsoleKey.D3 ||
                         gomb.Key == ConsoleKey.NumPad3)
                {
                    PartiPontErtekeValasztasa();
                }
                else if (gomb.Key == ConsoleKey.D4 ||
                         gomb.Key == ConsoleKey.NumPad4)
                {
                    if (jatekosPont >= CelPont || gepPont >= CelPont)
                    {
                        BajnoksagInditasa();
                    }
                    else
                    {
                        Uzenet("A jelenlegi bajnokság még tart. Előbb fejezd be!");
                    }
                }
                else if (gomb.Key == ConsoleKey.D5 ||
                         gomb.Key == ConsoleKey.NumPad5)
                {
                    fut = false;
                }
            }

            return aktualisPenz;
        }

        private bool BajnoksagInditasa()
        {
            Console.Clear();
            Fejlec();
            Console.WriteLine("BAJNOKSÁGI NEVEZÉS");
            Console.WriteLine("--------------------------------");
            Console.WriteLine("A nevezés játékbeli, virtuális zsetonnal történik.");
            Console.WriteLine("Győzelem esetén a nevezési díj 5-szörösét kapod.");
            Console.WriteLine("Vereség esetén a nevezési díj nem jár vissza.");
            Console.WriteLine();
            Console.WriteLine("Virtuális egyenleged: " + aktualisPenz + " pénz");
            Console.WriteLine();
            Console.WriteLine("Válassz nevezési díjat:");
            Console.WriteLine("1 - 10000 pénz");
            Console.WriteLine("2 - 25000 pénz");
            Console.WriteLine("3 - 50000 pénz");
            Console.WriteLine("0 - Vissza");
            Console.Write("Választás: ");

            ConsoleKeyInfo gomb = Console.ReadKey(true);
            int dij;

            if (gomb.Key == ConsoleKey.D1 || gomb.Key == ConsoleKey.NumPad1)
                dij = 10000;
            else if (gomb.Key == ConsoleKey.D2 || gomb.Key == ConsoleKey.NumPad2)
                dij = 25000;
            else if (gomb.Key == ConsoleKey.D3 || gomb.Key == ConsoleKey.NumPad3)
                dij = 50000;
            else
            {
                Uzenet("A nevezést megszakítottad.");
                return false;
            }

            if (aktualisPenz < dij)
            {
                Uzenet("Nincs elegendő virtuális zsetonod a nevezéshez!");
                return false;
            }

            aktualisPenz -= dij;
            nevezesiDij = dij;
            bajnoksagNevezesAktiv = true;
            jutalomKifizetve = false;
            jatekosPont = 0;
            gepPont = 0;

            Uzenet("Sikeres nevezés! Levontunk " + dij + " pénzt. " +
                   "Bajnokság-győzelem esetén " + (dij * 5) + " pénzt kapsz.");
            return true;
        }

        private void Fejlec()
        {
            Console.WriteLine("================================");
            Console.WriteLine("          TEXAS HOLD'EM");
            Console.WriteLine("================================");
            Console.WriteLine();
        }

        private void PontTabla()
        {
            Console.WriteLine("BAJNOKSÁG ÁLLÁSA");
            Console.WriteLine("--------------------------------");
            Console.WriteLine("Te:  " + jatekosPont + " pont");
            Console.WriteLine("Gép: " + gepPont + " pont");
            Console.WriteLine("--------------------------------");
            Console.WriteLine("Egy megnyert parti értéke: " + partiPontErteke + " pont");
            Console.WriteLine("A győzelemhez " + CelPont + " pont szükséges.");
            if (bajnoksagNevezesAktiv)
            {
                Console.WriteLine("Nevezési díj: " + nevezesiDij + " virtuális pénz");
                Console.WriteLine("Lehetséges jutalom: " + (nevezesiDij * 5) + " virtuális pénz");
            }
        }

        private void NehezsegValasztasa()
        {
            Console.Clear();
            Fejlec();

            Console.WriteLine("Válassz nehézséget:");
            Console.WriteLine();
            Console.WriteLine("1 - Könnyű");
            Console.WriteLine("2 - Közepes");
            Console.WriteLine("3 - Nehéz");
            Console.WriteLine();
            Console.Write("Választás: ");

            ConsoleKeyInfo gomb = Console.ReadKey(true);

            if (gomb.Key == ConsoleKey.D1 ||
                gomb.Key == ConsoleKey.NumPad1)
            {
                nehezseg = 1;
            }
            else if (gomb.Key == ConsoleKey.D2 ||
                     gomb.Key == ConsoleKey.NumPad2)
            {
                nehezseg = 2;
            }
            else if (gomb.Key == ConsoleKey.D3 ||
                     gomb.Key == ConsoleKey.NumPad3)
            {
                nehezseg = 3;
            }
            else
            {
                Uzenet("Érvénytelen választás!");
                return;
            }

            string nev;

            if (nehezseg == 1)
                nev = "Könnyű";
            else if (nehezseg == 2)
                nev = "Közepes";
            else
                nev = "Nehéz";

            Uzenet("Kiválasztott nehézség: " + nev);
        }

        private void PartiPontErtekeValasztasa()
        {
            Console.Clear();
            Fejlec();

            Console.WriteLine("PARTI PONTÉRTÉKÉNEK BEÁLLÍTÁSA");
            Console.WriteLine("--------------------------------");
            Console.WriteLine("Válaszd ki, hány bajnoksági pont járjon");
            Console.WriteLine("egy megnyert partiért:");
            Console.WriteLine();
            Console.WriteLine("1 - 1 pont");
            Console.WriteLine("2 - 2 pont");
            Console.WriteLine("3 - 5 pont");
            Console.WriteLine();
            Console.Write("Választás: ");

            ConsoleKeyInfo gomb = Console.ReadKey(true);

            if (gomb.Key == ConsoleKey.D1 ||
                gomb.Key == ConsoleKey.NumPad1)
            {
                partiPontErteke = 1;
            }
            else if (gomb.Key == ConsoleKey.D2 ||
                     gomb.Key == ConsoleKey.NumPad2)
            {
                partiPontErteke = 2;
            }
            else if (gomb.Key == ConsoleKey.D3 ||
                     gomb.Key == ConsoleKey.NumPad3)
            {
                partiPontErteke = 5;
            }
            else
            {
                Uzenet("Érvénytelen választás!");
                return;
            }

            Uzenet("Az egy partiért járó pontérték: "
                + partiPontErteke + " pont.");
        }

        private void JatekKor()
        {
            List<Kartya> pakli = PakliLetrehozasa();
            Keveres(pakli);

            List<Kartya> jatekosLapok = new List<Kartya>();
            List<Kartya> gepLapok = new List<Kartya>();
            List<Kartya> kozosLapok = new List<Kartya>();

            // Saját lapok kiosztása
            for (int i = 0; i < 2; i++)
            {
                jatekosLapok.Add(pakli[0]);
                pakli.RemoveAt(0);

                gepLapok.Add(pakli[0]);
                pakli.RemoveAt(0);
            }

            // Öt közös lap kiosztása
            for (int i = 0; i < 5; i++)
            {
                kozosLapok.Add(pakli[0]);
                pakli.RemoveAt(0);
            }

            // OSZTÁS
            Console.Clear();
            Fejlec();

            Console.WriteLine("OSZTÁS");
            Console.WriteLine();
            Console.WriteLine("A TE LAPJAID:");
            LapokKiirasa(jatekosLapok);

            Console.WriteLine();
            Console.WriteLine("A gép lapjai: [REJTETT] [REJTETT]");

            Varakozas();

            // FLOP
            Console.Clear();
            Fejlec();

            Console.WriteLine("FLOP - Három közös lap");
            Console.WriteLine();

            LapokKiirasa(kozosLapok.Take(3).ToList());
            SajátLapokKiirasa(jatekosLapok);

            if (!JatekosDontes())
            {
                Console.WriteLine();
                Console.WriteLine("Feladtad a partit.");
                PontotAdunk(false);
                return;
            }

            if (!GepDontes(gepLapok, kozosLapok.Take(3).ToList()))
            {
                Console.WriteLine();
                Console.WriteLine("A gép befejezte a partit.");
                PontotAdunk(true);
                return;
            }

            // TURN
            Console.Clear();
            Fejlec();

            Console.WriteLine("TURN - Negyedik közös lap");
            Console.WriteLine();

            LapokKiirasa(kozosLapok.Take(4).ToList());
            SajátLapokKiirasa(jatekosLapok);

            if (!JatekosDontes())
            {
                Console.WriteLine();
                Console.WriteLine("Feladtad a partit.");
                PontotAdunk(false);
                return;
            }

            if (!GepDontes(gepLapok, kozosLapok.Take(4).ToList()))
            {
                Console.WriteLine();
                Console.WriteLine("A gép befejezte a partit.");
                PontotAdunk(true);
                return;
            }

            // RIVER
            Console.Clear();
            Fejlec();

            Console.WriteLine("RIVER - Ötödik közös lap");
            Console.WriteLine();

            LapokKiirasa(kozosLapok);
            SajátLapokKiirasa(jatekosLapok);

            Varakozas();

            // SHOWDOWN
            Eredmeny(jatekosLapok, gepLapok, kozosLapok);
        }

        private bool JatekosDontes()
        {
            Console.WriteLine();
            Console.WriteLine("--------------------------------");
            Console.WriteLine("Mit szeretnél tenni?");
            Console.WriteLine();
            Console.WriteLine("1 - Folytatás");
            Console.WriteLine("2 - Parti feladása");
            Console.WriteLine();
            Console.Write("Választás: ");

            while (true)
            {
                ConsoleKeyInfo gomb = Console.ReadKey(true);

                if (gomb.Key == ConsoleKey.D1 ||
                    gomb.Key == ConsoleKey.NumPad1)
                {
                    return true;
                }

                if (gomb.Key == ConsoleKey.D2 ||
                    gomb.Key == ConsoleKey.NumPad2)
                {
                    return false;
                }
            }
        }

        private bool GepDontes(
            List<Kartya> gepLapok,
            List<Kartya> kozosLapok)
        {
            List<Kartya> osszes = new List<Kartya>();

            osszes.AddRange(gepLapok);
            osszes.AddRange(kozosLapok);

            KezErtek kez = LegjobbKez(osszes);

            int minimum;

            if (nehezseg == 1)
                minimum = 0;
            else if (nehezseg == 2)
                minimum = 1;
            else
                minimum = 2;

            Console.WriteLine();
            Console.WriteLine("A gép döntést hoz...");

            if (kez.Kategoria < minimum)
            {
                Console.WriteLine("A gép nem folytatja a partit.");
                return false;
            }

            Console.WriteLine("A gép folytatja a partit.");
            return true;
        }

        private List<Kartya> PakliLetrehozasa()
        {
            List<Kartya> pakli = new List<Kartya>();

            string[] szinek =
            {
                "Pikk", "Kőr", "Káró", "Treff"
            };

            foreach (string szin in szinek)
            {
                for (int i = 2; i <= 14; i++)
                {
                    pakli.Add(new Kartya(i, szin));
                }
            }

            return pakli;
        }

        private void Keveres(List<Kartya> pakli)
        {
            for (int i = pakli.Count - 1; i > 0; i--)
            {
                int j = random.Next(i + 1);

                Kartya ideiglenes = pakli[i];
                pakli[i] = pakli[j];
                pakli[j] = ideiglenes;
            }
        }

        private void Eredmeny(
            List<Kartya> jatekosLapok,
            List<Kartya> gepLapok,
            List<Kartya> kozosLapok)
        {
            Console.Clear();
            Fejlec();

            Console.WriteLine("SHOWDOWN - VÉGEREDMÉNY");
            Console.WriteLine();

            Console.WriteLine("A TE LAPJAID:");
            LapokKiirasa(jatekosLapok);

            Console.WriteLine();
            Console.WriteLine("A GÉP LAPJAI:");
            LapokKiirasa(gepLapok);

            Console.WriteLine();
            Console.WriteLine("KÖZÖS LAPOK:");
            LapokKiirasa(kozosLapok);

            List<Kartya> jatekosOsszes = new List<Kartya>();
            jatekosOsszes.AddRange(jatekosLapok);
            jatekosOsszes.AddRange(kozosLapok);

            List<Kartya> gepOsszes = new List<Kartya>();
            gepOsszes.AddRange(gepLapok);
            gepOsszes.AddRange(kozosLapok);

            KezErtek jatekosEro = LegjobbKez(jatekosOsszes);
            KezErtek gepEro = LegjobbKez(gepOsszes);

            Console.WriteLine();
            Console.WriteLine("A TE LEGJOBB KOMBINÁCIÓD:");
            Console.WriteLine(jatekosEro.Nev);
            LapokKiirasa(jatekosEro.Lapok);

            Console.WriteLine();
            Console.WriteLine("A GÉP LEGJOBB KOMBINÁCIÓJA:");
            Console.WriteLine(gepEro.Nev);
            LapokKiirasa(gepEro.Lapok);

            int eredmeny = Osszehasonlitas(jatekosEro, gepEro);

            Console.WriteLine();
            Console.WriteLine("--------------------------------");

            if (eredmeny > 0)
            {
                Console.WriteLine("MEGNYERTED A PARTIT!");
                PontotAdunk(true);
            }
            else if (eredmeny < 0)
            {
                Console.WriteLine("A GÉP NYERTE A PARTIT!");
                PontotAdunk(false);
            }
            else
            {
                Console.WriteLine("DÖNTETLEN!");

                jatekosPont += partiPontErteke;
                gepPont += partiPontErteke;

                PontTabla();
                BajnoksagEllenorzese();
                Varakozas();
            }
        }

        private void PontotAdunk(bool jatekosNyert)
        {
            if (jatekosNyert)
            {
                jatekosPont += partiPontErteke;
                Console.WriteLine();
                Console.WriteLine("Kaptál " + partiPontErteke + " bajnoksági pontot!");
            }
            else
            {
                gepPont += partiPontErteke;
                Console.WriteLine();
                Console.WriteLine("A gép kapott " + partiPontErteke + " bajnoksági pontot.");
            }

            PontTabla();
            BajnoksagEllenorzese();
            Varakozas();
        }

        private void BajnoksagEllenorzese()
        {
            if (jatekosPont >= CelPont)
            {
                Console.WriteLine();
                Console.WriteLine("================================");
                Console.WriteLine("MEGNYERTED A BAJNOKSÁGOT!");
                Console.WriteLine("================================");
                if (bajnoksagNevezesAktiv && !jutalomKifizetve)
                {
                    int jutalom = nevezesiDij * 5;
                    aktualisPenz += jutalom;
                    jutalomKifizetve = true;
                    bajnoksagNevezesAktiv = false;
                    Console.WriteLine("Bajnoksági jutalmad: " + jutalom + " virtuális zseton!");
                    Console.WriteLine("Új virtuális egyenleged: " + aktualisPenz + " zseton");
                }
            }
            else if (gepPont >= CelPont)
            {
                Console.WriteLine();
                Console.WriteLine("================================");
                Console.WriteLine("A GÉP NYERTE A BAJNOKSÁGOT!");
                Console.WriteLine("================================");
                if (bajnoksagNevezesAktiv)
                {
                    bajnoksagNevezesAktiv = false;
                    jutalomKifizetve = true;
                    Console.WriteLine("Nem nyertél jutalmat. A nevezési díj nem jár vissza.");
                    Console.WriteLine("Virtuális egyenleged: " + aktualisPenz + " zseton");
                }
            }
        }

        private void Uzenet(string szoveg)
        {
            Console.Clear();
            Fejlec();

            Console.WriteLine(szoveg);
            Console.WriteLine();
            Console.WriteLine("Nyomj meg egy gombot...");
            Console.ReadKey(true);
        }

        private void Varakozas()
        {
            Console.WriteLine();
            Console.WriteLine("Nyomj meg egy gombot a folytatáshoz...");
            Console.ReadKey(true);
        }

        private void LapokKiirasa(List<Kartya> lapok)
        {
            foreach (Kartya lap in lapok)
            {
                Console.Write("[" + lap + "] ");
            }

            Console.WriteLine();
        }

        private void SajátLapokKiirasa(List<Kartya> lapok)
        {
            Console.WriteLine();
            Console.WriteLine("A TE LAPJAID:");
            LapokKiirasa(lapok);
        }

        private KezErtek LegjobbKez(List<Kartya> lapok)
        {
            KezErtek legjobb = null;

            for (int a = 0; a < lapok.Count - 4; a++)
            {
                for (int b = a + 1; b < lapok.Count - 3; b++)
                {
                    for (int c = b + 1; c < lapok.Count - 2; c++)
                    {
                        for (int d = c + 1; d < lapok.Count - 1; d++)
                        {
                            for (int e = d + 1; e < lapok.Count; e++)
                            {
                                List<Kartya> otLap = new List<Kartya>
                                {
                                    lapok[a],
                                    lapok[b],
                                    lapok[c],
                                    lapok[d],
                                    lapok[e]
                                };

                                KezErtek aktualis = Ertekeles(otLap);

                                if (legjobb == null ||
                                    Osszehasonlitas(aktualis, legjobb) > 0)
                                {
                                    legjobb = aktualis;
                                }
                            }
                        }
                    }
                }
            }

            return legjobb;
        }

        private KezErtek Ertekeles(List<Kartya> lapok)
        {
            List<int> ertekek = lapok
                .Select(x => x.Ertek)
                .OrderByDescending(x => x)
                .ToList();

            bool flos = lapok.All(x => x.Szin == lapok[0].Szin);
            int sor = SorMagassag(ertekek);

            var csoportok = lapok
                .GroupBy(x => x.Ertek)
                .Select(x => new
                {
                    Ertek = x.Key,
                    Darab = x.Count()
                })
                .OrderByDescending(x => x.Darab)
                .ThenByDescending(x => x.Ertek)
                .ToList();

            KezErtek eredmeny = new KezErtek();
            eredmeny.Lapok = lapok;

            if (flos && sor > 0)
            {
                eredmeny.Kategoria = 8;
                eredmeny.Erossegek.Add(sor);
                eredmeny.Nev = sor == 14 ? "Royal flöss" : "Színsor";
            }
            else if (csoportok[0].Darab == 4)
            {
                eredmeny.Kategoria = 7;
                eredmeny.Erossegek.Add(csoportok[0].Ertek);
                eredmeny.Erossegek.Add(csoportok[1].Ertek);
                eredmeny.Nev = "Póker";
            }
            else if (csoportok[0].Darab == 3 &&
                     csoportok[1].Darab == 2)
            {
                eredmeny.Kategoria = 6;
                eredmeny.Erossegek.Add(csoportok[0].Ertek);
                eredmeny.Erossegek.Add(csoportok[1].Ertek);
                eredmeny.Nev = "Full house";
            }
            else if (flos)
            {
                eredmeny.Kategoria = 5;
                eredmeny.Erossegek.AddRange(ertekek);
                eredmeny.Nev = "Flöss";
            }
            else if (sor > 0)
            {
                eredmeny.Kategoria = 4;
                eredmeny.Erossegek.Add(sor);
                eredmeny.Nev = "Sor";
            }
            else if (csoportok[0].Darab == 3)
            {
                eredmeny.Kategoria = 3;
                eredmeny.Erossegek.Add(csoportok[0].Ertek);

                foreach (var csoport in csoportok.Skip(1))
                    eredmeny.Erossegek.Add(csoport.Ertek);

                eredmeny.Nev = "Drill";
            }
            else if (csoportok[0].Darab == 2 &&
                     csoportok[1].Darab == 2)
            {
                eredmeny.Kategoria = 2;
                eredmeny.Erossegek.Add(csoportok[0].Ertek);
                eredmeny.Erossegek.Add(csoportok[1].Ertek);
                eredmeny.Erossegek.Add(csoportok[2].Ertek);
                eredmeny.Nev = "Két pár";
            }
            else if (csoportok[0].Darab == 2)
            {
                eredmeny.Kategoria = 1;
                eredmeny.Erossegek.Add(csoportok[0].Ertek);

                foreach (var csoport in csoportok.Skip(1))
                    eredmeny.Erossegek.Add(csoport.Ertek);

                eredmeny.Nev = "Egy pár";
            }
            else
            {
                eredmeny.Kategoria = 0;
                eredmeny.Erossegek.AddRange(ertekek);
                eredmeny.Nev = "Magas lap";
            }

            return eredmeny;
        }

        private int SorMagassag(List<int> ertekek)
        {
            List<int> egyedi = ertekek
                .Distinct()
                .OrderBy(x => x)
                .ToList();

            if (egyedi.Count != 5)
                return 0;

            if (egyedi[4] - egyedi[0] == 4)
                return egyedi[4];

            if (egyedi.SequenceEqual(
                new List<int> { 2, 3, 4, 5, 14 }))
            {
                return 5;
            }

            return 0;
        }

        private int Osszehasonlitas(KezErtek egyik, KezErtek masik)
        {
            if (egyik.Kategoria > masik.Kategoria)
                return 1;

            if (egyik.Kategoria < masik.Kategoria)
                return -1;

            for (int i = 0; i < egyik.Erossegek.Count; i++)
            {
                if (egyik.Erossegek[i] > masik.Erossegek[i])
                    return 1;

                if (egyik.Erossegek[i] < masik.Erossegek[i])
                    return -1;
            }

            return 0;
        }
    }
}
