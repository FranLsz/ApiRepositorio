using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Repositorio.ViewModel;

namespace Repositorio.Repositorio
{
    public interface IRepositorio<TModelo, TViewModel> where TModelo : class where TViewModel : IViewModel<TModelo>
    {
        TViewModel Add(TViewModel model);
        int Borrar(TViewModel model);
        int Borrar(Expression<Func<TModelo, bool>> consulta);
        int Actualizar(TViewModel model);
        ICollection<TViewModel> Get();
        //es un array de claves primarias, el params indica que
        //se pueden meter  X parametros
        TViewModel Get(params Object[] keys);
        ICollection<TViewModel> Get(Expression<Func<TModelo, bool>> consulta);
    }
}
