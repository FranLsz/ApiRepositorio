using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Repositorio.ViewModel;

//Se crea una interfaz generica que sera implementada
//en cada tipo de repositorio (Entity, WebService, SAP, etc)
namespace Repositorio.Repositorio
{
    //La interfaz IRepositorio tendra dos objetos:

    //TModelo -> Objeto conectado con la BBDD (Alumno, Curso, etc)

    //TViewModel -> Representación desconectada de su respectivo 
    //objeto conectado (ViewModelAlumno, ViewModelCurso, etc)

    //Ejemplo: IRepository<Alumno, ViewModelAlumno>

    //Establece que TModelo sera una clase y TViewModel incorporara la interfaz IViewModel<TModelo>
    public interface IRepositorio<TModelo, TViewModel>
        where TModelo : class
        where TViewModel : IViewModel<TModelo>
    {
        //Añade un objeto conectado a partir del objeto 
        //desconectado enviado por parametro
        TViewModel Add(TViewModel model);

        //Actualiza un objeto conectado a partir del objeto 
        //desconectado enviado por parametro y devuelve las filas afectadas
        int Actualizar(TViewModel model);

        //Borra un objeto conectado a partir del objeto 
        //desconectado enviado por parametro y devuelve las filas afectadas
        int Borrar(TViewModel model);

        //Permite borrar a partir de una expresion lambda
        int Borrar(Expression<Func<TModelo, bool>> consulta);

        //Devuelve una coleccion de objetos desconectados
        ICollection<TViewModel> Get();

        //El argumento es un array de claves primarias, 
        //"params" indica que se pueden meter X parametros
        TViewModel Get(params Object[] keys);

        //El argumento permite que se le pase a la funcion una expresion lambda
        //Esta expresion sera de la clase TModelo y devolvera true/false
        //en caso de que se cumpla o no
        ICollection<TViewModel> Get(Expression<Func<TModelo, bool>> consulta);
    }
}
