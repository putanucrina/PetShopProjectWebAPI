using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication2;
using WebApplication2.Models;
using System.Net.Http;

namespace WebApplication2.Controllers
{
    public class PetsController : Controller
    {

        // GET: Pets
        public async Task<ActionResult> Index()
        {
            IEnumerable<Pet> pets = Enumerable.Empty<Pet>(); 

            using (var client = new HttpClient())
            {
                //HTTP GET
                var response = await client.GetAsync("http://localhost:62368/api/pets");

                if (response.IsSuccessStatusCode)
                {
                    pets = await response.Content.ReadAsAsync<IEnumerable<Pet>>();
                }
            }

            return View(pets);
        }

        // GET: Pets/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            using (var client = new HttpClient())
            {
                //HTTP GET
                var response = await client.GetAsync($@"http://localhost:62368/api/pets/{id}");

                if (response.IsSuccessStatusCode)
                {
                    var pet = await response.Content.ReadAsAsync<Pet>();
                    return View(pet);
                }
               
            }
            return HttpNotFound();
        }

        // GET: Pets/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Pets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Categorie,Rasa,Pret,AnPrimire")] Pet pET)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    //HTTP GET
                    var response = await client.PostAsJsonAsync("http://localhost:62368/api/pets", pET);

                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                    
                }
                return RedirectToAction("Index");
            }

            return View(pET);
        }

        // GET: Pets/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            using (var client = new HttpClient())
            {
                //HTTP GET
                var response = await client.GetAsync($@"http://localhost:62368/api/pets/{id}");

                if (response.IsSuccessStatusCode)
                {
                    var pet = await response.Content.ReadAsAsync<Pet>();
                    return View(pet);
                }

            }
            return HttpNotFound();
        }

        // POST: Pets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Categorie,Rasa,Pret,AnPrimire")] Pet pET)
        {
            using (var client = new HttpClient())
            {
                //HTTP GET
                var response = await client.PutAsJsonAsync($@"http://localhost:62368/api/pets/{pET.Id}", pET);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }

                ModelState.AddModelError("Pet","Something went wrong while updating the db");

            }

            return View(pET);
        }

        // GET: Pets/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            using (var client = new HttpClient())
            {
                //HTTP GET
                var response = await client.GetAsync($@"http://localhost:62368/api/pets/{id}");

                if (response.IsSuccessStatusCode)
                {
                    var pet = await response.Content.ReadAsAsync<Pet>();
                    return View(pet);
                }
            }

            return HttpNotFound();
        }

        //// POST: Pets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            using (var client = new HttpClient())
            {
                //HTTP GET
                var response = await client.DeleteAsync($@"http://localhost:62368/api/pets/{id}");

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }

                ModelState.AddModelError("Pet", "Something went wrong while deleting the db");
            }

            return RedirectToAction("Index");
        }

    }
}
