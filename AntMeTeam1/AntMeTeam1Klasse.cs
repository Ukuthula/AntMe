using AntMe.Deutsch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AntMe.Spieler;

namespace AntMe.Player.AntMeTeam1
{
    /// <summary>
    /// Diese Datei enthält die Beschreibung für deine Ameise. Die einzelnen Code-Blöcke 
    /// (Beginnend mit "public override void") fassen zusammen, wie deine Ameise in den 
    /// entsprechenden Situationen reagieren soll. Welche Befehle du hier verwenden kannst, 
    /// findest du auf der Befehlsübersicht im Wiki (http://wiki.antme.net/de/API1:Befehlsliste).
    /// 
    /// Wenn du etwas Unterstützung bei der Erstellung einer Ameise brauchst, findest du
    /// in den AntMe!-Lektionen ein paar Schritt-für-Schritt Anleitungen.
    /// (http://wiki.antme.net/de/Lektionen)
    /// </summary>
    [Spieler(
        Volkname = "AntMeTeam1",   // Hier kannst du den Namen des Volkes festlegen
        Vorname = "Julian",       // An dieser Stelle kannst du dich als Schöpfer der Ameise eintragen
        Nachname = "Hesse"       // An dieser Stelle kannst du dich als Schöpfer der Ameise eintragen
    )]

    /// Kasten stellen "Berufsgruppen" innerhalb deines Ameisenvolkes dar. Du kannst hier mit
    /// den Fähigkeiten einzelner Ameisen arbeiten. Wie genau das funktioniert kannst du der 
    /// Lektion zur Spezialisierung von Ameisen entnehmen (http://wiki.antme.net/de/Lektion7).
    [Kaste(
        Name = "Standard",                  // Name der Berufsgruppe
        AngriffModifikator = -1,             // Angriffsstärke einer Ameise
        DrehgeschwindigkeitModifikator = -1, // Drehgeschwindigkeit einer Ameise
        EnergieModifikator = -1,             // Lebensenergie einer Ameise
        GeschwindigkeitModifikator = 2,     // Laufgeschwindigkeit einer Ameise
        LastModifikator = 2,                // Tragkraft einer Ameise
        ReichweiteModifikator = -1,          // Ausdauer einer Ameise
        SichtweiteModifikator = 0           // Sichtweite einer Ameise
    )]

    [Kaste(
        Name = "Sammler",                  // Name der Berufsgruppe
        AngriffModifikator = -1,             // Angriffsstärke einer Ameise
        DrehgeschwindigkeitModifikator = -1, // Drehgeschwindigkeit einer Ameise
        EnergieModifikator = -1,             // Lebensenergie einer Ameise
        GeschwindigkeitModifikator = 2,     // Laufgeschwindigkeit einer Ameise
        LastModifikator = 2,                // Tragkraft einer Ameise
        ReichweiteModifikator = 0,          // Ausdauer einer Ameise
        SichtweiteModifikator = -1           // Sichtweite einer Ameise
    )]

    [Kaste(
        Name = "Sucher",                  // Name der Berufsgruppe
        AngriffModifikator = -1,             // Angriffsstärke einer Ameise
        DrehgeschwindigkeitModifikator = -1, // Drehgeschwindigkeit einer Ameise
        EnergieModifikator = -1,             // Lebensenergie einer Ameise
        GeschwindigkeitModifikator = 2,     // Laufgeschwindigkeit einer Ameise
        LastModifikator = 0,                // Tragkraft einer Ameise
        ReichweiteModifikator = -1,          // Ausdauer einer Ameise
        SichtweiteModifikator = 2           // Sichtweite einer Ameise
    )]

    [Kaste(
        Name = "Spotter",                  // Name der Berufsgruppe
        AngriffModifikator = 0,             // Angriffsstärke einer Ameise
        DrehgeschwindigkeitModifikator = -1, // Drehgeschwindigkeit einer Ameise
        EnergieModifikator = 1,             // Lebensenergie einer Ameise
        GeschwindigkeitModifikator = 0,     // Laufgeschwindigkeit einer Ameise
        LastModifikator = -1,                // Tragkraft einer Ameise
        ReichweiteModifikator = -1,          // Ausdauer einer Ameise
        SichtweiteModifikator = 2           // Sichtweite einer Ameise
    )]

