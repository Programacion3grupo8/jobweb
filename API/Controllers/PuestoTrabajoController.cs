using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Dynamic;
using API.Data;
using API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PuestoTrabajoController : ControllerBase
    {
        private JobWebDB db = new JobWebDB();
        // GET: api/PuestoTrabajo or GET: api/PuestoTrabajo?search=query
        [HttpGet]
        public IEnumerable<Listing> Get(string search = "")
        {

            if (search == "")
            {

                List<Listing> all = (from job in db.PuestoTrabajo
                                     join com in db.Compañia on job.idCompañia equals com.id
                                     join cat in db.Categoria on job.idCategoria equals cat.id
                                     select new
                                     {
                                         Nombre = com.nombre,
                                         Categoria = cat.categoria,
                                         Tipo = job.tipo,
                                         Posicion = job.posicion,
                                         Ubicacion = job.ubicacion,
                                         Logo = com.logo
                                     }).AsEnumerable().Select(x => new Listing
                                     {
                                         company = x.Nombre,
                                         categoria = x.Categoria,
                                         tipo = x.Tipo,
                                         posicion = x.Posicion,
                                         ubicacion = x.Ubicacion,
                                         logo = x.Logo
                                     }).ToList();

                return all;
            }

            List<Listing> q = db.PuestoTrabajo.Join(db.Compañia,
                jobs => jobs.idCompañia,
                com => com.id,
                (jobs, com) => new { Jobs = jobs, Com = com }).Join(db.Categoria,
                jobs => jobs.Jobs.idCategoria,
                cat => cat.id,
                (jobs, cat) => new { Jobs = jobs, Cat = cat }).Where(
                q => q.Jobs.Jobs.posicion.Contains(search) ||
                q.Jobs.Jobs.ubicacion.Contains(search) ||
                q.Cat.categoria.Contains(search) ||
                q.Jobs.Com.nombre.Contains(search)).Select(q => new
                {
                    Nombre = q.Jobs.Com.nombre,
                    Categoria = q.Cat.categoria,
                    Tipo = q.Jobs.Jobs.tipo,
                    Posicion = q.Jobs.Jobs.posicion,
                    Ubicacion = q.Jobs.Jobs.ubicacion,
                    Logo = q.Jobs.Com.logo
                }).AsEnumerable().Select(x => new Listing {
                    company = x.Nombre,
                    categoria = x.Categoria,
                    logo = x.Logo,
                    posicion = x.Posicion,
                    tipo = x.Tipo,
                    ubicacion = x.Ubicacion
                }).ToList();

            return q;
        }

        // GET: api/PuestoTrabajo/5
        [HttpGet("{id}")]
        public PuestoTrabajo Get(int id)
        {
            return db.PuestoTrabajo.Find(id);
        }

        // POST: api/PuestoTrabajo
        [HttpPost]
        public void Post([FromBody] PuestoTrabajo value)
        {
            db.PuestoTrabajo.Add(value);
            db.SaveChanges();
        }

        // PUT: api/PuestoTrabajo/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] PuestoTrabajo value)
        {
            value.id = id;
            db.PuestoTrabajo.Update(value);
            db.SaveChanges();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            PuestoTrabajo value = db.PuestoTrabajo.Find(id);
            db.PuestoTrabajo.Remove(value);
            db.SaveChanges();
        }
    }
}
