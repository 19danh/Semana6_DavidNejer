using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Semana6_DavidNejer
{
    public partial class MainPage : ContentPage
    {

        private const string Url = "http://172.20.80.1/moviles/post.php";
        private readonly HttpClient client = new HttpClient();
        private ObservableCollection<Semana6_DavidNejer.Ws.Datos> _post;
        private int codigo = 0;
        private string nombre = null;
        private string apellido = null;
        private int edad = 0;

        public MainPage()
        {
            InitializeComponent();
            get();
        }

        public async void get()
        {
            try
            {
                var content = await client.GetStringAsync(Url);
                List<Semana6_DavidNejer.Ws.Datos> posts = JsonConvert.DeserializeObject<List<Semana6_DavidNejer.Ws.Datos>>(content);
                _post = new ObservableCollection<Semana6_DavidNejer.Ws.Datos>(posts);

                MyListView.ItemsSource = _post;

            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", "ERROR" + ex.Message, "OK");
            }
        }

        private async void btnPost_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new viewInsertar());
        }

        private  async void btnActualizar_Clicked(object sender, EventArgs e)
        {

            try
            {
                if (codigo == 0 & nombre == null & apellido == null & edad == 0)
                {
                    await DisplayAlert("Error", "Por favor, seleccione una fila", "Ok");
                }
                else
                {
                    await Navigation.PushAsync(new Actualizar(codigo, nombre, apellido, edad));
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
            }



        }

        private async void btnDelete_Clicked(object sender, EventArgs e)
        {

            if (codigo == 0 & nombre == null & apellido == null & edad == 0)
            {
                await DisplayAlert("Error", "Por favor, seleccione una fila", "Ok");
            }
            else
            {
                await Navigation.PushAsync(new Eliminar(codigo, nombre, apellido, edad));
            }
        }

        private void MyListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var sel = ((ListView)sender).SelectedItem as Semana6_DavidNejer.Ws.Datos;
            if (sel == null)
                return;
            codigo = sel.codigo;
            nombre = sel.nombre;
            apellido = sel.apellido;
            edad = sel.edad;
        }
    }
}
