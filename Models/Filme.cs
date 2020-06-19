using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Locadora.Models
{
    [Table("Filme")]
    public class Filme
    {
        [Key]
        public int IdFilme { get; set; }
        [Required(ErrorMessage ="Campo 'Nome' é obrigatório.")]
        public string Nome { get; set; }
        [Required(ErrorMessage ="Campo 'Categoria' é obrigatório.")]
        public string Categoria { get; set; }
        [Required(ErrorMessage ="Defina se o filme estará disponível.")]
        /// <summary>
        /// Campo setado como true, uma vez que uma regra não foi definida para esta propriedade, ao adicionar um filme
        /// o valor default será sempre disponível.
        /// </summary>
        public bool Disponivel { get; set; } = true;
    }
}
