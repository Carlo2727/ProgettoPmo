using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FoodDispenser
{
    static class Distributore
    {
        /*Dichiaro una lista di tipo Prodotto chiamata prodotti
          e una variabile prezzoProdotto*/
        public static List<Prodotto> prodotti;
        //Variabile prezzo prodotto per passare il prezzo trovato dalla ricerca del prodotto all'acquisto del prodotto
        public static double prezzoProdotto;

        //Metodo statico accendiDistributore
        public static void AccendiDistributore()
        {
            //Acquisizione percorso file Inventario.txt da console
            string filePath;
            //Colore del testo console: rosso
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Benvenuto! prima di iniziare, digita qui il percorso per acquisire l'inventario:");
            //Inserimento percorso file e inizializzazione classe
            filePath = Console.ReadLine();
            SaldoCliente saldo = new SaldoCliente()
            {
                CreditoInserito = 0
            };
            try
            {
                Distributore.LogAccensioneSpegnimentoDistributore("Accensione"," riuscita!");
                var lines = File.ReadAllLines(filePath);
                //Colore del testo console: verde
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("I prodotti disponibili oggi sono:\nNome      Prezzo  Codice  Disponibilità");
                //Inizializzazione lista di tipo Prodotto da compilare
                Distributore.prodotti = new List<Prodotto>();
                //Acquisizione da file degli attributi di ogni prodotto
                foreach (var item in lines)
                {
                    //Compilazione lista di ogni prodotto
                    var elements = item.Split("-");                                     
                    Prodotto prodotto = new Prodotto()
                    {
                        Nome = elements[0],
                        Prezzo = double.Parse(elements[1]),
                        CodiceRiconoscimento = int.Parse(elements[2]),
                        Disponibilità = int.Parse(elements[3])
                    };
                    prodotti.Add(prodotto);
                    Console.WriteLine(prodotto.Nome + "     " + prodotto.Prezzo + "     " + prodotto.CodiceRiconoscimento + "       " + prodotto.Disponibilità);
                }
                //Richiamo metodo ConsoleComandi
                ConsoleComandi(saldo);

            }
            //catch per l'exception
            catch (FileNotFoundException)
            {
                Distributore.LogAccensioneSpegnimentoDistributore("Accensione"," fallita");
                Console.WriteLine("File di input non trovato, impossibile avviare programma");
                Distributore.LogErroreProgramma("Il programma è stato chiuso in quanto: è stato inserito un percorso file non valido.");
            }
        }

        //Metodo statico Console Comandi
        public static void ConsoleComandi(SaldoCliente saldo)
        {

            int scelta;
            try
            {
                do
                {
                    //Colore del testo console: blu
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("\nScegli l'operazione da fare: \n1.Inserisci denaro\n2.Cerca il prodotto e acquista\n3.Visualizza il saldo attuale\n4.Ritira il resto e termina");
                    scelta = int.Parse(Console.ReadLine());

                    switch (scelta)
                    {
                        case 1:
                            //Caso uno per richiamare il metodo InserisciDenaro
                            InserisciDenaro(saldo);
                            break;
                        case 2:
                            //Caso due per richiamare il metodo Ricerca prodotto
                            RicercaProdotto(saldo);
                            break;
                        case 3:
                            //Caso tre per richiamare il metodo Ricerca prodotto
                            //Colore del testo console: grigio scuro
                            Console.ForegroundColor = ConsoleColor.DarkGray;
                            Console.WriteLine("Il tuo saldo attuale è di: " + String.Format("{0:0.00}", saldo.SaldoAttuale));
                            break;
                        case 4:
                            //Caso quattro per richiedere il resto
                            //Colore del testo console: blu scuro
                            Console.ForegroundColor = ConsoleColor.DarkBlue;
                            Console.WriteLine("Erogazione resto in corso...\nResto erogato: " + String.Format("{0:0.00}", saldo.SaldoAttuale));
                            //Colore del testo console: bianco
                            Console.ForegroundColor = ConsoleColor.White;
                            Distributore.LogErogazioneResto(saldo.SaldoAttuale);
                            Distributore.LogAccensioneSpegnimentoDistributore("Spegnimento",".");
                            break;
                        default:
                            //Colore del testo console: rosso
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Inserire un numero valido\n\n");
                            Distributore.LogErroreProgramma("Nel menu di selezione delle operazioni da fare è stato inserito un numero non valido.");
                            break;
                    }
                } while (scelta != 4);
            }
            //catch per l'exception
            catch (FormatException)
            {
                //Colore del testo console: rosso
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Input non valido, digita un numero.\nChiusura del programma...");
                Distributore.LogErroreProgramma("Il programma è stato chiuso in quanto nel menu di selezione delle operazioni da fare è stata inserita una stringa non valida invece di un numero.");
            }
        }
        //Metodo statico per inserimento denaro
        public static void InserisciDenaro(SaldoCliente saldo)
        {
            //Variabile per acquisire la scelta da console
            int scelta;
            try
            {
                do
                {
                    //Colore del testo console: giallo
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    //Richiesta del numero corrispondente all'importo da inserire
                    Console.WriteLine("Digita il numero corrispondente all'importo da inserire:\n1.0,10\n2.0,20\n3.0,50\n4.1,00\n5.2,00\n6.Esci");
                    scelta = int.Parse(Console.ReadLine());
                    //Case per aggiungere il denaro a seconda del numero inserito
                    switch (scelta)
                    {
                        case 1:
                            Distributore.AggiuntaDenaroEStampa(saldo, 0.10);
                            break;
                        case 2:
                            Distributore.AggiuntaDenaroEStampa(saldo, 0.20);
                            break;
                        case 3:
                            Distributore.AggiuntaDenaroEStampa(saldo, 0.50);
                            break;
                        case 4:
                            Distributore.AggiuntaDenaroEStampa(saldo, 1.00);
                            break;
                        case 5:
                            Distributore.AggiuntaDenaroEStampa(saldo, 2.00);
                            break;
                        case 6:
                            Console.WriteLine("Ritorno al menu...");
                            break;
                        default:
                            //Colore del testo console: rosso
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Inserire un numero valido");
                            Distributore.LogErroreProgramma("Nel menu di selezione del denaro da inserire è stato inserito un numero non valido.");
                            //Colore del testo console: blu
                            Console.ForegroundColor = ConsoleColor.Blue;
                            break;
                    }
                }while (scelta != 6);

            }
            //catch per l'exception
            catch (FormatException)
            {
                //Colore del testo console: rosso
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Input non valido, digita un numero\n");
                Distributore.LogErroreProgramma("Nel menu di selezione del denaro da inserire è stata inserita una stringa non valida invece di un numero.");
            }
        }
        //Metodo statico per l'aggiunta del denaro e per stampare il saldo attuale
        public static void AggiuntaDenaroEStampa(SaldoCliente saldo, double creditoDaAggiungere)
        {
            //Colore del testo console: grigio scuro
            Console.ForegroundColor = ConsoleColor.DarkGray;
            //Somma del credito inserito al saldo attuale
            saldo.CreditoInserito = creditoDaAggiungere;
            saldo.SaldoAttuale += saldo.CreditoInserito;
            Console.WriteLine("Credito aggiunto! Saldo attuale: " + String.Format("{0:0.00}", saldo.SaldoAttuale));
            Distributore.LogAggiuntaCredito(saldo.CreditoInserito, saldo.SaldoAttuale);
            //Colore del testo console: giallo
            Console.ForegroundColor = ConsoleColor.Yellow;
        }
        //Metodo per la ricerca del prodotto    
        public static void RicercaProdotto(SaldoCliente saldo)
        {
            //Variabile per acquisire il codice inserito
            int codiceInserito;
            //Colore del testo console: magenta
            Console.ForegroundColor = ConsoleColor.Magenta;
            try
            {
                Console.WriteLine("Seleziona usando il codice di riconoscimento il prodotto da acquistare");
                codiceInserito = int.Parse(Console.ReadLine());
                //Ciclo per la ricerca del prodotto
                for (int i = 0; i < Distributore.prodotti.Count; i++)
                {
                    //Se viene trovato il prodotto ed è disponibile
                    if ((codiceInserito == Distributore.prodotti[i].CodiceRiconoscimento) && (Distributore.prodotti[i].Disponibilità > 0))
                    {
                        //Colore del testo console: giallo scuro
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine("Il prodotto corrispondente al codice 0" + Distributore.prodotti[i].CodiceRiconoscimento + " è: " + Distributore.prodotti[i].Nome.Trim() + ",prezzo: " + Distributore.prodotti[i].Prezzo);
                        Distributore.prezzoProdotto = Distributore.prodotti[i].Prezzo;
                        //Richiamo del metodo per acquistare il prodotto
                        AcquistoProdotto(saldo);
                        //Decremento della disponibilità del prodotto
                        Distributore.prodotti[i].Disponibilità--;
                        Distributore.LogInserimentoCodiceRiconoscimento(codiceInserito, Distributore.prodotti[i].Nome, Distributore.prodotti[i].Disponibilità);
                    }
                    //Se il prodotto non è disponibile
                    else if ((codiceInserito == Distributore.prodotti[i].CodiceRiconoscimento) && (Distributore.prodotti[i].Disponibilità <= 0))
                    {
                        //Colore del testo console: rosso
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Il prodotto selezionato non è più disponibile, prova a comprare un altro prodotto");
                        Distributore.LogErroreProgramma("Nella ricerca del prodotto da acquistare il prodotto da acquistare è esaurito.");
                    }
                    //Se il codice inserito non è valido allora codiceInserito == 0
                    if (codiceInserito < 1 || codiceInserito > Distributore.prodotti.Count)
                    {
                        codiceInserito = 0;
                    }
                }
                //Se codiceInserito == 0
                if (codiceInserito == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Il numero inserito non corrisponde a nessun prodotto, riprova");
                    Distributore.LogErroreProgramma("Nella ricerca del prodotto da acquistare è stato inserito un numero che non corrisponde a nessun prodotto.");
                }
            }
            //catch per l'exception
            catch (FormatException)
            {
                //Colore del testo console: rosso
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Input non valido, digita un numero\n");
                Distributore.LogErroreProgramma("Nella ricerca del prodotto da acquistare è stata inserita una stringa non valida invece di un numero.");
            }
        }
        //Metodo statico per l'acquisto del prodotto
        public static void AcquistoProdotto(SaldoCliente saldo)
        {
            //Colore del testo console: giallo
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Tentativo di acquisto del prodotto...\n");
            //Se il saldo attuale non è sufficiente per acquistare il prodotto
            if (saldo.SaldoAttuale < Distributore.prezzoProdotto) 
            {
                //Colore del testo console: rosso
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Il credito inserito non è sufficiente per acquistare il prodotto selezionato, cerca un altro prodotto o inserisci il denaro\n\n");
                Distributore.LogAcquistoProdotto("fallito", saldo.CreditoInserito,Distributore.prezzoProdotto);
            }
            //Se invece saldo attuale è sufficiente
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                saldo.SaldoAttuale -= Distributore.prezzoProdotto;
                Console.WriteLine("Acquisto riuscito!\nSaldo attuale: " + String.Format("{0:0.00}", saldo.SaldoAttuale) + "\n\n");
                Distributore.LogAcquistoProdotto("riuscito", saldo.SaldoAttuale,Distributore.prezzoProdotto);
            }
        }
        //Metodo statico per scrivere sul file log per l'accensione del distributore
        public static void LogAccensioneSpegnimentoDistributore(string statoDistributore, string esitoLog)
        {
            using (TextWriter tsw = new StreamWriter("Log.txt", true))
            {
                tsw.WriteLine(statoDistributore + " del distributore" + esitoLog);
            }
        }
        //Metodo statico per scrivere sul file log l'aggiunta del credito
        public static void LogAggiuntaCredito(double creditoInseritoLog, double saldoAttualeLog)
        {
            using (TextWriter tsw = new StreamWriter("Log.txt", true))
            {
                tsw.WriteLine("L'utente ha inserito un credito di: " + String.Format("{0:0.00}", creditoInseritoLog) + ". Saldo attuale: " + String.Format("{0:0.00}", saldoAttualeLog));
                creditoInseritoLog = 0;
            }
        }
        //Metodo statico per scrivere sul file log l'inserimento del codice di riconoscimento con stampa delle informazioni sul prodotto
        public static void LogInserimentoCodiceRiconoscimento(int codiceLog,string nomeProdottoLog,int nuovaDisponibilitàLog)
        {
            using (TextWriter tsw = new StreamWriter("Log.txt", true))
            {
                tsw.WriteLine("L'utente ha inserito il codice: " + codiceLog + ",che corrisponde al prodotto: " + nomeProdottoLog +". Nuova disponibilità del prodotto: " + nuovaDisponibilitàLog);
            }
        }
        //Metodo statico per scrivere sul file log l'esito dell'acquisto
        public static void LogAcquistoProdotto(string esitoAcquistoLog, double saldoAttualeLog, double prezzoProdottoLog)
        {
            using (TextWriter tsw = new StreamWriter("Log.txt", true))
            {
                tsw.WriteLine("Tentativo di acquisto " + esitoAcquistoLog + ": credito attuale cliente: " + String.Format("{0:0.00}", saldoAttualeLog));
                if(esitoAcquistoLog == "riuscito")
                {
                    tsw.WriteLine("L'utente ha speso: " + String.Format("{0:0.00}", prezzoProdottoLog));
                }
            }
        }
        //Metodo statico per scrivere sul file log il resto erogato
        public static void LogErogazioneResto(double restoErogatoLog)
        {
            using (TextWriter tsw = new StreamWriter("Log.txt", true))
            {
                tsw.WriteLine("Resto erogato: " + String.Format("{0:0.00}", restoErogatoLog));
            }
        }
        //Metodo statico per scrivere sul file log l'eventuale motivo della chiusura prematura del programma
        public static void LogErroreProgramma(string esitoLog)
        {
            using (TextWriter tsw = new StreamWriter("Log.txt", true))
            {
                tsw.WriteLine(esitoLog);
            }
        }
    }
}
