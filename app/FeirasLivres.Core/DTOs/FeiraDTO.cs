using System.ComponentModel.DataAnnotations;

namespace FeirasLivres.Core.DTOs
{
    public class FeiraDTO
    {        
        [Required(ErrorMessage = "The {0} field is required")]
        public int Longitude { get; set; }

        [Required(ErrorMessage = "The {0} field is required")]
        public int Latitude { get; set; }

        [Required(ErrorMessage = "The {0} field is required")]
        public string SetorCensitario { get; set; }

        [Required(ErrorMessage = "The {0} field is required")]
        public string AreaPonderacao { get; set; }

        [Required(ErrorMessage = "The {0} field is required")]
        public string Regiao { get; set; }

        [Required(ErrorMessage = "The {0} field is required")]
        public string Subregiao { get; set; }

        [Required(ErrorMessage = "The {0} field is required")]
        public string NomeFeira { get; set; }

        [Required(ErrorMessage = "The {0} field is required")]
        public string Registro { get; set; }

        [Required(ErrorMessage = "The {0} field is required")]
        public string Logradouro { get; set; }

        [Required(ErrorMessage = "The {0} field is required")]
        public string Numero { get; set; }

        [Required(ErrorMessage = "The {0} field is required")]
        public string Bairro { get; set; }

        [Required(ErrorMessage = "The {0} field is required")]
        public string Referencia { get; set; }

        [Required(ErrorMessage = "The {0} field is required")]
        public int CodigoDistrito { get; set; }

        [Required(ErrorMessage = "The {0} field is required")]
        public int CodigoSubprefeitura { get; set; }
    }
}
