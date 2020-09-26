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
        public dynamic Get(string search = "")
        {

            if (search == "")
            {
                return db.PuestoTrabajo.ToList();
            }
            var q = db.PuestoTrabajo.Join(db.Compañia,
                jobs => jobs.idCompañia,
                com => com.id,
                (jobs, com) => new { Jobs = jobs, Com = com }).Join(db.Categoria,
                jobs => jobs.Jobs.idCategoria,
                cat => cat.id,
                (jobs, cat) => new { Jobs = jobs, Cat = cat }).Where(
                q => q.Jobs.Jobs.posicion.Contains(search) ||
                q.Jobs.Jobs.ubicacion.Contains(search) ||
                q.Cat.categoria.Contains(search) ||
                q.Jobs.Com.nombre.Contains(search)).ToList();
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
