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
        // Simular la l�gica de verificaci�n de credenciales
        string email = emailEntry.Text; // Obtener el valor del Entry de correo
        string password = passwordEntry.Text; // Obtener el valor del Entry de contrase�a

        return email == "narciniegas@prodygytek.com" && password == "12345";
    }

    private async void LoginButtonClicked(object sender, EventArgs e)
    {
        // Simular la verificaci�n de credenciales
        bool areCredentialsValid = SimulateCredentialVerification();

        if (areCredentialsValid)
        {
            await DisplayAlert("\nValidado con �xito!", "\nCorreo y contrase�a validos.", "OK");
            emailEntry.Text = string.Empty;
            passwordEntry.Text = string.Empty;

            await Shell.Current.GoToAsync("///main"); //Ir a la vista MainPage.xaml
        }
        else
        {
            await DisplayAlert("Error!!", "\nCorreo � contrase�a inv�lida.\nInt�ntelo de nuevo.", "OK");
            passwordEntry.Text = string.Empty;
        }
    }
}