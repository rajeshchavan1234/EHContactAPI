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
using EHContactAPI.Models;

namespace EHContactAPI.Controllers
{
    public class EHContactsController : ApiController
    {
        private EHContactDBEntities db = new EHContactDBEntities();

        // GET: api/EHContacts
        public IEnumerable<EHContact> GetEHContacts()
        {
            return db.EHContacts.ToList();
        }

        // GET: api/EHContacts/5
        [ResponseType(typeof(EHContact))]
        public IHttpActionResult GetEHContact(int id)
        {
            EHContact eHContact = db.EHContacts.Find(id);
            if (eHContact == null)
            {
                return NotFound();
            }
            return Ok(eHContact);
        }

        // PUT: api/EHContacts/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutEHContact(EHContact eHContact)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var existingContact = db.EHContacts.Where(s => s.ContactID == eHContact.ContactID)
                                                    .FirstOrDefault<EHContact>();
            if (existingContact != null)
            {
                existingContact.FirstName = eHContact.FirstName;
                existingContact.LastName = eHContact.LastName;
                existingContact.Email = eHContact.Email;
                existingContact.PhoneNumber = eHContact.PhoneNumber;
                existingContact.Status = eHContact.Status;
                db.SaveChanges();
            }
            else
            {
                return NotFound();
            }
            return Ok(eHContact);
        }

        // POST: api/EHContacts
        [ResponseType(typeof(EHContact))]
        public IHttpActionResult PostEHContact(EHContact eHContact)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            db.EHContacts.Add(eHContact);
            db.SaveChanges();
            return CreatedAtRoute("DefaultApi", new { id = eHContact.ContactID }, eHContact);
        }

        // DELETE: api/EHContacts/5
        [ResponseType(typeof(EHContact))]
        public IHttpActionResult DeleteEHContact(int id)
        {
            EHContact eHContact = db.EHContacts.Find(id);
            if (eHContact == null)
            {
                return NotFound();
            }
            db.EHContacts.Remove(eHContact);
            db.SaveChanges();
            return Ok(eHContact);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EHContactExists(int id)
        {
            return db.EHContacts.Count(e => e.ContactID == id) > 0;
        }
    }
}