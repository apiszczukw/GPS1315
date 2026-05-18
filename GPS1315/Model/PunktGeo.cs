using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPS1315.Model
{
    public class PunktGeo
    {
        public double Dlugosc { get; set; }

        public double Szerokosc { get; set; }

        public PunktGeo(double dlugosc, double szerokosc)
        {
            Dlugosc = dlugosc;
            Szerokosc = szerokosc;
        }
    }
}
