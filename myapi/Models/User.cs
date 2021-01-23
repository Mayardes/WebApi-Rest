using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace myapi.Models
{
    [Table("Usuario")]
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage="Este campo é obrigatório")]
        [MaxLength(20, ErrorMessage ="Este campo deve possuir entre 3 e 20 caracteres")]
        [MinLength(3, ErrorMessage = "Este campo deve possuir entre 3 e 20 caracteres")]
        public string Username { get; set; }

        [Required(ErrorMessage="Este campo é obrigatório")]
        [MaxLength(20, ErrorMessage ="Este campo deve possuir entre 3 e 20 caracteres")]
        [MinLength(3, ErrorMessage = "Este campo deve possuir entre 3 e 20 caracteres")]
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
