using GPS1315.Infrastructure;
using GPS1315.Model;
using System.Text.Json;

namespace GPS1315;

public partial class WyznaczTrasePage : ContentPage
{
    PunktGeo punktStartowy;
    PunktGeo punktDocelowy;

	public WyznaczTrasePage()
	{
		InitializeComponent();
	}

    private async void btnSzukaj_Clicked(object sender, EventArgs e)
    {
        string adresStart = entAdresStart.Text;

        string url = $"https://nominatim.openstreetmap.org/search?q={adresStart}&format=json&limit=1";

        var client = new HttpClient();
        client.DefaultRequestHeaders.Add("User-Agent", "ProjektStudenckiUKW");

        var json = await client.GetStringAsync(url);

        var doc = JsonDocument.Parse(json);

        var root = doc.RootElement;

        if(root.GetArrayLength() > 0)
        {
            var wynik = root[0];

            string nazwa = wynik.GetProperty("display_name").GetString();

            lblWynikStart.Text = "Znaleziono punkt startowy:\n" + nazwa;

            double lat = double.Parse(wynik.GetProperty("lat").GetString(), System.Globalization.CultureInfo.InvariantCulture);
            double lon = double.Parse(wynik.GetProperty("lon").GetString(), System.Globalization.CultureInfo.InvariantCulture);

            punktStartowy = new PunktGeo(lon, lat);
        }
    }

    private async void btnZatwierdz_Clicked(object sender, EventArgs e)
    {
        if(punktStartowy != null)
        {
            DaneGeo.CzyNowyStart = true;
            DaneGeo.PunktStartowy = punktStartowy;

            await Navigation.PopAsync();
        }
    }
}