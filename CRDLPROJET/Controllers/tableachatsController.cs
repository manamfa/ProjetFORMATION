using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CRDLPROJET.Models;
using Rotativa.MVC;

namespace CRDLPROJET.Controllers
{
    public class tableachatsController : Controller
    {
        private facturationclientBOUEntities2 db = new facturationclientBOUEntities2();

        // GET: tableachats
        List<tableachat> list = null;
        public ActionResult Index()
        {
            var facturation = db.facturations;
            ViewData["facturation"] = facturation.ToList();

            var tableachats = db.tableachats.Include(t => t.client).Include(t => t.produit);
            return View(tableachats.ToList());
        }

        public ActionResult Index1()
        {
            var facturation = db.facturations;
            ViewData["facturation"] = facturation.ToList();

            var tableachats = db.tableachats.Include(t => t.client).Include(t => t.produit);
            return View(tableachats.ToList());
        }

        public JsonResult getProducts(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<produit> Produit = db.produits.Where(x => x.categorieID == id).ToList();
            return Json(Produit, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Initialiser()
        {
            var vps = db.tableachats.ToList();
            foreach (var vp in vps)
                db.tableachats.Remove(vp);
            db.SaveChanges();
            return RedirectToAction("create3", "tableachats");
        }

        public ActionResult Initialis2()
        {
            var vps = db.tableachats.ToList();
            foreach (var vp in vps)
                db.tableachats.Remove(vp);
            db.SaveChanges();
            return RedirectToAction("create1", "tableachats");
        }

        public ActionResult Initialis3()
        {
            var vps = db.tableachats.ToList();
            foreach (var vp in vps)
                db.tableachats.Remove(vp);
            db.SaveChanges();
            return RedirectToAction("create", "clients");
        }

        public ActionResult Initialis4()
        {
            var vps = db.tableachats.ToList();
            foreach (var vp in vps)
                db.tableachats.Remove(vp);
            db.SaveChanges();
            return RedirectToAction("create1", "clients");
        }
        public ActionResult PrintPage()
        {

            return new ActionAsPdf("Index", list);
        }
        
        public JsonResult getUnitPrice(int product_id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var product = db.produits.Where(p => p.produitID == product_id).ToList().FirstOrDefault();
            return Json(product, JsonRequestBehavior.AllowGet);
        }


        // GET: tableachats/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tableachat tableachat = db.tableachats.Find(id);
            if (tableachat == null)
            {
                return HttpNotFound();
            }
            return View(tableachat);
        }



        public ActionResult Create2()
        {
            var client = db.clients;
            ViewData["client"] = client.ToList();

            var tableachat = db.tableachats;
            ViewData["tableachat"] = tableachat.ToList();

            var facturation = db.facturations;
            ViewData["facturation"] = facturation.ToList();

            ViewBag.categorieID = new SelectList(db.categories, "categorieID", "nomcategorie");
            ViewBag.clientID = new SelectList(db.clients, "clientID", "nomclient");
            ViewBag.produitID = new SelectList(db.produits, "produitID", "nomproduit");
            return View();
        }

        // POST: tableachats/Create
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create2(tableachat tableachat, historiquefacturation historiquefacturation, produit produit)
        {
            var table = db.tableachats;
            ViewData["table"] = table.ToList();

            List<Histofactmodel> model = (from d in db.tableachats

                                          select new { d.Total } into x
                                          group x by new { } into g
                                          select new Histofactmodel
                                          {
                                              Sommeto = (g.Select(x => x.Total).Sum()).Value,
                                          }).ToList();

            if (ModelState.IsValid)
            {
                var v = from p in db.produits
                            //join u in db.tableachats
                            //   on p.produitID equals u.produitID
                        where (p.produitID == tableachat.produitID)
                        select new testquantite
                        {
                            quanti = p.Quantite.Value,
                            quantit = tableachat.Quantite.Value,
                        };



                int k = 0; int l = 0; int rest = 0;
                foreach (var item in v)
                {
                    k = item.quanti;
                    l = item.quantit;
                }

                if (k >= l)
                {
                    rest = k - l;



                    produit produtData = db.produits.Where(u => u.produitID == produit.produitID).SingleOrDefault();
                    produtData.Quantite = rest;




                    db.tableachats.Add(tableachat);

                    db.historiquefacturations.Add(historiquefacturation);
                    db.SaveChanges();

                    TempData["msg"] = "<script>alert('Opération Réussie avec success');</script>";

                }
                else
                {
                    TempData["msg"] = "<script>alert('La Quantite Demandée est Inférieur à Celle du Stock');</script>";
                }
                return RedirectToAction("create2", "tableachats");
            }
             ViewBag.clientID = new SelectList(db.clients, "clientID", "nomclient", tableachat.clientID);
             ViewBag.produitID = new SelectList(db.produits, "produitID", "nomproduit", tableachat.produitID);
             


            return View(tableachat);
        }



        // GET: tableachats/Create
        public ActionResult Create()
        {
            var client = db.clients;
            ViewData["client"] = client.ToList();


            var tableachat = db.tableachats;
            ViewData["tableachat"] = tableachat.ToList();

            var facturation = db.facturations;
            ViewData["facturation"] = facturation.ToList();

            ViewBag.categorieID = new SelectList(db.categories, "categorieID", "nomcategorie");
            ViewBag.clientID = new SelectList(db.clients, "clientID", "nomclient");
            ViewBag.produitID = new SelectList(db.produits, "produitID", "nomproduit");
            return View();
        }

        // POST: tableachats/Create
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(tableachat tableachat, historiquefacturation historiquefacturation,produit produit)
        {   

             if (ModelState.IsValid)
            {
                var v = from p in db.produits
                            //join u in db.tableachats
                            //   on p.produitID equals u.produitID
                        where (p.produitID == tableachat.produitID)
                        select new testquantite
                        {
                            quanti = p.Quantite.Value,
                            quantit = tableachat.Quantite.Value,
                        };



                int k = 0; int l = 0; int rest = 0;
                foreach (var item in v)
                {
                    k = item.quanti;
                    l = item.quantit;
                }

                if (k >= l)
                    {
                        rest = k - l;



                        produit produtData = db.produits.Where(u => u.produitID == produit.produitID).SingleOrDefault();
                        produtData.Quantite = rest;




                        db.tableachats.Add(tableachat);
                        
                        db.historiquefacturations.Add(historiquefacturation);
                        db.SaveChanges();

                    TempData["msg"] = "<script>alert('Opération Réussie avec success');</script>";

                }
                else
                {
                    TempData["msg"] = "<script>alert('La Quantite Demandée est Inférieur à Celle du Stock');</script>";
                }


                return RedirectToAction("create", "tableachats");
            }
            

            ViewBag.clientID = new SelectList(db.clients, "clientID", "nomclient", tableachat.clientID);
            ViewBag.produitID = new SelectList(db.produits, "produitID", "nomproduit", tableachat.produitID);
            return View(tableachat);
        }



        public ActionResult Create4()
        {
            var client = db.clients;
            ViewData["client"] = client.ToList();

            var tableachat = db.tableachats;
            ViewData["tableachat"] = tableachat.ToList();

            var facturation = db.facturations;
            ViewData["facturation"] = facturation.ToList();
            
            ViewBag.categorieID = new SelectList(db.categories, "categorieID", "nomcategorie");
            ViewBag.clientID = new SelectList(db.clients, "clientID", "nomclient");
            ViewBag.produitID = new SelectList(db.produits, "produitID", "nomproduit");
            //ViewBag.ID = new SelectList(db.tableachats, "produitID", "nomproduit");
            return View();
        }
        // POST: tableachats/Create
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create4(tableachat tableachat, facturation facturation, historiquefacturation historiquefacturation, produit produit)
        {
            if (ModelState.IsValid)
            {
                var v = from p in db.produits
                            //join u in db.tableachats
                            //   on p.produitID equals u.produitID
                        where (p.produitID == tableachat.produitID)
                        select new testquantite
                        {
                            quanti = p.Quantite.Value,
                            quantit = tableachat.Quantite.Value,
                        };



                int k = 0; int l = 0; int rest = 0;
                foreach (var item in v)
                {
                    k = item.quanti;
                    l = item.quantit;
                }

                if (k >= l)
                {
                    rest = k - l;



                    produit produtData = db.produits.Where(u => u.produitID == produit.produitID).SingleOrDefault();
                    produtData.Quantite = rest;




                    db.tableachats.Add(tableachat);
                    db.facturations.Add(facturation);
                    db.historiquefacturations.Add(historiquefacturation);
                    db.SaveChanges();

                    TempData["msg"] = "<script>alert('Opération Réussie avec success');</script>";

                }
                else
                {
                    TempData["msg"] = "<script>alert('La Quantite Demandée est Inférieur à Celle du Stock');</script>";
                }


                return RedirectToAction("create4", "tableachats");
            }

            ViewBag.clientID = new SelectList(db.clients, "clientID", "nomclient", tableachat.clientID);
            ViewBag.produitID = new SelectList(db.produits, "produitID", "nomproduit", tableachat.produitID);
            return View(tableachat);
        }





        public ActionResult Create1()
        {

            var histo = db.tableachats;
            ViewData["histo"] = histo.ToList();

            var client = db.clients;
            ViewData["client"] = client.ToList();

            var facturation = db.facturations;
            ViewData["facturation"] = facturation.ToList();

            ViewBag.categorieID = new SelectList(db.categories, "categorieID", "nomcategorie");
            ViewBag.clientID = new SelectList(db.clients, "clientID", "nomclient");
            ViewBag.produitID = new SelectList(db.produits, "produitID", "nomproduit");
            //ViewBag.ID = new SelectList(db.tableachats, "produitID", "nomproduit");
            return View();
        }
        // POST: tableachats/Create
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create1(tableachat tableachat, facturation facturation, historiquefacturation historiquefacturation,produit produit)
        {
            if (ModelState.IsValid)
            {
                var v = from p in db.produits
                            //join u in db.tableachats
                            //   on p.produitID equals u.produitID
                        where (p.produitID == tableachat.produitID)
                        select new testquantite
                        {
                            quanti = p.Quantite.Value,
                            quantit = tableachat.Quantite.Value,
                        };



                int k = 0; int l = 0; int rest = 0;
                foreach (var item in v)
                {
                    k = item.quanti;
                    l = item.quantit;
                }

                if (k >= l)
                    {
                        rest = k - l;



                        produit produtData = db.produits.Where(u => u.produitID == produit.produitID).SingleOrDefault();
                        produtData.Quantite = rest;




                        db.tableachats.Add(tableachat);
                        db.facturations.Add(facturation);
                        db.historiquefacturations.Add(historiquefacturation);
                        db.SaveChanges();

                    TempData["msg"] = "<script>alert('Opération Réussie avec success');</script>";

                }
                else
                {
                    TempData["msg"] = "<script>alert('La Quantite Demandée est Inférieur à Celle du Stock');</script>";
                }


                return RedirectToAction("create4", "tableachats");
            }

            ViewBag.clientID = new SelectList(db.clients, "clientID", "nomclient", tableachat.clientID);
            ViewBag.produitID = new SelectList(db.produits, "produitID", "nomproduit", tableachat.produitID);
            return View(tableachat);
        }


        public ActionResult Create3()
        {
            var client = db.clients;
            ViewData["client"] = client.ToList();

            var facturation = db.facturations;
            ViewData["facturation"] = facturation.ToList();

            ViewBag.categorieID = new SelectList(db.categories, "categorieID", "nomcategorie");
            ViewBag.clientID = new SelectList(db.clients, "clientID", "nomclient");
            ViewBag.produitID = new SelectList(db.produits, "produitID", "nomproduit");
            return View();
        }
        // POST: tableachats/Create
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create3(tableachat tableachat, facturation facturation, historiquefacturation historiquefacturation,produit produit)
        {
            if (ModelState.IsValid)
            {
                var v = from p in db.produits
                            //join u in db.tableachats
                            //   on p.produitID equals u.produitID
                        where (p.produitID == tableachat.produitID)
                        select new testquantite
                        {
                            quanti = p.Quantite.Value,
                            quantit = tableachat.Quantite.Value,
                        };



                int k = 0; int l = 0; int rest = 0;
                foreach (var item in v)
                {
                    k = item.quanti;
                    l = item.quantit;
                }

                if (k >= l)
                {
                    rest = k - l;



                    produit produtData = db.produits.Where(u => u.produitID == produit.produitID).SingleOrDefault();
                    produtData.Quantite = rest;




                    db.tableachats.Add(tableachat);
                    db.facturations.Add(facturation);
                    db.historiquefacturations.Add(historiquefacturation);
                    db.SaveChanges();

                    TempData["msg"] = "<script>alert('Opération Réussie avec success');</script>";

                }
                else
                {
                    TempData["msg"] = "<script>alert('La Quantite Demandée est Inférieur à Celle du Stock');</script>";
                }


                return RedirectToAction("create5", "tableachats");
            }

            ViewBag.clientID = new SelectList(db.clients, "clientID", "nomclient", tableachat.clientID);
            ViewBag.produitID = new SelectList(db.produits, "produitID", "nomproduit", tableachat.produitID);
            return View(tableachat);
        }

        public ActionResult Create5()
        {
            var client = db.clients;
            ViewData["client"] = client.ToList();


            var tableachat = db.tableachats;
            ViewData["tableachat"] = tableachat.ToList();


            var facturation = db.facturations;
            ViewData["facturation"] = facturation.ToList();

            ViewBag.categorieID = new SelectList(db.categories, "categorieID", "nomcategorie");
            ViewBag.clientID = new SelectList(db.clients, "clientID", "nomclient");
            ViewBag.produitID = new SelectList(db.produits, "produitID", "nomproduit");
            return View();
        }
        // POST: tableachats/Create
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create5(tableachat tableachat, facturation facturation, historiquefacturation historiquefacturation, produit produit)
        {
            if (ModelState.IsValid)
            {
                var v = from p in db.produits
                            //join u in db.tableachats
                            //   on p.produitID equals u.produitID
                        where (p.produitID == tableachat.produitID)
                        select new testquantite
                        {
                            quanti = p.Quantite.Value,
                            quantit = tableachat.Quantite.Value,
                        };



                int k = 0; int l = 0; int rest = 0;
                foreach (var item in v)
                {
                    k = item.quanti;
                    l = item.quantit;
                }

                if (k >= l)
                {
                    rest = k - l;



                    produit produtData = db.produits.Where(u => u.produitID == produit.produitID).SingleOrDefault();
                    produtData.Quantite = rest;




                    db.tableachats.Add(tableachat);
                    db.facturations.Add(facturation);
                    db.historiquefacturations.Add(historiquefacturation);
                    db.SaveChanges();

                    TempData["msg"] = "<script>alert('Opération Réussie avec success');</script>";

                }
                else
                {
                    TempData["msg"] = "<script>alert('La Quantite Demandée est Inférieur à Celle du Stock');</script>";
                }


                return RedirectToAction("create5", "tableachats");
            }

            ViewBag.clientID = new SelectList(db.clients, "clientID", "nomclient", tableachat.clientID);
            ViewBag.produitID = new SelectList(db.produits, "produitID", "nomproduit", tableachat.produitID);
            return View(tableachat);
        }





        // GET: tableachats/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tableachat tableachat = db.tableachats.Find(id);
            if (tableachat == null)
            {
                return HttpNotFound();
            }
            ViewBag.clientID = new SelectList(db.clients, "clientID", "nomclient", tableachat.clientID);
            ViewBag.produitID = new SelectList(db.produits, "produitID", "nomproduit", tableachat.produitID);
            return View(tableachat);
        }

