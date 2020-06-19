using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Locadora.Models
{
    [Table("Cliente")]
    public class Cliente
    {
        [Key]
        public int IdCliente { get; set; }
        [Required(ErrorMessage ="Este campo é obrigatório.")]
        public string Nome { get; set; }
        [Required(ErrorMessage ="Este campo é obrigatório.")]
        public string Sobrenome { get; set; }
        [CustomValidate.ValidaCPF(ErrorMessage ="CPF inválido")]
        [Required(ErrorMessage ="CPF obrigatório")]
        public string CPF { get; set; }
    }
}
