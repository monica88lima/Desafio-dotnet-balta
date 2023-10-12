using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class UserModel
    {
        [Key]
        public int UserId { get; set; }

        [Display(Name = " Primeiro Nome")]
        [MaxLength(80)]
        [MinLength(2)]
        [Required(ErrorMessage = "Campo Obrigatório")]
        public string FirstName { get; set; }

        [Display(Name = "Sobrenome")]
        [MaxLength(80)]
        [MinLength(2)]
        [Required(ErrorMessage = "Campo Obrigatório")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório")]
        [EmailAddress (ErrorMessage = "Formato de e-mail inválido!")]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Display(Name = "Senha")]
        [Required(ErrorMessage = "Campo Obrigatório")]
        [StringLength(100, MinimumLength = 6 )]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public DateTime CreationDate { get; set; } = DateTime.Now;

    }
}
