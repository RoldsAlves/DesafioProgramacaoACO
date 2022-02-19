using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DesafioProgramacao.Models
{
    public class Endereco
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("Logradouro")]
        public string Logradouro { get; set; }
        [DisplayName("Nº")]
        public int Numero { get; set; }
        [DisplayName("Complemento")]
        public string Complemento { get; set; }
        [DisplayName("CEP")]
        [DataType(DataType.PostalCode)]
        public string CEP { get; set; }
        [DisplayName("Bairro")]
        public string Bairro { get; set; }
        [DisplayName("Cidade")]
        public string Cidade { get; set; }
        [DisplayName("Estado")]
        public int EstadoId { get; set; }
        public virtual Estados Estado { get; set; }
        public virtual List<UsuarioEndereco> UsuarioEnderecos { get; set; }
    }
}