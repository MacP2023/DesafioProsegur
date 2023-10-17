using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ApiGestionPedidos.Models
{
    public class Orden
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrdenID { get; set; }

      
        public int ClientID { get; set; }

        [Required]
        public int Estado {  get; set; }

        [Required]
        public DateTime FechaOrden {  get; set; }

        public List<OrdenDetalle> OrdenDetalle { get; set; }
    }
}
