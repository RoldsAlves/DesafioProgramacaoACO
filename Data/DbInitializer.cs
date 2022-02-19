using DesafioProgramacao.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DesafioProgramacao.Data
{
    public class DbInitializer : System.Data.Entity. DropCreateDatabaseIfModelChanges<Context>
    {
        protected override void Seed(Context context)
        {
            if (context.Usuarios.Any()) return;

            var perfil = new List<Perfil>()
            {
                new Perfil { Descricao = "Administrador" },
                new Perfil { Descricao = "Gerente" },
                new Perfil { Descricao = "Usuário" }
            };
            perfil.ForEach(p => context.Perfils.Add(p));
            context.SaveChanges();

            var estados = new List<Estados>()
            {
                new Estados { Estado = "Acre", UF = "AC"},
                new Estados { Estado = "Alagoas", UF = "AL"},
                new Estados { Estado = "Amapá", UF = "AP"},
                new Estados { Estado = "Amazonas", UF = "AM"},
                new Estados { Estado = "Bahia", UF = "BA"},
                new Estados { Estado = "Ceará", UF = "CE"},
                new Estados { Estado = "Distrito Federal", UF = "DF"},
                new Estados { Estado = "Espírito Santo", UF = "ES"},
                new Estados { Estado = "Goiás", UF = "GO"},
                new Estados { Estado = "Maranhão", UF = "MA"},
                new Estados { Estado = "Mato Grosso", UF = "MT"},
                new Estados { Estado = "Mato Grosso do Sul", UF = "MS"},
                new Estados { Estado = "Minas Gerais", UF = "MG"},
                new Estados { Estado = "Pará", UF = "PA"},
                new Estados { Estado = "Paraíba", UF = "PB"},
                new Estados { Estado = "Paraná", UF = "PR"},
                new Estados { Estado = "Pernambuco", UF = "PE"},
                new Estados { Estado = "Piauí", UF = "PI"},
                new Estados { Estado = "Rio de Janeiro", UF = "RJ"},
                new Estados { Estado = "Rio Grande do Norte", UF = "RN"},
                new Estados { Estado = "Rio Grande do Sul", UF = "RS"},
                new Estados { Estado = "Rondônia", UF = "RO"},
                new Estados { Estado = "Roraima", UF = "RR"},
                new Estados { Estado = "Santa Catarina", UF = "SC"},
                new Estados { Estado = "São Paulo", UF = "SP"},
                new Estados { Estado = "Sergipe", UF = "SE"},
                new Estados { Estado = "Tocantins", UF = "TO"}
            };
            estados.ForEach(e => context.Estados.Add(e));
            context.SaveChanges();
        }
    }
}