        // POST: tableachats/Edit/5
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,clientID,produitID,Quantite,Total")] tableachat tableachat)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tableachat).State = EntityState.Modified;
                db.SaveChanges();

                TempData["msg"] = "<script>alert('Opération Réussie avec success');</script>";

                return RedirectToAction("Index");
            }
            ViewBag.clientID = new SelectList(db.clients, "clientID", "nomclient", tableachat.clientID);
            ViewBag.produitID = new SelectList(db.produits, "produitID", "nomproduit", tableachat.produitID);
            return View(tableachat);
        }

        // GET: tableachats/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tableachat tableachat = db.tableachats.Find(id);
            if (tableachat == null)
            {
                return HttpNotFound();
            }
            return View(tableachat);
        }


        public ActionResult Delete1(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tableachat tableachat = db.tableachats.Find(id);
            if (tableachat == null)
            {
                return HttpNotFound();
            }
            return View(tableachat);
        }
        public ActionResult DeleteAd(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tableachat tableachat = db.tableachats.Find(id);
            if (tableachat == null)
            {
                return HttpNotFound();
            }
            return View(tableachat);
        }


        public ActionResult DeleteAdce(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tableachat tableachat = db.tableachats.Find(id);
            if (tableachat == null)
            {
                return HttpNotFound();
            }
            return View(tableachat);
        }
        // POST: tableachats/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tableachat tableachat = db.tableachats.Find(id);
            db.tableachats.Remove(tableachat);
            db.SaveChanges();

            TempData["msg"] = "<script>alert('La Supression Réussie avec success');</script>";

            return RedirectToAction("Index");
        }

        [HttpPost, ActionName("Delete1")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed1(int id)
        {
            tableachat tableachat = db.tableachats.Find(id);
            db.tableachats.Remove(tableachat);
            db.SaveChanges();

            TempData["msg"] = "<script>alert('La Supression Réussie avec success');</script>";

            return RedirectToAction("create","tableachats");
        }

        [HttpPost, ActionName("DeleteAd")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmedAd(int id)
        {
            tableachat tableachat = db.tableachats.Find(id);
            db.tableachats.Remove(tableachat);
            db.SaveChanges();

            TempData["msg"] = "<script>alert('La Supression Réussie avec success');</script>";

            return RedirectToAction("create4", "tableachats");
        }

        [HttpPost, ActionName("DeleteAdce")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmedAdce(int id)
        {
            tableachat tableachat = db.tableachats.Find(id);
            db.tableachats.Remove(tableachat);
            db.SaveChanges();

            TempData["msg"] = "<script>alert('La Supression Réussie avec success');</script>";

            return RedirectToAction("create5", "tableachats");
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
