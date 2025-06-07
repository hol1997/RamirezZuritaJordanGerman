using RamirezZuritaJordanGerman.Modelos;
namespace RamirezZuritaJordanGerman.Views;

public partial class PrincipalPage : ContentPage
{
    Estudiante estudianteSeleccionado;
    public PrincipalPage()
	{
		InitializeComponent();
        CargarEstudiantes();
    }

    private async void CargarEstudiantes()
    {
        estudiantesList.ItemsSource = await App.Database.ObtenerEstudiantesAsync();
        LimpiarCampos();
    }

    private async void Guardar_Clicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(nombreEntry.Text) || string.IsNullOrWhiteSpace(notaEntry.Text))
        {
            await DisplayAlert("Error", "Todos los campos son obligatorios", "OK");
            return;
        }

        float nota;
        if (!float.TryParse(notaEntry.Text, out nota))
        {
            await DisplayAlert("Error", "Nota inválida", "OK");
            return;
        }

        var estudiante = estudianteSeleccionado ?? new Estudiante();

        estudiante.Nombre = nombreEntry.Text;
        estudiante.Apellido = apellidoEntry.Text;
        estudiante.Curso = cursoEntry.Text;
        estudiante.Paralelo = paraleloEntry.Text;
        estudiante.NOTA_FINAL = nota;

        await App.Database.GuardarEstudianteAsync(estudiante);
        await DisplayAlert("Éxito", "Guardado correctamente", "OK");

        estudianteSeleccionado = null;
        CargarEstudiantes();
    }

    private async void Eliminar_Clicked(object sender, EventArgs e)
    {
        if (estudianteSeleccionado == null)
        {
            await DisplayAlert("Error", "Seleccione un estudiante para eliminar", "OK");
            return;
        }

        bool confirm = await DisplayAlert("Confirmar", "¿Eliminar estudiante?", "Sí", "No");
        if (confirm)
        {
            await App.Database.EliminarEstudianteAsync(estudianteSeleccionado);
            estudianteSeleccionado = null;
            CargarEstudiantes();
        }
    }

    private void OnSeleccionado(object sender, SelectedItemChangedEventArgs e)
    {
        estudianteSeleccionado = (Estudiante)e.SelectedItem;

        if (estudianteSeleccionado != null)
        {
            nombreEntry.Text = estudianteSeleccionado.Nombre;
            apellidoEntry.Text = estudianteSeleccionado.Apellido;
            cursoEntry.Text = estudianteSeleccionado.Curso;
            paraleloEntry.Text = estudianteSeleccionado.Paralelo;
            notaEntry.Text = estudianteSeleccionado.NOTA_FINAL.ToString();
        }

    }
    private void LimpiarCampos()
    {
        nombreEntry.Text = "";
        apellidoEntry.Text = "";
        cursoEntry.Text = "";
        paraleloEntry.Text = "";
        notaEntry.Text = "";
    }

    private  async void Button_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new CamaraPage());
    }
    private async void mostra_Clicked(object sender, EventArgs e)
{
    var lista = await App.Database.ObtenerEstudiantesAsync();

    if (lista == null || lista.Count == 0)
    {
        await DisplayAlert("Información", "No hay estudiantes registrados.", "OK");
        return;
    }

    string mensaje = "";

    foreach (var est in lista)
    {
        mensaje += $"Nombre: {est.Nombre} {est.Apellido}\n";
        mensaje += $"Curso: {est.NOTA_FINAL} - Paralelo: {est.Paralelo}\n";
        mensaje += $"Nota: {est.Curso}\n";
        mensaje += "---------------------------\n";
    }

    await DisplayAlert("Estudiantes Registrados", mensaje, "OK");
}
  private async void actualiza_Clicked(object sender, EventArgs e)
  {
      if (estudianteSeleccionado == null)
      {
          await DisplayAlert("Error", "Seleccione un estudiante de la lista para actualizar.", "OK");
          return;
      }

      if (string.IsNullOrWhiteSpace(nombreEntry.Text) ||
          string.IsNullOrWhiteSpace(apellidoEntry.Text) ||
          string.IsNullOrWhiteSpace(cursoEntry.Text) ||
          string.IsNullOrWhiteSpace(paraleloEntry.Text) ||
          string.IsNullOrWhiteSpace(notaEntry.Text))
      {
          await DisplayAlert("Error", "Todos los campos son obligatorios.", "OK");
          return;
      }

      if (!float.TryParse(notaEntry.Text, out float nota))
      {
          await DisplayAlert("Error", "La nota debe ser un número válido.", "OK");
          return;
      }

      estudianteSeleccionado.Nombre = nombreEntry.Text;
      estudianteSeleccionado.Apellido = apellidoEntry.Text;
      estudianteSeleccionado.Curso = cursoEntry.Text;
      estudianteSeleccionado.Paralelo = paraleloEntry.Text;
      estudianteSeleccionado.NOTA_FINAL = nota;

      await App.Database.GuardarEstudianteAsync(estudianteSeleccionado);

      await DisplayAlert("Actualizado", "Estudiante actualizado correctamente", "OK");

      estudianteSeleccionado = null;
      CargarEstudiantes();
  }
}
