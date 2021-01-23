using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace myapi.Models
{
    [Table("Produto")]
    public class Product
    {
        [Key]
        [Column("Id")]
        public int Id {get; set; }

        [Required(ErrorMessage="Este campo é obrigatório")]
        [MaxLength(60, ErrorMessage ="Este campo deve possuir entre 3 e 60 caracteres")]
        [MinLength(3, ErrorMessage = "Este campo deve possuir entre 3 e 60 caracteres")]
        [Column("Titulo")]
        public string Title { get; set; }

        [MaxLength(1024, ErrorMessage ="Este campo deve conter 1024 caracteres")]
        public string Description { get; set; }

        [Required(ErrorMessage="Este campo é obrigatório")]
        [Range(1, int.MaxValue, ErrorMessage ="Este campo deve ser maior que zero")]
        [Column("Valor")]
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }

    }
}
