using RamirezZuritaJordanGerman.Modelos;
namespace RamirezZuritaJordanGerman
{
    public partial class App : Application
    {
        private static EstudianteDatabase _database;

        public static EstudianteDatabase Database
        {
            get
            {
                if (_database == null)
                {
                    string dbPath = Path.Combine(FileSystem.AppDataDirectory, "estudiantes.db3");
                    _database = new EstudianteDatabase(dbPath);
                }
                return _database;
            }
        }

        public App()
        {
            InitializeComponent();
            InicializarDatosAsync();
        }

        private async void InicializarDatosAsync()
        {
            var db = Database;
            var usuario = await db.ValidarUsuario("", "");

            if (usuario == null)
            {
                await db.GuardarLoginAsync(new Modelos.EstudianteLogin
                {
                    USUARIO = "",
                    CONTRASEÑA = "",
                });
            }
        }
        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new NavigationPage(new Views.LoginPage()));
        }
    }
}