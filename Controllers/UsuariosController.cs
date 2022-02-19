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
    public class UsuariosController : Controller
    {
        private Context db = new Context();

        // GET: Usuarios
        public async Task<ActionResult> Index(DateTime? date1, DateTime? date2, string name, string cpf)
        {
            if (date1 == null && date2 != null)
            {
                date1 = date2;
            }
            else if (date2 == null && date1 != null)
            {
                date2 = date1;
            }

            var usuarios = db.Usuarios.Include(u => u.Perfil).Include(u => u.UsuarioEnderecos);
            if (!string.IsNullOrEmpty(cpf))
            {
                usuarios = usuarios.Where(u => u.CPF == cpf);
                return View(await usuarios.ToListAsync());
            }

            if (!string.IsNullOrEmpty(name))
            {
                usuarios = usuarios.Where(u => u.Nome.Contains(name));
                return View(await usuarios.ToListAsync());
            }

            if(date1 != null || date2 != null)
            {
                usuarios = usuarios.Where(u => DbFunctions.TruncateTime(u.DataCadastro) >= date1
                    && DbFunctions.TruncateTime(u.DataCadastro) <= date2);
                return View(await usuarios.ToListAsync());
            }
        
            return View(await usuarios.ToListAsync());
        }

        // GET: Usuarios/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = await db.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // GET: Usuarios/Create
        public ActionResult Create()
        {
            ViewBag.PerfilId = new SelectList(db.Perfils, "Id", "Descricao");
            ViewBag.EstadoId = new SelectList(db.Estados, "Id", "UF");
            ViewBag.EnderecoId = new SelectList(ListItemEndereco(), "Value", "Text");
            ViewBag.TipoEndereco = new SelectList(TipoEnderecos());
            Usuario usuario = new Usuario();
            usuario.UsuarioEnderecos = new List<UsuarioEndereco>()
            {
                new UsuarioEndereco
                {

                }
            };
            return View(usuario);
        }

        // POST: Usuarios/Create
        // Para se proteger de mais ataques, habilite as propriedades específicas às quais você quer se associar. Para 
        // obter mais detalhes, veja https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,CPF,Nome,Email,PerfilId,DataCadastro,UsuarioEnderecos")] Usuario usuario, List<int> EnderecoId, List<string> TipoEndereco)
        {
            usuario.DataCadastro = DateTime.Today;
            List<UsuarioEndereco> usuarioEndereco = new List<UsuarioEndereco>();
            for (int item = 0; item < EnderecoId.Count; item++)
            {
                if (EnderecoId[item] > 0)
                {
                    usuarioEndereco.Add(new UsuarioEndereco
                    {
                        TipoEndereco = TipoEndereco[item],
                        EnderecoId = EnderecoId[item],
                        Endereco = db.Enderecos.Find(EnderecoId[item]),
                        Usuario = usuario
                    });
                }
            }

            if (ModelState.IsValid)
            {
                db.Usuarios.Add(usuario);
                usuarioEndereco.ForEach(ue => db.UsuarioEnderecos.Add(ue));
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.PerfilId = new SelectList(db.Perfils, "Id", "Descricao", usuario.PerfilId);
            ViewBag.EnderecoId = new SelectList(db.Enderecos, "Id", "Logradouro", EnderecoId);
            ViewBag.TipoEndereco = new SelectList(TipoEnderecos());
            return View(usuario);
        }

        // GET: Usuarios/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = await db.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            ViewBag.PerfilId = new SelectList(db.Perfils, "Id", "Descricao", usuario.PerfilId);
            ViewBag.EnderecoId = new SelectList(db.Enderecos, "Id", "Logradouro", usuario.UsuarioEnderecos);
            ViewBag.TipoEndereco = new SelectList(TipoEnderecos(), usuario.UsuarioEnderecos);
            return View(usuario);
        }

        // POST: Usuarios/Edit/5
        // Para se proteger de mais ataques, habilite as propriedades específicas às quais você quer se associar. Para 
        // obter mais detalhes, veja https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,CPF,Nome,Email,PerfilId,DataCadastro")] Usuario usuario, List<int> EnderecoId, List<string> TipoEndereco)
        {
            List<UsuarioEndereco> usuarioEndereco = new List<UsuarioEndereco>();
            for (int item = 0; item < EnderecoId.Count; item++)
            {
                if (EnderecoId[item] > 0)
                {
                    usuarioEndereco.Add(new UsuarioEndereco
                    {
                        TipoEndereco = TipoEndereco[item],
                        EnderecoId = EnderecoId[item],
                        Endereco = db.Enderecos.Find(EnderecoId[item]),
                        Usuario = usuario
                    });
                }
            }

            if (ModelState.IsValid)
            {
                db.Entry(usuario).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.PerfilId = new SelectList(db.Perfils, "Id", "Descricao", usuario.PerfilId);
            ViewBag.EnderecoId = new SelectList(db.Enderecos, "Id", "Logradouro");
            ViewBag.TipoEndereco = new SelectList(TipoEnderecos());
            return View(usuario);
        }

        // GET: Usuarios/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = await db.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Usuario usuario = await db.Usuarios.FindAsync(id);
            db.Usuarios.Remove(usuario);
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

        private List<string> TipoEnderecos()
        {
            var tipo = new List<string>()
            {
                "Residencial",
                "Comercial"
            };
            return tipo;
        }

        private List<SelectListItem> ListItemEndereco()
        {
            var lista = db.Enderecos.Include(x => x.Estado);
            var selectListItem = new List<SelectListItem>();
            foreach(var item in lista)
            {
                selectListItem.Add(new SelectListItem
                {
                    Value = item.Id.ToString(),
                    Text = $"{item.Logradouro} {item.Numero}, {item.Bairro} - {item.Cidade}/{item.Estado.UF}"
                });
            }
            return selectListItem;
        }
    }
}
