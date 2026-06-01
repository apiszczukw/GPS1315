using GPS1315.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPS1315.Infrastructure
{
    public class DaneGeo
    {
        public static PunktGeo PunktStartowy { get; set; }

        public static bool CzyNowyStart { get; set; } = false;
    }
}
