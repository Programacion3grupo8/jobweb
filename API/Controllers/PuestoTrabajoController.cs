using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        // GET: api/PuestoTrabajo
        [HttpGet]
        public IEnumerable<PuestoTrabajo> Get(string search = "")
        {
            return db.PuestoTrabajo.ToList();
            //dynamic modelresults = new ExpandoObject();

            //if (search == "")
            //{
            //    var all = db.PuestoTrabajo.Join(db.Compañia,
            //    jobs => jobs.idCompañia,
            //    com => com.id,
            //    (jobs, com) => new { Jobs = jobs, Com = com }).Join(db.Categoria,
            //    jobs => jobs.Jobs.idCategoria,
            //    cat => cat.id,
            //    (jobs, cat) => new { Jobs = jobs, Cat = cat }).Select(q => new {
            //        q.Jobs.Com.nombre,
            //        q.Cat.categoria,
            //        q.Jobs.Jobs.tipo,
            //        q.Jobs.Jobs.posicion,
            //        q.Jobs.Jobs.ubicacion,
            //        q.Jobs.Com.logo
            //    }).ToList();

            //    return (IEnumerable<Listing>)all;
            //}

            ////modelresults.Categories = db.Categoria.Where(q => q.categoria.Contains(search)).ToList();
            ////modelresults.Locations = db.PuestoTrabajo.Where(q => q.ubicacion.Contains(search)).ToList();
            ////modelresults.Positions = db.PuestoTrabajo.Where(q => q.posicion.Contains(search)).ToList();
            ////modelresults.Companies = db.Compañia.Where(q => q.nombre.Contains(search)).ToList();

            //var q = db.PuestoTrabajo.Join(db.Compañia,
            //    jobs => jobs.idCompañia,
            //    com => com.id,
            //    (jobs, com) => new { Jobs = jobs, Com = com }).Join(db.Categoria,
            //    jobs => jobs.Jobs.idCategoria,
            //    cat => cat.id,
            //    (jobs, cat) => new { Jobs = jobs, Cat = cat }).Where(
            //    q => q.Jobs.Jobs.posicion.Contains(search) ||
            //    q.Jobs.Jobs.ubicacion.Contains(search) ||
            //    q.Cat.categoria.Contains(search) ||
            //    q.Jobs.Com.nombre.Contains(search)).Select(q => new
            //    {
            //        q.Jobs.Com.nombre,
            //        q.Cat.categoria,
            //        q.Jobs.Jobs.tipo,
            //        q.Jobs.Jobs.posicion,
            //        q.Jobs.Jobs.ubicacion,
            //        q.Jobs.Com.logo
            //    }).ToList();

            //return (IEnumerable<Listing>)q;
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
