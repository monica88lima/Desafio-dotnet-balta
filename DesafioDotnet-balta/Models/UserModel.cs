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

        
        [MaxLength(80)]
        [MinLength(2)]
        [Required(ErrorMessage = "Campo Obrigatório")]
        public string FirstName { get; set; }

        
        [MaxLength(80)]
        [MinLength(2)]
        [Required(ErrorMessage = "Campo Obrigatório")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório")]
        [EmailAddress (ErrorMessage = "Formato de e-mail inválido!")]
        
        public string Email { get; set; }

        
        [Required(ErrorMessage = "Campo Obrigatório")]
        [StringLength(100, MinimumLength = 6 )]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        
        public string Profile{ get; set; }

        public DateTime CreationDate { get; set; } = DateTime.Now;

    }
}
