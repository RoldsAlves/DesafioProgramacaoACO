﻿using DesafioProgramacao.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace DesafioProgramacao.Data
{
    public class Context : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public Context() : base("name=Context")
        {
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        public System.Data.Entity.DbSet<DesafioProgramacao.Models.Usuario> Usuarios { get; set; }

        public System.Data.Entity.DbSet<DesafioProgramacao.Models.Perfil> Perfils { get; set; }

        public System.Data.Entity.DbSet<DesafioProgramacao.Models.Endereco> Enderecos { get; set; }

        public System.Data.Entity.DbSet<DesafioProgramacao.Models.Estados> Estados { get; set; }

        public System.Data.Entity.DbSet<DesafioProgramacao.Models.UsuarioEndereco> UsuarioEnderecos { get; set; }
    }
}
