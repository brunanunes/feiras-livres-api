using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FeirasLivres.Core.Entities
{
    [Table("TBL_SUBPREFEITURAS")]
    public class Subprefeitura
    {
        public Subprefeitura()
        {
            Feiras = new HashSet<Feira>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("CODSUBPREF"), Required]
        public int CodigoSubprefeitura { get; set; }

        [Column("SUBPREF"), Required]
        [StringLength(25)]
        public string NomeSubprefeitura { get; set; }

        [JsonIgnore]
        public virtual ICollection<Feira> Feiras { get; set; }
    }
}