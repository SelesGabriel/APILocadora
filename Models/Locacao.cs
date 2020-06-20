using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Locadora.Models
{
    [Table("Locacao")]
    public class Locacao
    {
        [Key]
        public int IdLocacao { get; set; }
        [Required(ErrorMessage = "Insira o Id do filme.")]
        public int FilmeId { get; set; }
        [Required(ErrorMessage = "Insira o Id do cliente.")]
        public int ClienteId { get; set; }
        [Required(ErrorMessage = "Coloque a quantidade de dias que o filme ficará com o cliente.")]
        public int QtdDias { get; set; }
        public DateTime DataLocacao { get; set; } = DateTime.Now;
        [NotMapped]
        public string Mensagem
        {
            get
            {
                DateTime dataFinal = DataLocacao.AddDays(QtdDias);
                int diasFaltantes = (int)dataFinal.Subtract(DateTime.Today).TotalDays;
                if (Devolveu)
                    return "Cliente devolveu o filme";
                else if (diasFaltantes == 0)
                    return "A data de entrega se encerra hoje.";
                else if (diasFaltantes > 0)
                    return $"Faltam {diasFaltantes} dias para se encerrar o prazo de entrega";

                diasFaltantes.ToString().Replace("-", "");
                return $"A data de entrega expirou faz {diasFaltantes} dias.";

            }
        }
        public bool Devolveu { get; set; } = false; //Default = false;
        public Filme Filme { get; set; }
        public Cliente Cliente { get; set; }

    }
}