    [Kaste(
        Name = "Fighter",                  // Name der Berufsgruppe
        AngriffModifikator = 2,             // Angriffsstärke einer Ameise
        DrehgeschwindigkeitModifikator = -1, // Drehgeschwindigkeit einer Ameise
        EnergieModifikator = 2,             // Lebensenergie einer Ameise
        GeschwindigkeitModifikator = -1,     // Laufgeschwindigkeit einer Ameise
        LastModifikator = -1,                // Tragkraft einer Ameise
        ReichweiteModifikator = -1,          // Ausdauer einer Ameise
        SichtweiteModifikator = 0           // Sichtweite einer Ameise
    )]


    public class AntMeTeam1Klasse : Basisameise
    {
        #region Self-Made

        private Spielobjekt ziel = null; //Speichert den Zuckerberg, damit die Ameise später den Zucker wiederfindet
        private Bau bau = null;
        private Spielobjekt ankunftsort = null;
        private Ticket ticket = null;
        private String ticketTyp = null;
        private Random rnd = new Random();
        private int angriffscounter = 0;
        private int resetcounter = 0;
   
        private const String fighter = "Fighter";
        private const String obsts = "obst";
        private const String zuckers = "zucker";
        private const String wanzes = "wanze";
        private const String fameises = "ameise";
        


        private int ZufallsZahl(int wert1, int wert2)
        {
            return rnd.Next(wert1, wert2);
        }

        private void GeheZuZielOptimized(Spielobjekt spielobjekt)
        {
            int distance = Koordinate.BestimmeEntfernung(this, spielobjekt);
            //int angle = Koordinate.BestimmeRichtung(this, spielobjekt);
            DreheZuZiel(spielobjekt);
            GeheGeradeaus(distance);
        }

        private void GeheZuBauOptimized(Spielobjekt spielobjekt)
        {
            if (bau != null)
            {
                GeheZuZielOptimized(spielobjekt);
                //Denke("Nach Hause");
                ankunftsort = spielobjekt;
            }
            else
            {
                GeheZuBau();
            }
        }

        #endregion Self-Made

        #region Kasten

        /// <summary>
        /// Jedes mal, wenn eine neue Ameise geboren wird, muss ihre Berufsgruppe
        /// bestimmt werden. Das kannst du mit Hilfe dieses Rückgabewertes dieser 
        /// Methode steuern.
        /// Weitere Infos unter http://wiki.antme.net/de/API1:BestimmeKaste
        /// </summary>
        /// <param name="anzahl">Anzahl Ameisen pro Kaste</param>
        /// <returns>Name der Kaste zu der die geborene Ameise gehören soll</returns>
        public override string BestimmeKaste(Dictionary<string, int> anzahl)
        {
            // Gibt den Namen der betroffenen Kaste zurück.
            ///*
            resetcounter++;

            if(resetcounter > 20)
            {
                TicketManager.Instance.ClearEnemyTickets();
            }

            if (anzahl["Sammler"] <= 30)
            {
                return "Sammler";
            }
            else
            {
                if (anzahl[fighter] < 50)
                {
                    return fighter;
                }
                else
                {
                    return "Sammler";
                }
            }
        }

        #endregion

        #region Fortbewegung

