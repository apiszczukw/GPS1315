using GPS1315.Infrastructure;
using Mapsui;
using Mapsui.Projections;

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


        }
    }

}
