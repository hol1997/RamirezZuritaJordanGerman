﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace RamirezZuritaJordanGerman.Modelos
{
    [Table("estudiantelogin")]
    public class EstudianteLogin
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string USUARIO { get; set; }
        public string CONTRASEÑA { get; set; }
    }
}
