namespace RamirezZuritaJordanGerman.Views;

public partial class CamaraPage : ContentPage
{
	public CamaraPage()
	{
		InitializeComponent();
	}

    private async void Button_Clicked(object sender, EventArgs e)
    {
        try
        {
            if (MediaPicker.Default.IsCaptureSupported)
            {
                var photo = await MediaPicker.Default.CapturePhotoAsync();
                if (photo != null)
                {
                    var stream = await photo.OpenReadAsync();
                    photoImage.Source = ImageSource.FromStream(() => stream);
                }
            }
            else
            {
                await DisplayAlert("Error", "La cámara no está disponible", "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"No se pudo tomar la foto: {ex.Message}", "OK");
        }
    }
}