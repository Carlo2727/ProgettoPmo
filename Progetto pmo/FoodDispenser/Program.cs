using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FoodDispenser
{
    class Program
    {
        static void Main()
        {
            //Cancellazione del file log precedente
            File.Delete("Log.txt");
            //Richiamo del metodo statico accendiDistributore
            Distributore.AccendiDistributore();            
        }
    }
}
