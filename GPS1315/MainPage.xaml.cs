using GPS1315.Infrastructure;
using Mapsui;
using Mapsui.Layers;
using Mapsui.Nts;
using Mapsui.Projections;
using Mapsui.Styles;
using NetTopologySuite.Geometries;
using Color = Mapsui.Styles.Color;

namespace GPS1315
{
    public partial class MainPage : ContentPage
    {
       

        public MainPage()
        {
            InitializeComponent();

            mojaMapa.Map?.Layers.Add(Mapsui.Tiling.OpenStreetMap.CreateTileLayer());

            var wspolrzedne = SphericalMercator.FromLonLat(18.0084, 53.1235);
            var rJagiellonow = new MPoint(wspolrzedne.x, wspolrzedne.y);

            mojaMapa.Map?.Navigator.CenterOn(rJagiellonow);
            mojaMapa.Map?.Navigator.ZoomTo(2);
        }

        private void btnJedz_Clicked(object sender, EventArgs e)
        {
            var punktyTrasy = TrasaManager.PobierzDaneTestowe();

            if(punktyTrasy == null || punktyTrasy.Count == 0)
            {
                lblOpisTrasy.Text = "Nie znaleziono trasy";
                return;
            }

            var listaWspolrzednych = new List<Coordinate>();

            foreach (var punkt in punktyTrasy)
            {
                var wynikKonwersji = SphericalMercator.FromLonLat(punkt.Dlugosc, punkt.Szerokosc);

                listaWspolrzednych.Add(new Coordinate(wynikKonwersji.x, wynikKonwersji.y));
            }

            var ksztaltTrasy = new LineString(listaWspolrzednych.ToArray());

            var sciezkaNaMapie = new GeometryFeature(ksztaltTrasy);

            sciezkaNaMapie.Styles.Add(new VectorStyle
            {
                Line = new Pen(Color.Pink, 8)
            });

            var warstwaTrasy = new MemoryLayer()
            {
                Name = "WarstwaTrasy",
                Features = new[] { sciezkaNaMapie }
            };

            // czyszczenie starej linii
            var stareWarstwy = mojaMapa.Map.Layers.Where(w => w.Name == "WarstwaTrasy").ToList();

            foreach (var warstwa in stareWarstwy)
            {
                mojaMapa.Map.Layers.Remove(warstwa);
            }

            mojaMapa.Map.Layers.Add(warstwaTrasy);
            mojaMapa.Refresh();

            lblOpisTrasy.Text = "Trasa została narysowana!";
        }

        private async void btnTrasaOSRM_Clicked(object sender, EventArgs e)
        {
            // r.Jagiellonow
            double startLon = 18.0084;
            double startLat = 53.1235;

            // Hel
            double metaLat = 54.609445;
            double metaLon = 18.801177;


            var punktyTrasy = await TrasaManager.PobierzDaneOSRM(startLon, startLat, metaLon, metaLat);

            if (punktyTrasy == null || punktyTrasy.Count == 0)
            {
                lblOpisTrasy.Text = "Nie znaleziono trasy";
                return;
            }

            var listaWspolrzednych = new List<Coordinate>();

            foreach (var punkt in punktyTrasy)
            {
                var wynikKonwersji = SphericalMercator.FromLonLat(punkt.Dlugosc, punkt.Szerokosc);

                listaWspolrzednych.Add(new Coordinate(wynikKonwersji.x, wynikKonwersji.y));
            }

            var ksztaltTrasy = new LineString(listaWspolrzednych.ToArray());

            var sciezkaNaMapie = new GeometryFeature(ksztaltTrasy);

            sciezkaNaMapie.Styles.Add(new VectorStyle
            {
                Line = new Pen(Color.Pink, 8)
            });

            var warstwaTrasy = new MemoryLayer()
            {
                Name = "WarstwaTrasy",
                Features = new[] { sciezkaNaMapie }
            };

            // czyszczenie starej linii
            var stareWarstwy = mojaMapa.Map.Layers.Where(w => w.Name == "WarstwaTrasy").ToList();

            foreach (var warstwa in stareWarstwy)
            {
                mojaMapa.Map.Layers.Remove(warstwa);
            }

            mojaMapa.Map.Layers.Add(warstwaTrasy);
            mojaMapa.Refresh();

            lblOpisTrasy.Text = "Trasa została narysowana!";
        }

        private void btnZoomIn_Clicked(object sender, EventArgs e)
        {
            mojaMapa.Map.Navigator.ZoomIn();
        }

        private void btnZoomOut_Clicked(object sender, EventArgs e)
        {
            mojaMapa.Map.Navigator.ZoomOut();
        }
    }

}
