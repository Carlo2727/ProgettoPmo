using System;
using System.Collections.Generic;
using System.Text;

namespace FoodDispenser
{
    class Prodotto
    {
        //Attributo per il nome
        public string Nome
        { 
            get; 
            set;
        }
        //Attributo per il prezzo
        public double Prezzo
        {
            get;
            set;
        }
        //Attributo per il codice di riconoscimento
        public int CodiceRiconoscimento
        {
            get;
            set;
        }
        //Attributo per la disponibilità
        public int Disponibilità
        {
            get;
            set;
        }
    }
}
