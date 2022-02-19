using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DesafioProgramacao.Data;
using DesafioProgramacao.Models;

namespace DesafioProgramacao.Controllers
{
    public class EnderecosController : Controller
    {
        private Context db = new Context();

        // GET: Enderecos
        public async Task<ActionResult> Index()
        {
            var enderecos = db.Enderecos.Include(e => e.Estado);
            return View(await enderecos.ToListAsync());
        }

        // GET: Enderecos/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Endereco endereco = await db.Enderecos.FindAsync(id);
            if (endereco == null)
            {
                return HttpNotFound();
            }
            return View(endereco);
        }

        // GET: Enderecos/Create
        public ActionResult Create()
        {
            ViewBag.EstadoId = new SelectList(db.Estados, "Id", "Estado");
            return View();
        }

        // POST: Enderecos/Create
        // Para se proteger de mais ataques, habilite as propriedades específicas às quais você quer se associar. Para 
        // obter mais detalhes, veja https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Logradouro,Numero,Complemento,CEP,Bairro,Cidade,EstadoId")] Endereco endereco)
        {
            if (ModelState.IsValid)
            {
                db.Enderecos.Add(endereco);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.EstadoId = new SelectList(db.Estados, "Id", "Estado", endereco.EstadoId);
            return View(endereco);
        }

        // GET: Enderecos/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Endereco endereco = await db.Enderecos.FindAsync(id);
            if (endereco == null)
            {
                return HttpNotFound();
            }
            ViewBag.EstadoId = new SelectList(db.Estados, "Id", "Estado", endereco.EstadoId);
            return View(endereco);
        }

        // POST: Enderecos/Edit/5
        // Para se proteger de mais ataques, habilite as propriedades específicas às quais você quer se associar. Para 
        // obter mais detalhes, veja https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Logradouro,Numero,Complemento,CEP,Bairro,Cidade,EstadoId")] Endereco endereco)
        {
            if (ModelState.IsValid)
            {
                db.Entry(endereco).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.EstadoId = new SelectList(db.Estados, "Id", "Estado", endereco.EstadoId);
            return View(endereco);
        }

        // GET: Enderecos/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Endereco endereco = await db.Enderecos.FindAsync(id);
            if (endereco == null)
            {
                return HttpNotFound();
            }
            return View(endereco);
        }

        // POST: Enderecos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Endereco endereco = await db.Enderecos.FindAsync(id);
            db.Enderecos.Remove(endereco);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public JsonResult SalvarEndereco(string logradouro, int numero, string complemento, string cep, string bairro, string cidade, int estadoId)
        {
            var endereco = new Endereco
            {
                Logradouro = logradouro,
                Numero = numero,
                Complemento = complemento,
                CEP = cep,
                Bairro = bairro,
                Cidade = cidade,
                EstadoId = estadoId
            };
            SalvarRegistro(endereco);
            var listaEndereco = db.Enderecos.Include(e => e.Estado).ToList();
            List<SelectListItem> selectList = new List<SelectListItem>();

            listaEndereco.ForEach(item => selectList.Add(new SelectListItem
            {
                Value = item.Id.ToString(),
                Text = $"{item.Logradouro} {item.Numero}, {item.Bairro} - {item.Cidade}/{item.Estado.UF}"
            }));
            return Json(new SelectList(listaEndereco, "Value", "Text"), JsonRequestBehavior.AllowGet);
        }

        private void SalvarRegistro(Endereco endereco)
        {
            if (ModelState.IsValid)
            {
                db.Enderecos.Add(endereco);
                db.SaveChanges();
            }
        }
    }
}
