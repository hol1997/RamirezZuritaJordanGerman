using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace RamirezZuritaJordanGerman.Modelos
{
    public class EstudianteDatabase
    {
        private readonly SQLiteAsyncConnection _database;

        public EstudianteDatabase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Estudiante>().Wait();
            _database.CreateTableAsync<EstudianteLogin>().Wait();
        }

        // Métodos de Login
        public Task<EstudianteLogin> ValidarUsuario(string usuario, string contraseña) =>
            _database.Table<EstudianteLogin>().Where(x => x.USUARIO == usuario && x.CONTRASEÑA == contraseña).FirstOrDefaultAsync();

        // CRUD para Estudiantes
        public Task<List<Estudiante>> ObtenerEstudiantesAsync() => _database.Table<Estudiante>().ToListAsync();
        public Task<int> GuardarEstudianteAsync(Estudiante est) => est.COD_ESTUDIANTE != 0 ? _database.UpdateAsync(est) : _database.InsertAsync(est);
        public Task<int> EliminarEstudianteAsync(Estudiante est) => _database.DeleteAsync(est);

        public Task<int> GuardarLoginAsync(EstudianteLogin login) =>
    _database.InsertAsync(login);
    }
}
