using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Repositorio.ViewModel;

//Clase preparada para trabajar con Entity, implementará la interfaz IRepositorio con su respectiva clase
//conectada y desconectada
namespace Repositorio.Repositorio
{
    //Esta clase implementa la Interface IRepositorio donde TModelo es una clase conectada
    //y TViewModel es la clase desconectada de ese TModelo
    public class RepositorioEntity<TModelo, TViewModel> : IRepositorio<TModelo, TViewModel>
        where TModelo : class
        where TViewModel : IViewModel<TModelo>, new()
    {
        //Conexión con Entity
        private DbContext context;

        //DbSet de la conexión, seria el equivalente a "context.Alumno", "context.Curso", etc
        protected DbSet<TModelo> DbSet => context.Set<TModelo>();

        //Constructor que inicializa la conexión
        public RepositorioEntity(DbContext context)
        {
            this.context = context;
        }

        //Añade un modelo a la BBDD, recibe y devuelve un objeto desconectado
        //En el proceso lo transforma a un objeto conectado y lo inserta usando el DbSet
        public TViewModel Add(TViewModel model)
        {
            //obtiene la version conectada del objeto desconectado
            var m = model.ToBaseDatos();

            //inserta esa version conectada
            DbSet.Add(m);
            try
            {
                //guarda los cambios
                context.SaveChanges();

                //recupera la version desconectada (para que? si es la misma version que el model)
                model.FromBaseDatos(m);
                return model;
            }
            catch (Exception e)
            {
                //si falla devuelve su valor por defecto
                return default(TViewModel);
            }

        }

        //Recibe un objeto desconectado, obtiene sus claves primarias y lo borra
        public int Borrar(TViewModel model)
        {
            //Busca el objeto conectado a partir de las claves primarias
            //declaradas en el objeto desconectado
            var obj = DbSet.Find(model.GetKeys());

            //lo borra
            DbSet.Remove(obj);

            try
            {
                //guarda los cambios y devuelve el numero de filas afectadas
                return context.SaveChanges();
            }
            catch (Exception e)
            {
                //si falla devuelve 0 
                return 0;
            }


        }

        //Borra uno o mas objetos a partir de una expresion lambda
        public int Borrar(Expression<Func<TModelo, bool>> consulta)
        {
            //obtiene los registros que cumplan la restricción
            var data = DbSet.Where(consulta);

            //los borra
            DbSet.RemoveRange(data);

            try
            {
                //guarda los cambios y devuelve el numero de filas afectadas
                return context.SaveChanges();
            }
            catch (Exception e)
            {
                //si falla devuelve 0 
                return 0;
            }
        }

        //Recibe un objeto desconectado, obtiene sus claves primarias
        //y lo actualiza en la BBDD
        public int Actualizar(TViewModel model)
        {
            //Busca el objeto conectado a partir de las claves primarias
            //declaradas en el objeto desconectado
            var obj = DbSet.Find(model.GetKeys());

            //Actualiza los valores del objeto conectado a partir de las 
            //propiedades del objeto desconectado
            model.UpdateBaseDatos(obj);

            try
            {
                //guarda los cambios y devuelve el numero de filas afectadas
                return context.SaveChanges();
            }
            catch (Exception e)
            {
                //si falla devuelve 0 
                return 0;
            }
        }

        //Obtiene un conjunto de objetos desconectados a partir de sus versiones conectadas
        public ICollection<TViewModel> Get()
        {
            //crea la lista en la que se van a guardar los objetos desconectados
            var data = new List<TViewModel>();

            //obtiene todos los objetos conectados en relacion al modelo del BdSet (Alumno, Curso, etc)
            foreach (var modelo in DbSet)
            {
                //Crea un objeto desconectado 
                //(Será de la clase que corresponda, Alumno, Curso, Aula, etc)
                TViewModel obj = new TViewModel();

                //Transpasa las propiedades del objeto conectado a las propiedades
                //del objeto desconectado
                obj.FromBaseDatos(modelo);

                //lo añade a la lista de objetos desconectados
                data.Add(obj);
            }

            //devuelve la lista
            return data;
        }

        //Obtiene un objeto desconectado a partir de una expresion lambda
        public TViewModel Get(params object[] keys)
        {
            //Obtiene el objeto conectado a partir de las claves primaras 
            //declaradas en el objeto desconectado
            var dato = DbSet.Find(keys);

            //Crea un objeto desconectado
            //(Será de la clase que corresponda, Alumno, Curso, Aula, etc)
            TViewModel obj = new TViewModel();

            //Transpasa las propiedades del objeto conectado a las propiedades
            //del objeto desconectado
            obj.FromBaseDatos(dato);

            //lo devuelve
            return obj;
        }

        //Recupera una serie de objetos desconectados a partir de una expresion lambda
        public ICollection<TViewModel> Get(Expression<Func<TModelo, bool>> consulta)
        {
            //crea la lista en la que se van a guardar los objetos desconectados
            var data = new List<TViewModel>();

            //obtiene y recorre todos los objetos conectados que cumplan la expresion lambda
            foreach (var modelo in DbSet.Where(consulta))
            {
                //Crea un objeto desconectado
                //(Será de la clase que corresponda, Alumno, Curso, Aula, etc)
                TViewModel obj = new TViewModel();

                //Transpasa las propiedades del objeto conectado a las propiedades
                //del objeto desconectado
                obj.FromBaseDatos(modelo);

                //lo añade a la lista de objetos desconectados
                data.Add(obj);
            }

            //devuelve la lista
            return data;
        }
    }
}
