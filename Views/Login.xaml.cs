using Microsoft.Maui.ApplicationModel.Communication;

namespace Prodygytek_Kapture_App.Views;
public partial class Login : ContentPage
{
    public Login()
    {
        InitializeComponent();
    }

    private bool SimulateCredentialVerification()
    {
        // Simular la lógica de verificación de credenciales
        string email = emailEntry.Text; // Obtener el valor del Entry de correo
        string password = passwordEntry.Text; // Obtener el valor del Entry de contraseña

        return email == "narciniegas@prodygytek.com" && password == "12345";
    }

    private async void LoginButtonClicked(object sender, EventArgs e)
    {
        // Simular la verificación de credenciales
        bool areCredentialsValid = SimulateCredentialVerification();

        if (areCredentialsValid)
        {
            await DisplayAlert("\nValidado con éxito!", "\nCorreo y contraseña validos.", "OK");
            emailEntry.Text = string.Empty;
            passwordEntry.Text = string.Empty;

            await Shell.Current.GoToAsync("///main"); //Ir a la vista MainPage.xaml
        }
        else
        {
            await DisplayAlert("Error!!", "\nCorreo ó contraseña inválida.\nInténtelo de nuevo.", "OK");
            passwordEntry.Text = string.Empty;
        }
    }
}