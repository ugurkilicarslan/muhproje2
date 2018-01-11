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
using Personelislemleri.Models;

namespace Personelislemleri.Controllers
{
    public class PersonelController : ApiController
    {
        //db sınıfı tanımlanır
        private DBModel db = new DBModel();

        // GET api/Personel
        public IQueryable<Personel> GetPersonel()
        {//get methodunda db deki personel classı geri döndürülür
            return db.Personel;
        }

        // GET api/Personel/5
        [ResponseType(typeof(Personel))]
        public IHttpActionResult GetPersonel(int id)
        {//eğer get ile birlik id de gönderilirse
            //db de personele ait idli kayıt aranır
            Personel personel = db.Personel.Find(id);
            if (personel == null)
            {//boş ise NotFound yollanır
                return NotFound();
            }
            //Boş değilse Ok olarak nesne yollanır
            return Ok(personel);
        }

        // PUT api/Personel/5
        public IHttpActionResult PutPersonel(int id, Personel personel)
        {//Güncelleme işlemlerinde gelen id ile birlikte personel modeli

            if (id != personel.Personelid)
            {//gelen personel modelindeki id ile get den gelen id eşit değilse
                return BadRequest();//badrequest döner
            }
            //eğer eşleşirse 
            //db ye personel modeli yollanır
            db.Entry(personel).State = EntityState.Modified;

            try
            {//db deki değişikler kayıt edilir
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {//eğer herhangi bir hata dönerse 
                if (!PersonelExists(id))
                {//notfound geri döndürülür
                    return NotFound();
                }
                else
                {//ise catch den atar
                    throw;
                }
            }
            //geriye nocontent yollanır
            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST api/Personel
        [ResponseType(typeof(Personel))]
        public IHttpActionResult PostPersonel(Personel personel)
        {//api ye personel modeli post edildiğinde

            //database e personel eklenir 
            db.Personel.Add(personel);
            //değişikler kayıt edilir
            db.SaveChanges();

            //geriye eklenen kayıt id si ve modeli geri döndürülür
            return CreatedAtRoute("DefaultApi", new { id = personel.Personelid }, personel);
        }

        // DELETE api/Personel/5
        [ResponseType(typeof(Personel))]
        public IHttpActionResult DeletePersonel(int id)
        {//apiye delete ile id geldiğinde
            //personel modelinde bu id li değer aranır
            Personel personel = db.Personel.Find(id);
            if (personel == null)
            {//eğer veri yoksa notfound döndürülür
                return NotFound();
            }
            //db den kayıt silinir
            db.Personel.Remove(personel);
            //db deki değişiklikler kayıt edilir
            db.SaveChanges();

            //geriye Ok olarak personel modeli yollanır
            return Ok(personel);
        }

        protected override void Dispose(bool disposing)
        {//bellek temizleme işlemi yapılır
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PersonelExists(int id)
        {//db deki personel idli kayıt kontrol edilir
            return db.Personel.Count(e => e.Personelid == id) > 0;
        }
    }
}