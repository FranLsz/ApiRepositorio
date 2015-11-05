namespace Repositorio.ViewModel
{
    //Toda entidad o clase (Alumno, Curso, Aula, etc) de la base de datos que quiera ser mapeada incorporará esta Interface
    //TModelo es la entidad en si, que como sera implementada por X entidades, se declara un objeto
    //genérico
    public interface IViewModel<TModelo>
        where TModelo : class
    {
        //Posee las principales funciones de acceso, consulta y manipulación de datos

        //INSERT
        TModelo ToBaseDatos();

        //SELECT
        void FromBaseDatos(TModelo modelo);

        //UPDATE
        void UpdateBaseDatos(TModelo modelo);

        //Obtención de las PRIMARY KEYS de la entidad
        object[] GetKeys();
    }
}