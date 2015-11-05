using System;
using System.Collections.Generic;
using Repositorio.Model;

//Respresentación desconectada de un objeto conectado con la base de datos
//Será necesario crear una clase ViewModel por cada entidad que se quiera desconectar
//ViewModelAlumno(objeto desconectado) - Alumno(objeto conectado)
namespace Repositorio.ViewModel
{
    //Creación de una clase ViewModel para la entidad Alumno
    //Implementará la Interface IViewModel que operará con la clase Alumno
    public class ViewModelAlumno : IViewModel<Alumno>
    {
        //Propiedades que se envían o reciben de la vista
        //Normalmente serán las columnas de la bbdd
        public string dni { get; set; }
        public string nombre { get; set; }
        public List<String> Cursos { get; set; }

        //Transforma el objeto desconectado en uno conectado y lo devuelve
        public Alumno ToBaseDatos()
        {
            return new Alumno()
            {
                dni = dni,
                nombre = nombre
            };
        }

        //Guarda las propiedades de un objeto conectado recibido por parametro 
        //en las propiedades equivalentes del objeto desconectado
        public void FromBaseDatos(Alumno modelo)
        {
            dni = modelo.dni;
            nombre = modelo.nombre;
        }

        //Guarda las propiedades del objeto desconectado en las propiedades equivalentes
        //del objeto conectado
        public void UpdateBaseDatos(Alumno modelo)
        {
            modelo.dni = dni;
            modelo.nombre = nombre;
        }

        //Devolución de las PRIMARY KEYS del objeto
        public object[] GetKeys()
        {
            return new[] { dni };
        }
    }
}