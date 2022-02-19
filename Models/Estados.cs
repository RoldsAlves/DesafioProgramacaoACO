using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DesafioProgramacao.Models
{
    public class Estados
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("Estado")]
        public string Estado { get; set; }
        [DisplayName("UF")]
        public string UF { get; set; }
    }
}