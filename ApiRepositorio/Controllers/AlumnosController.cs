using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.Practices.Unity;
using Repositorio.Repositorio;
using Repositorio.ViewModel;
using Repositorio.Model;

namespace ApiRepositorio.Controllers
{
    public class AlumnosController : ApiController
    {
        [Dependency]
        public IRepositorio<Alumno, ViewModelAlumno> repo { get; set; }

        /*public AlumnosController()
        {
            context = new Alumno15Entities();
            repo = new RepositorioEntity<Alumno, ViewModelAlumno>(context);
        }*/

        public ICollection<ViewModelAlumno> Get()
        {
            return repo.Get();
        }

        [ResponseType(typeof(ViewModelAlumno))]
        public IHttpActionResult Get(String id)
        {
            var data = repo.Get(id);
            if (data == null)
                return NotFound();

            return Ok(data);
        }
    }
}
