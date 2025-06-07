using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace RamirezZuritaJordanGerman.Modelos
{
    [Table("estudiante")]
    public class Estudiante
    {
        [PrimaryKey, AutoIncrement]
        public int COD_ESTUDIANTE { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Curso { get; set; }
        public string Paralelo { get; set; }
        public float NOTA_FINAL { get; set; }
    }
}
