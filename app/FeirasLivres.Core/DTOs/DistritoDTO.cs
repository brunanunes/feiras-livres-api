using System.ComponentModel.DataAnnotations;

namespace FeirasLivres.Core.DTOs
{
    public class DistritoDTO
    {
        [Required]
        public int CodigoDistrito { get; set; }
        [Required]
        public string NomeDistrito { get; set; }
    }
}