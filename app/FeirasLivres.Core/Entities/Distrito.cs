using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FeirasLivres.Core.Entities
{
    [Table("TBL_DISTRITOS")]
    public class Distrito
    {
        public Distrito()
        {
            Feiras = new HashSet<Feira>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("CODDIST"), Required]
        public int CodigoDistrito { get; set; }

        [Column("DISTRITO"), Required]
        [StringLength(18)]
        public string NomeDistrito { get; set; }

        [JsonIgnore]
        public virtual ICollection<Feira> Feiras { get; set; }
    }
}