        /// <summary>
        /// Wenn die Ameise keinerlei Aufträge hat, wartet sie auf neue Aufgaben. Um dir das 
        /// mitzuteilen, wird diese Methode hier aufgerufen.
        /// Weitere Infos unter http://wiki.antme.net/de/API1:Wartet
        /// </summary>
        public override void Wartet()
        {
            //Initialisiere Bau
            bau = TicketManager.Instance.GetBau();

            if (bau == null)
            {
                GeheZuBau();
                bau = Ziel as Bau;
                TicketManager.Instance.ReportBau(bau);
                BleibStehen();
                switch (this.Kaste)
                {
                    case fighter:
                        break;

                    default:
                        TicketManager.Instance.RegisterAmeise(this);
                        break;
                }
            }



            #region Sammler Wartet

            //Falls es noch ein Ziel gibt, dann gehe zum Ziel, sont hole dir ein Ticket
            if (ziel != null && this.Kaste != fighter)
            {
                GeheZuZielOptimized(ziel);
            }
            else
            {
                if (ticket == null && this.Kaste != fighter)
                {
                    ticket = TicketManager.Instance.OGetTicket();

                    if (ticket != null)
                    {
                        ziel = ticket.Obst;
                        ticketTyp = obsts;
                        GeheZuZielOptimized(ziel);
                        Denke("Zucker");
                    }
                }
                if (ticket == null && this.Kaste != fighter)
                {
                    ticket = TicketManager.Instance.ZGetTicket();

                    if (ticket != null)
                    {
                        ziel = ticket.Zucker;
                        ticketTyp = zuckers;
                        GeheZuZielOptimized(ziel);
                        Denke("Obst");
                    }
                }
                if (ticket == null)
                {
                    DreheUmWinkel(ZufallsZahl(0, 360));
                    GeheGeradeaus();
                    ticketTyp = null;
                }
            }

            #endregion


            #region Fighter Warten

            if (ticket == null && this.Kaste == fighter)
            {
                if(ticket == null)
                {
                    ticket = TicketManager.Instance.FGetTicket(this);
                    
                    if(ticket != null)
                    {
                        ziel = ticket.Ameise;
                        ticketTyp = fameises;
                        GeheZuZielOptimized(ziel);
                        Denke("FAmeise!!");
                    }
                }
                if(ticket == null)
                {
                    ticket = TicketManager.Instance.WGetTicket(this);

                    if(ticket != null)
                    {
                        ziel = ticket.Wanze;
                        ticketTyp = wanzes;
                        GeheZuZielOptimized(ziel);
                        Denke("FAmeise!!");
                    }
                }
                if (ticket != null)
                {
                    GeheZuZielOptimized(ziel);
                }
                else
                {
                    DreheUmWinkel(ZufallsZahl(0, 360));
                    GeheGeradeaus();
                }
            }
            #endregion
        }

        /// <summary>
        /// Erreicht eine Ameise ein drittel ihrer Laufreichweite, wird diese Methode aufgerufen.
        /// Weitere Infos unter http://wiki.antme.net/de/API1:WirdM%C3%BCde
        /// </summary>
        public override void WirdMüde()
        {
        }

        /// <summary> && zuckers == null
        /// Wenn eine Ameise stirbt, wird diese Methode aufgerufen. Man erfährt dadurch, wie 
        /// die Ameise gestorben ist. Die Ameise kann zu diesem Zeitpunkt aber keinerlei Aktion 
        /// mehr ausführen.
        /// Weitere Infos unter http://wiki.antme.net/de/API1:IstGestorben
        /// </summary>
        /// <param name="todesart">Art des Todes</param>
        public override void IstGestorben(Todesart todesart)
        {
            TicketManager.Instance.UnregisterAmeise(this, ticket, ticketTyp);
        }

