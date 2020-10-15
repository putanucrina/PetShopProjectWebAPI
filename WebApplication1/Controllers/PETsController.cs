using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebApplication1;
using System.Web.Http.Cors;

namespace WebApplication1.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class PETsController : ApiController
    {
        private PetShopDBEntities db = new PetShopDBEntities();

        // GET: api/PETs
        public IQueryable<PET> GetPETs()
        {
            return db.PETs;
        }

        [HttpGet]
        [Route("api/pets/filter/{filter?}")]
        [ResponseType(typeof(IQueryable<PET>))]
        public IHttpActionResult Filter(string filter)
        {
            if (string.IsNullOrEmpty(filter))
            {
                return Ok(db.PETs);
            }

            return Ok(db.PETs.Where(x => x.Rasa.Contains(filter)));
        }

        // GET: api/PETs/5
        [ResponseType(typeof(PET))]
        public IHttpActionResult GetPET(int id)
        {
            PET pET = db.PETs.Find(id);
            if (pET == null)
            {
                return NotFound();
            }

            return Ok(pET);
        }

        // PUT: api/PETs/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPET(int id, PET pET)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != pET.Id)
            {
                return BadRequest();
            }

            db.Entry(pET).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PETExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/PETs
        [ResponseType(typeof(PET))]
        public IHttpActionResult PostPET(PET pET)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PETs.Add(pET);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = pET.Id }, pET);
        }

        // DELETE: api/PETs/5
        [ResponseType(typeof(PET))]
        public IHttpActionResult DeletePET(int id)
        {
            PET pET = db.PETs.Find(id);
            if (pET == null)
            {
                return NotFound();
            }

            db.PETs.Remove(pET);
            db.SaveChanges();

            return Ok(pET);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PETExists(int id)
        {
            return db.PETs.Count(e => e.Id == id) > 0;
        }
    }
}