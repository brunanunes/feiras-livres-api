using System.ComponentModel.DataAnnotations;

namespace FeirasLivres.Core.DTOs
{
    public class SubprefeituraDTO
    {
        [Required]
        public int CodigoSubprefeitura { get; set; }
        [Required]
        public string NomeSubprefeitura { get; set; }
    }
}
