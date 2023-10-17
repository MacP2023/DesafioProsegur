using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GestionDePedidosSB.Models
{
    public class Orden
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrdenID { get; set; }

        // [ForeignKey("UsuarioID")]
        public int ClientID { get; set; }

       
        public int Estado { get; set; }

       
        public DateTime FechaOrden { get; set; }

        public List<OrdenDetalle> OrdenDetalle { get; set; }

    }
}
