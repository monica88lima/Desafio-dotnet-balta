using System.ComponentModel.DataAnnotations;

namespace DesafioDotnet_balta.DTOs
{
    public class UserRegisterDTO
    {
        
        [MaxLength(80)]
        [MinLength(2)]
        [Required(ErrorMessage = "Campo Obrigatório")]
        public string FirstName { get; set; }


        [MaxLength(80)]
        [MinLength(2)]
        [Required(ErrorMessage = "Campo Obrigatório")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório")]
        [EmailAddress(ErrorMessage = "Formato de e-mail inválido!")]

        public string Email { get; set; }


        [Required(ErrorMessage = "Campo Obrigatório")]
        [StringLength(100, MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório")]
        public string Profile { get; set; }

       
    }
}
