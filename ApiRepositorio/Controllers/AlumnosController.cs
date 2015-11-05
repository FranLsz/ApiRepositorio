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
        //Anotación para indicar que esta propiedad va a ser injectada
        [Dependency]
        public IRepositorio<Alumno, ViewModelAlumno> repo { get; set; }

        /*public AlumnosController()
        {
            context = new Alumno15Entities();
            repo = new RepositorioEntity<Alumno, ViewModelAlumno>(context);
        }*/

        public ICollection<ViewModelAlumno> GetAlumnos()
        {
            return repo.Get();
        }

        [ResponseType(typeof(ViewModelAlumno))]
        public IHttpActionResult GetAlumnos(String id)
        {
            //var data = repo.Get(dni); FAIL
            var data = repo.Get(o => o.dni.Equals(id));
            if (data == null)
                return NotFound();

            return Ok(data);
        }


        [ResponseType(typeof(ViewModelAlumno))]
        public IHttpActionResult PostAlumnos(ViewModelAlumno alumno)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            repo.Add(alumno);

            return Created("DefaultApi", alumno);
        }

        //pendiente
        [ResponseType(typeof(ViewModelAlumno))]
        public IHttpActionResult PutAlumnos(String id, ViewModelAlumno alumno)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != alumno.dni)
            {
                return BadRequest();
            }

            repo.Actualizar(alumno);

            return Created("DefaultApi", alumno);
        }

        //pendiente
        [ResponseType(typeof(ViewModelAlumno))]
        public IHttpActionResult DeleteAlumnos(String id)
        {
            var alumno = repo.Get(id);
            if (alumno == null)
            {
                return NotFound();
            }
            repo.Borrar(alumno);

            return Ok(alumno);
        }


    }
}
