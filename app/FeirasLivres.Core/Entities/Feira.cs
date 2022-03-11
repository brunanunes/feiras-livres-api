using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FeirasLivres.Core.Entities
{
    [Table("TBL_FEIRAS")]
    public class Feira
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ID"), Required]
        public int Id { get; set; }

        [Column("LONG"), Required]
        public int Longitude { get; set; }

        [Column("LAT"), Required]
        public int Latitude { get; set; }

        [Column("SETCENS"), Required]
        [StringLength(15)]
        public string SetorCensitario { get; set; }

        [Column("AREAP"), Required]
        [StringLength(13)]
        public string AreaPonderacao { get; set; }
                
        [Column("REGIAO5"), Required]
        [StringLength(6)]
        public string Regiao { get; set; }

        [Column("REGIAO8"), Required]
        [StringLength(7)]
        public string Subregiao { get; set; }

        [Column("NOME_FEIRA"), Required]
        [StringLength(30)]
        public string NomeFeira { get; set; }

        [Column("REGISTRO"), Required]
        [StringLength(6)]
        public string Registro { get; set; }

        [Column("LOGRADOURO"), Required]
        [StringLength(34)]
        public string Logradouro { get; set; }

        [Column("NUMERO")]
        [StringLength(15)]
        public string Numero { get; set; }

        [Column("BAIRRO")]
        [StringLength(20)]
        public string Bairro { get; set; }

        [Column("REFERENCIA")]
        [StringLength(30)]
        public string Referencia { get; set; }

        [JsonIgnore]
        [Column("CODDIST"), Required]
        public int CodigoDistrito { get; set; }

        public virtual Distrito Distrito { get; set; }

        [JsonIgnore]
        [Column("CODSUBPREF"), Required]
        public int CodigoSubprefeitura { get; set; }

        public virtual Subprefeitura Subprefeitura { get; set; }
    }
}
