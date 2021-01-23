using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace myapi.Models
{
    [Table("Categoria")]
    public class Category
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [Required(ErrorMessage="Este campo é obrigatório")]
        [MaxLength(60, ErrorMessage ="Este campo deve possuir entre 3 e 60 caracteres")]
        [MinLength(3, ErrorMessage = "Este campo deve possuir entre 3 e 60 caracteres")]
        [Column("Titulo")]
        public string Title { get; set; }
    }
}
