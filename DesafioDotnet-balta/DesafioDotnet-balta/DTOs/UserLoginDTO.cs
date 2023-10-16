using System.ComponentModel.DataAnnotations;

namespace DesafioDotnet_balta.DTOs
{
    public class UserLoginDTO
    {
        [Required(ErrorMessage = "Campo Obrigatório")]
        [EmailAddress(ErrorMessage = "Formato de e-mail inválido!")]

        public string Email { get; set; }


        [Required(ErrorMessage = "Campo Obrigatório")]
        [StringLength(100, MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
