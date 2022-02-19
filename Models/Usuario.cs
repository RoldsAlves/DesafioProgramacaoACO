using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DesafioProgramacao.Models
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("CPF")]
        //[Index("IndexCPF", IsUnique = true, Order = 1)]
        public string CPF { get; set; }
        [DisplayName("Nome")]
        public string Nome { get; set; }
        [DisplayName("E-mail")]
        [DisplayFormat(ApplyFormatInEditMode = true, NullDisplayText = "nome@email.com")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [DisplayName("Perfil")]
        public int PerfilId { get; set; }
        public virtual Perfil Perfil { get; set; }
        [DisplayName("Data Cadastro")]
        [DataType(DataType.Date)]
        public DateTime DataCadastro { get; set; }
        [DisplayName("Endereços")]
        public virtual List<UsuarioEndereco> UsuarioEnderecos { get; set; }
    }
}