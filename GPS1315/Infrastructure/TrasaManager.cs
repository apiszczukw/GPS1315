using GPS1315.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GPS1315.Infrastructure
{
    public static class TrasaManager
    {


        public static List<PunktGeo> PobierzDaneTestowe()
        {
            // dane testowe prosze wkleić z pastebina -> t.ly/C8t87
            string testJson = @"
        {
            ""routes"": [
                {
                    ""geometry"": {
                        ""coordinates"": [
                            [18.0084, 53.1235],
                            [18.0100, 53.1242],
                            [18.0125, 53.1255],
                            [18.0150, 53.1268],
                            [18.0185, 53.1280]
                        ]
                    }
                }
            ]
        }";

            var odp = JsonSerializer.Deserialize<OdpowiedzOSRM>(testJson);

            var listaPunktow = new List<PunktGeo>();


            if(odp != null && odp.ListaTras.Count > 0)
            {
                var geometriaTrasy = odp.ListaTras[0].Geometria;

                if(geometriaTrasy != null && geometriaTrasy.PunktyWspolrzednych != null)
                {
                    foreach (var wspolrzedne in geometriaTrasy.PunktyWspolrzednych)
                    {
                        // najpierw w OSRM jest dlugość, potem szerokość
                        listaPunktow.Add(new PunktGeo(wspolrzedne[0], wspolrzedne[1]));
                    }
                }
            }



            return listaPunktow;
        }
    }
}