        /// <summary>
        /// Diese Methode wird in jeder Simulationsrunde aufgerufen - ungeachtet von zusätzlichen 
        /// Bedingungen. Dies eignet sich für Aktionen, die unter Bedingungen ausgeführt werden 
        /// sollen, die von den anderen Methoden nicht behandelt werden.
        /// Weitere Infos unter http://wiki.antme.net/de/API1:Tick
        /// </summary>
        public override void Tick()
        {
            //Schickt erschöpfte Ameisen zurück
            /*if (Reichweite - ZurückgelegteStrecke -20 < EntfernungZuBau)
            {
                GeheZuBau();
            }*/

            #region Sammler Tick



            if (this.Kaste == "Sammler" || this.Kaste == "Standard")
            { 
                //Wenn die Ameise Last hat, dann soll sie zum Bau gehen
                if (AktuelleLast != 0)
                {
                    GeheZuBauOptimized(bau);
                }

                //Schaue wie gro
                if (ankunftsort != null)
                {
                    int distance = Koordinate.BestimmeEntfernung(this, ankunftsort);
                    if (distance < Sichtweite / 2)
                    {
                        GeheZuBau();
                        ankunftsort = null;
                    }
                }

                //Findet heraus, ob der Zuckerberg noch existiert, wenn du dein Zucker schon abgeliefert hast
                if (ziel != null && AktuelleLast == 0 && this.Kaste != fighter)
                {
                    switch (ticketTyp)
                    {
                        case obsts:
                            if (!BrauchtNochTräger(ticket.Obst))
                            {
                                ziel = null;
                                ticket = null;
                                BleibStehen();
                            }
                            break;

                        case zuckers:
                            if (ticket.Zucker.Menge <= 0)
                            {
                                ziel = null;
                                ticket = null;
                                BleibStehen();
                            }
                            break;
                    }
                }
            }

            #endregion


            #region Fighter Tick

            if (this.Kaste == fighter)
            {
                if (this.Kaste == fighter && ticket != null)
                {
                    if (ticketTyp == fameises && ticket.Ameise.AktuelleEnergie == 0)
                    {
                        ticket = null;
                    }
                    if (ticketTyp == wanzes && ticket.Wanze.AktuelleEnergie == 0)
                    {
                        ticket = null;
                    }

                    if (ticketTyp == fameises && ticket != null)
                    {
                        if (Koordinate.BestimmeEntfernung(bau, ticket.Ameise) > 300)
                        {
                            ticket = null;
                            ticketTyp = null;
                        }
                    }
                    if (ticketTyp == wanzes && ticket != null)
                    {
                        if (Koordinate.BestimmeEntfernung(bau, ticket.Wanze) > 300)
                        {
                            ticket = null;
                            ticketTyp = null;
                        }
                    }
                }

                //Wenn der Fighter zu weit weg ist, dann soll es zurückkommen
                if (Koordinate.BestimmeEntfernung(this, bau) > 300)
                {
                    GeheZuBauOptimized(bau);
                    ticket = null;
                    ticketTyp = null;
                }


                //Findet heraus, ob es tickets für feindliche Ameisen gibt und dann den fightern diese zuspielt
                if (this.Kaste == fighter && this.AktuelleEnergie >= 50)
                {
                    if (ticketTyp == wanzes)
                    {
                        Ticket ticketAlternativ = null;
                        ticketAlternativ = TicketManager.Instance.FGetTicket(this);

                        if (ticketAlternativ != null)
                        {
                            TicketManager.Instance.UnregisterAmeise(this, ticket, ticketTyp);
                            ticket = ticketAlternativ;
                            ticketTyp = fameises;
                            ziel = ticket.Ameise;
                            GeheZuZielOptimized(ziel);
                        }
                        else
                        {
                            TicketManager.Instance.UnregisterAmeise(this, ticketAlternativ, ticketTyp);
                        }
                    }
                    //if (ticketTyp == fameises)
                    //{
                    //    Ticket ticketAlternativ = null;
                    //    ticketAlternativ = TicketManager.Instance.WGetTicket(this);

                    //    if(ticketAlternativ != null)
                    //    {
                    //        TicketManager.Instance.UnregisterAmeise(this, ticket, ticketTyp);
                    //        ticket = ticketAlternativ;
                    //        ticketTyp = wanzes;
                    //        ziel = ticket.Wanze;
                    //        GeheZuZielOptimized(ziel);
                    //    }
                    //    else
                    //    {
                    //        TicketManager.Instance.UnregisterAmeise(this, ticketAlternativ, ticketTyp);
                    //    }
                    //}

                }
            }

            #endregion

            //if(this.Kaste == fighter && ticket != null)
            //{
            //    GeheZuZielOptimized(ziel);

            //}

            //Ermöglicht anderen Ameisen zu wissen, wo Zucker ist
            /*
            if (AktuelleLast > 0)
            {
                if (GetragenesObst == null)
                {
                    SprüheMarkierung(Richtung + 180, 100);
                }
            }
            */
        }

        #endregion

        #region Nahrung

