using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DesafioProgramacao.Models
{
    public class Perfil
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("Descrição")]
        public string Descricao { get; set; }
    }
}