using Microsoft.Maui.Controls;
using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace Prodygytek_Kapture_App.Views
{
    [QueryProperty(nameof(HtmlContent), "HtmlContent")]
    [QueryProperty(nameof(FormName), "formName")]
    [QueryProperty(nameof(SelectedUser), "selectedUser")]
    public partial class Forms : ContentPage
    {
        public Forms()
        {
            InitializeComponent();
        }

        public string HtmlContent
        {
            set{ LoadHtmlContent(value); }
        }

        private string formName;
        public string FormName
        {
            get => formName;
            set => formName = value;
        }

        private string selectedUser;
        public string SelectedUser
        {
            get => selectedUser;
            set => selectedUser = value;
        }

        private void LoadHtmlContent(string htmlContent)
        {
            var htmlSource = new HtmlWebViewSource
            {
                Html = htmlContent,
            };
            webView.Source = htmlSource;
        }

        private async void OnGuardarClicked(object sender, EventArgs e)
        {
            // Obtener los datos del formulario desde el WebView
            string data = await webView.EvaluateJavaScriptAsync("getFormData()");

            // Encriptar los datos
            EncryptMD5 encryptor = new EncryptMD5();
            string encryptedData = encryptor.Encrypt(data);

            // Definir el nombre del archivo y el Path
            string fileName = $"formData_{formName}_{SelectedUser}.json";
            string filePath = Path.Combine(FileSystem.AppDataDirectory, fileName);

            if (File.Exists(filePath))
            {
                bool overwrite = await DisplayAlert("Confirmar", "Datos existentes. ¿Desea actualizarlo con la información actual?", "Sí", "No");
                if (!overwrite)
                {
                    return; // Cancelar la operación si el usuario selecciona "No"
                }
            }

            await File.WriteAllTextAsync(filePath, encryptedData);

            // Mostrar la ruta del archivo para depuración
            await DisplayAlert("Guardado", $"Los datos del formulario se han guardado correctamente en: {filePath}", "OK");
        }


        private async void OnCargarClicked(object sender, EventArgs e)
        {
            string fileName = $"formData_{formName}_{SelectedUser}.json";
            string filePath = Path.Combine(FileSystem.AppDataDirectory, fileName);

            if (File.Exists(filePath))
            {
                string encryptedData2 = await File.ReadAllTextAsync(filePath);

                // Desencriptar los datos
                EncryptMD5 encryptor = new EncryptMD5();
                string data = encryptor.Decrypt(encryptedData2);

                // Cargar los datos en el formulario usando JavaScript
                await webView.EvaluateJavaScriptAsync($"loadFormData(\"{data}\")");
                await DisplayAlert("Cargado", $"Los datos del formulario se han cargado exitosamente", "OK");
            }
            else
            {
                await DisplayAlert("Error", "No se encontró el archivo de datos del formulario.", "OK");
            }
        }

    }
}
