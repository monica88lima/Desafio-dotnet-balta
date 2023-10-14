using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class LocalityModel
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
        public DateTime CreationDate { get; set; } = DateTime.Now;
        public DateTime UpdateDate { get; set; } = DateTime.Now;
    }
}
