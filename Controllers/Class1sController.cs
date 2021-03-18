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
using Ex3D41.Data;
using Ex3D41.Models;

namespace Ex3D41.Controllers
{
    /*
    The WebApiConfig class may require additional changes to add a route for this controller. Merge these statements into the Register method of the WebApiConfig class as applicable. Note that OData URLs are case sensitive.

    using System.Web.Http.OData.Builder;
    using System.Web.Http.OData.Extensions;
    using Ex3D41.Models;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<Class1>("Class1s");
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class Class1sController : ODataController
    {
        private MovDbContext db = new MovDbContext();

        // GET: odata/Class1s
        [EnableQuery]
        public IQueryable<Class1> GetClass1s()
        {
            return db.Class1;
        }

        // GET: odata/Class1s(5)
        [EnableQuery]
        public SingleResult<Class1> GetClass1([FromODataUri] int key)
        {
            return SingleResult.Create(db.Class1.Where(class1 => class1.MId == key));
        }

        // PUT: odata/Class1s(5)
        public IHttpActionResult Put([FromODataUri] int key, Delta<Class1> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Class1 class1 = db.Class1.Find(key);
            if (class1 == null)
            {
                return NotFound();
            }

            patch.Put(class1);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Class1Exists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(class1);
        }

        // POST: odata/Class1s
        public IHttpActionResult Post(Class1 class1)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Class1.Add(class1);
            db.SaveChanges();

            return Created(class1);
        }

        // PATCH: odata/Class1s(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<Class1> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Class1 class1 = db.Class1.Find(key);
            if (class1 == null)
            {
                return NotFound();
            }

            patch.Patch(class1);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Class1Exists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(class1);
        }

        // DELETE: odata/Class1s(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            Class1 class1 = db.Class1.Find(key);
            if (class1 == null)
            {
                return NotFound();
            }

            db.Class1.Remove(class1);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Class1Exists(int key)
        {
            return db.Class1.Count(e => e.MId == key) > 0;
        }
    }
}
