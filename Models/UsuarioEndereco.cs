using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DesafioProgramacao.Models
{
    public class UsuarioEndereco
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("Usuario")]
        public int UsuarioId { get; set; }
        public virtual Usuario Usuario { get; set; }

        [DisplayName("Endereço")]
        public int EnderecoId { get; set; }
        public virtual Endereco Endereco { get; set; }

        [DisplayName("Tipo de Endereço")]
        public string TipoEndereco { get; set; }
    }
}