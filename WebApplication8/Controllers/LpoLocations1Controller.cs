using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Http.OData;
using System.Web.Http.OData.Routing;
using WebApplication8.Models;

namespace WebApplication8.Controllers
{
    /*
    The WebApiConfig class may require additional changes to add a route for this controller. Merge these statements into the Register method of the WebApiConfig class as applicable. Note that OData URLs are case sensitive.

    using System.Web.Http.OData.Builder;
    using System.Web.Http.OData.Extensions;
    using WebApplication8.Models;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<LpoLocation>("LpoLocations1");
    builder.EntitySet<Lpo>("Lpoes"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    [Route("api/LpoLocations1")]
    public class LpoLocations1Controller : ODataController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: odata/LpoLocations1
        [EnableQuery]
        public IQueryable<LpoLocation> GetLpoLocations1()
        {
            return db.LpoLocations;
        }

        // GET: odata/LpoLocations1(5)
        [EnableQuery]
        public SingleResult<LpoLocation> GetLpoLocation([FromODataUri] int key)
        {
            return SingleResult.Create(db.LpoLocations.Where(lpoLocation => lpoLocation.LpoLocationId == key));
        }

        // PUT: odata/LpoLocations1(5)
        public IHttpActionResult Put([FromODataUri] int key, Delta<LpoLocation> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            LpoLocation lpoLocation = db.LpoLocations.Find(key);
            if (lpoLocation == null)
            {
                return NotFound();
            }

            patch.Put(lpoLocation);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LpoLocationExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(lpoLocation);
        }

        // POST: odata/LpoLocations1
        [HttpPost]
        [Route("api/LpoLocations1")]
        public IHttpActionResult Post(LpoLocation lpoLocation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.LpoLocations.Add(lpoLocation);
            db.SaveChanges();

            return Created(lpoLocation);
        }

        // PATCH: odata/LpoLocations1(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<LpoLocation> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            LpoLocation lpoLocation = db.LpoLocations.Find(key);
            if (lpoLocation == null)
            {
                return NotFound();
            }

            patch.Patch(lpoLocation);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LpoLocationExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(lpoLocation);
        }

        // DELETE: odata/LpoLocations1(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            LpoLocation lpoLocation = db.LpoLocations.Find(key);
            if (lpoLocation == null)
            {
                return NotFound();
            }

            db.LpoLocations.Remove(lpoLocation);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/LpoLocations1(5)/Lpos
        [EnableQuery]
        public IQueryable<Lpo> GetLpos([FromODataUri] int key)
        {
            return db.LpoLocations.Where(m => m.LpoLocationId == key).SelectMany(m => m.Lpos);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool LpoLocationExists(int key)
        {
            return db.LpoLocations.Count(e => e.LpoLocationId == key) > 0;
        }
    }
}
