using System;
using System.Collections.Generic;
using Repositorio.Model;

namespace Repositorio.ViewModel
{
    public class ViewModelAlumno : IViewModel<Alumno>
    {
        //informacion que se envia o recibe de la vista
        public string dni { get; set; }
        public string nombre { get; set; }
        public List<String> Cursos { get; set; }

        public Alumno ToBaseDatos()
        {
            var al = new Alumno()
            {
                dni = dni,
                nombre = nombre
            };
            return al;
        }

        public void FromBaseDatos(Alumno modelo)
        {
            dni = modelo.dni;
            nombre = modelo.nombre;
        }

        public void UpdateBaseDatos(Alumno modelo)
        {
            modelo.dni = dni;
            modelo.nombre = nombre;
        }

        public object[] GetKeys()
        {
            return new[] {dni};
        }
    }
}