        /// <summary>
        /// Sobald eine Ameise innerhalb ihres Sichtradius einen Apfel erspäht wird 
        /// diese Methode aufgerufen. Als Parameter kommt das betroffene Stück Obst.
        /// Weitere Infos unter "http://wiki.antme.net/de/API1:Sieht(Obst)"
        /// </summary>
        /// <param name="obst">Das gesichtete Stück Obst</param>
        public override void Sieht(Obst obst)
        {
            //Übergebe Obst an den Ticketmanager
            TicketManager.Instance.ReportObst(obst);

            if (AktuelleLast == 0 && BrauchtNochTräger(obst) && ticketTyp == obsts)
            {
                GeheZuZiel(obst);
            }
        }

        /// <summary>
        /// Sobald eine Ameise innerhalb ihres Sichtradius einen Zuckerhügel erspäht wird 
        /// diese Methode aufgerufen. Als Parameter kommt der betroffene Zuckerghügel.
        /// Weitere Infos unter "http://wiki.antme.net/de/API1:Sieht(Zucker)"
        /// </summary>
        /// <param name="zucker">Der gesichtete Zuckerhügel</param>
        public override void Sieht(Zucker zucker)
        {
            //zuckers = zucker;
            TicketManager.Instance.ReportSugar(zucker);

            if (AktuelleLast == 0 && ticketTyp == zuckers)
            {
                GeheZuZiel(zucker);
            }
        }

        /// <summary>
        /// Hat die Ameise ein Stück Obst als Ziel festgelegt, wird diese Methode aufgerufen, 
        /// sobald die Ameise ihr Ziel erreicht hat. Ab jetzt ist die Ameise nahe genug um mit 
        /// dem Ziel zu interagieren.
        /// Weitere Infos unter "http://wiki.antme.net/de/API1:ZielErreicht(Obst)"
        /// </summary>
        /// <param name="obst">Das erreichte Stück Obst</param>
        public override void ZielErreicht(Obst obst)
        {
            //Nur wenn noch Träger gebraucht werden
            if (BrauchtNochTräger(obst) == true)
            {
                //SprüheMarkierung(1000, 300);
                Nimm(obst);
                GeheZuBau();
            }
        }

        /// <summary>
        /// Hat die Ameise eine Zuckerhügel als Ziel festgelegt, wird diese Methode aufgerufen, 
        /// sobald die Ameise ihr Ziel erreicht hat. Ab jetzt ist die Ameise nahe genug um mit 
        /// dem Ziel zu interagieren.
        /// Weitere Infos unter "http://wiki.antme.net/de/API1:ZielErreicht(Zucker)"
        /// </summary>
        /// <param name="zucker">Der erreichte Zuckerhügel</param>
        public override void ZielErreicht(Zucker zucker)
        {
            Nimm(zucker);
            GeheZuBau();
        }

        #endregion

        #region Kommunikation

        /// <summary>
        /// Markierungen, die von anderen Ameisen platziert werden, können von befreundeten Ameisen 
        /// gewittert werden. Diese Methode wird aufgerufen, wenn eine Ameise zum ersten Mal eine 
        /// befreundete Markierung riecht.
        /// Weitere Infos unter "http://wiki.antme.net/de/API1:RiechtFreund(Markierung)"
        /// </summary>
        /// <param name="markierung">Die gerochene Markierung</param>
        public override void RiechtFreund(Markierung markierung)
        {
            //Gehe in Richtung Zuckerberg
            if (markierung.Information < 1000)
            {
                if (Ziel == null)
                {
                    //Drehe Richtung Zuckerberg
                    DreheInRichtung(markierung.Information);
                    GeheGeradeaus();
                }
            }

            //Gehe zu Apfel um zu helfen
            if (markierung.Information == 1000 && AktuelleLast == 0)
            {
                GeheZuZiel(markierung);
            }
        }

        /// <summary>
        /// So wie Ameisen unterschiedliche Nahrungsmittel erspähen können, entdecken Sie auch 
        /// andere Spielelemente. Entdeckt die Ameise eine Ameise aus dem eigenen Volk, so 
        /// wird diese Methode aufgerufen.
        /// Weitere Infos unter "http://wiki.antme.net/de/API1:SiehtFreund(Ameise)"
        /// </summary>
        /// <param name="ameise">Erspähte befreundete Ameise</param>
        public override void SiehtFreund(Ameise ameise)
        {
        }

