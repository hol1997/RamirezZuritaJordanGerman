namespace RamirezZuritaJordanGerman.Views;

public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
		InitializeComponent();
	}

    private async void Button_Clicked(object sender, EventArgs e)
    {
        var usuario = usuarioEntry.Text?.Trim();
        var contrasena = contrasenaEntry.Text?.Trim();

        if (string.IsNullOrEmpty(usuario) || string.IsNullOrEmpty(contrasena))
        {
            await DisplayAlert("Error", "Ingrese usuario y contraseña", "OK");
            return;
        }

        var user = await App.Database.ValidarUsuario(usuario, contrasena);

        if (user != null)
        {
            await Navigation.PushAsync(new PrincipalPage());
        }
        else
        {
            await DisplayAlert("Error", "Usuario o contraseña incorrectos", "OK");
        }
    }
}