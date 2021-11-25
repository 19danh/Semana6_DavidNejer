using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Semana6_DavidNejer
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Eliminar : ContentPage
    {

        private HttpClient client;
        private int pk = 0;

        public Eliminar(int codigo, string nombre, string apellido, int edad)
        {
            InitializeComponent();
            Resultado.Text = "¿Desea elimiar el registro?" + codigo + nombre + apellido + edad;
            pk = codigo;
        }

        private async void btnAceptar_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (pk == 0)
                {
                    await DisplayAlert("Error", "No se encontro el codigo", "Ok");
                    await Navigation.PushAsync(new MainPage());
                }
                else
                {
                    client = new HttpClient();
                    await client.DeleteAsync("http://172.20.80.1/moviles/post.php?codigo=" + pk);
                    await DisplayAlert("Alerta", "Registro Eliminado", "Ok");
                    await Navigation.PushAsync(new MainPage());

                }
            }

            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "Ok");
            }
        }

    private void btnCancelar_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MainPage());
        }
    }
}