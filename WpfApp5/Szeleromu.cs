using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp5
{
     public class Szeleromu
    {
        public string nev { get; set; }
        public int szam { get; set; }
        public int teljesitmeny { get; set; }
        public int mukodesEv { get; set; }

        public Szeleromu(string sor) 
        {
            string[] sorok = sor.Split(';');
            nev = sorok[0];
            szam = int.Parse(sorok[1]);
            teljesitmeny= int.Parse(sorok[2]);
            mukodesEv= int.Parse(sorok[3]);
        }

        public char GetCategory()
        {
            if (teljesitmeny >= 1000)
            {
                return 'A';
            }
            else if (teljesitmeny >= 500 && teljesitmeny < 1000)
            {
                return 'B';
            }
            else
            {
                return 'C';
            }
        }
    }
}