        /// <summary>
        /// So wie Ameisen unterschiedliche Nahrungsmittel erspähen können, entdecken Sie auch 
        /// andere Spielelemente. Entdeckt die Ameise eine Ameise aus einem befreundeten Volk 
        /// (Völker im selben Team), so wird diese Methode aufgerufen.
        /// Weitere Infos unter "http://wiki.antme.net/de/API1:SiehtVerb%C3%BCndeten(Ameise)"
        /// </summary>
        /// <param name="ameise">Erspähte verbündete Ameise</param>
        public override void SiehtVerbündeten(Ameise ameise)
        {
        }

        #endregion

        #region Kampf

        /// <summary>
        /// So wie Ameisen unterschiedliche Nahrungsmittel erspähen können, entdecken Sie auch 
        /// andere Spielelemente. Entdeckt die Ameise eine Ameise aus einem feindlichen Volk, 
        /// so wird diese Methode aufgerufen.
        /// Weitere Infos unter "http://wiki.antme.net/de/API1:SiehtFeind(Ameise)"
        /// </summary>
        /// <param name="ameise">Erspähte feindliche Ameise</param>
        public override void SiehtFeind(Ameise ameise)
        {
            //wenn du ein Ticket hast die Ameise anzugreifen und Kämpfer bist greife an
            if (ticket != null && this.Kaste == fighter && ticketTyp == fameises)
            {
                if(ticket.Ameise == ameise)
                {
                    Denke("Attacke");
                    GreifeAn(ameise);
                }
            }

            if (this.Kaste == "Sammler")
            {
                TicketManager.Instance.ReportFAmeise(ameise);
            }
        }

        /// <summary>
        /// So wie Ameisen unterschiedliche Nahrungsmittel erspähen können, entdecken Sie auch 
        /// andere Spielelemente. Entdeckt die Ameise eine Wanze, so wird diese Methode aufgerufen.
        /// Weitere Infos unter "http://wiki.antme.net/de/API1:SiehtFeind(Wanze)"
        /// </summary>
        /// <param name="wanze">Erspähte Wanze</param>
        public override void SiehtFeind(Wanze wanze)
        {

            if (AktuelleLast == 0 && ticket == null)
            {
                //GeheWegVon(wanze);
                Denke("Hilfe");

            }

            //wenn du ein Ticket hast die Wanze anzugreifen und Kämpfer bist greife an
            if (ticket != null && this.Kaste == fighter && ticketTyp == wanzes)
            {
                if(ticket.Wanze == wanze)
                {
                    Denke("Attacke");
                    GreifeAn(wanze);
                }
            }

            if (this.Kaste == "Sammler")
            {
                TicketManager.Instance.ReportWanze(wanze);
            }

        }

        /// <summary>
        /// Es kann vorkommen, dass feindliche Lebewesen eine Ameise aktiv angreifen. Sollte 
        /// eine feindliche Ameise angreifen, wird diese Methode hier aufgerufen und die 
        /// Ameise kann entscheiden, wie sie darauf reagieren möchte.
        /// Weitere Infos unter "http://wiki.antme.net/de/API1:WirdAngegriffen(Ameise)"
        /// </summary>
        /// <param name="ameise">Angreifende Ameise</param>
        public override void WirdAngegriffen(Ameise ameise)
        {
            TicketManager.Instance.ReportFAmeise(ameise);

            if (this.Kaste == "Fighter")
            {
                this.GreifeAn(ameise);
            }
            else
            {
                angriffscounter++;
            }
        }

        /// <summary>
        /// Es kann vorkommen, dass feindliche Lebewesen eine Ameise aktiv angreifen. Sollte 
        /// eine Wanze angreifen, wird diese Methode hier aufgerufen und die Ameise kann 
        /// entscheiden, wie sie darauf reagieren möchte.
        /// Weitere Infos unter "http://wiki.antme.net/de/API1:WirdAngegriffen(Wanze)"
        /// </summary>
        /// <param name="wanze">Angreifende Wanze</param>
        public override void WirdAngegriffen(Wanze wanze)
        {
            TicketManager.Instance.ReportWanze(wanze);

            if (this.Kaste == "Fighter")
            {
                this.GreifeAn(wanze);
            }
        }

        #endregion
    }
}