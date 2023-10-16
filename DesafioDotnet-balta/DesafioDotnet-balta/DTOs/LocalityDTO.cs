using System.ComponentModel.DataAnnotations;

namespace DesafioDotnet_balta.DTOs
{
    public class LocalityDTO
    {
        [Key]
        [Required(ErrorMessage = "Campo Obrigatório")]
        [MaxLength(7)]
        [MinLength(7)]
        public string Id { get; set; }

        [StringLength(2)]
        [Required(ErrorMessage = "Campo Obrigatório")]
        public string State { get; set; }

        [MaxLength(100)]
        [MinLength(2)]
        [Required(ErrorMessage = "Campo Obrigatório")]
        public string City { get; set; }
        
    }
}
