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
    builder.EntitySet<Area>("Areas1");
    builder.EntitySet<Project>("Projects"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class Areas1Controller : ODataController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: odata/Areas1
        [EnableQuery]
        public IQueryable<Area> GetAreas1()
        {
            return db.Areas;
        }

        // GET: odata/Areas1(5)
        [EnableQuery]
        public SingleResult<Area> GetArea([FromODataUri] int key)
        {
            return SingleResult.Create(db.Areas.Where(area => area.AreaId == key));
        }

        // PUT: odata/Areas1(5)
        public IHttpActionResult Put([FromODataUri] int key, Delta<Area> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Area area = db.Areas.Find(key);
            if (area == null)
            {
                return NotFound();
            }

            patch.Put(area);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AreaExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(area);
        }

        // POST: odata/Areas1
        public IHttpActionResult Post(Area area)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Areas.Add(area);
            db.SaveChanges();

            return Created(area);
        }

        // PATCH: odata/Areas1(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<Area> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Area area = db.Areas.Find(key);
            if (area == null)
            {
                return NotFound();
            }

            patch.Patch(area);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AreaExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(area);
        }

        // DELETE: odata/Areas1(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            Area area = db.Areas.Find(key);
            if (area == null)
            {
                return NotFound();
            }

            db.Areas.Remove(area);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/Areas1(5)/Projects
        [EnableQuery]
        public IQueryable<Project> GetProjects([FromODataUri] int key)
        {
            return db.Areas.Where(m => m.AreaId == key).SelectMany(m => m.Projects);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AreaExists(int key)
        {
            return db.Areas.Count(e => e.AreaId == key) > 0;
        }
    }
}
