using Repositorio.Model;

//**Comentarios puestos en ViewModelAlumno**
namespace Repositorio.ViewModel
{
    public class ViewModelCurso : IViewModel<Curso>
    {
        public int idCurso { get; set; }
        public string nombre { get; set; }
        public int duracion { get; set; }
        public System.DateTime inicio { get; set; }
        public System.DateTime fin { get; set; }
        public int idAula { get; set; }


        public Curso ToBaseDatos()
        {
            return new Curso()
            {
                idCurso = idCurso,
                idAula = idAula,
                nombre = nombre,
                duracion = duracion,
                inicio = inicio,
                fin = fin
            };

        }

        public void FromBaseDatos(Curso modelo)
        {
            idCurso = modelo.idCurso;
            idAula = modelo.idAula;
            nombre = nombre;
            inicio = inicio;
            fin = fin;
        }

        public void UpdateBaseDatos(Curso modelo)
        {
            modelo.idCurso = idCurso;
            modelo.idAula = idAula;
            modelo.nombre = nombre;
            modelo.inicio = inicio;
            modelo.fin = fin;
        }

        public object[] GetKeys()
        {
            return new[] { (object)idCurso };
        }
    }
}
